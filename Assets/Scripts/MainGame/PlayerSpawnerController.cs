using Fusion;
using UnityEngine;

public class PlayerSpawnerController : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] private NetworkPrefabRef playerNetworkPrefab = NetworkPrefabRef.Empty;
    [SerializeField] private Transform[] spawnPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayerJoined(PlayerRef player)
    {
        SpawnPlayer(player);
    }
    void SpawnPlayer(PlayerRef playerRef)
    {
        if(Runner.IsServer)
        {
            int index = playerRef % spawnPoints.Length;
            NetworkObject obj = Runner.Spawn(playerNetworkPrefab, spawnPoints[index].position, Quaternion.identity, playerRef);

            Runner.SetPlayerObject(playerRef, obj);
        }
    }
    public override void Spawned()
    {
        if(Runner.IsServer)
        {
            foreach(PlayerRef item in Runner.ActivePlayers)
            {
                SpawnPlayer(item);
            }
        }
    }

    public void PlayerLeft(PlayerRef player)
    {
        DespawnPlayer(player);
    }
    void DespawnPlayer(PlayerRef player)
    {
        if(Runner.IsServer)
        {
            if(Runner.TryGetPlayerObject(player, out NetworkObject playerNetworkObject))
            {
                Runner.Despawn(playerNetworkObject);
            }

            Runner.SetPlayerObject(player, null);
        }
    }
}
