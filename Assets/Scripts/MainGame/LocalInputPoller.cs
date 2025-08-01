using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LocalInputPoller : NetworkBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] PlayerController player;

    public override void Spawned()
    {
        if(Runner.LocalPlayer.IsValid && Object.HasInputAuthority)
        {
            Runner.AddCallbacks(this);
        }
    }
    public void OnConnectedToServer(NetworkRunner runner)
    {
         
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
         
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
         
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
         
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
         
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
         
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if(Runner != null && Runner.IsRunning)
        {
            PlayerData data = player.GetPlayerNetworkInput();
            input.Set(data);
        }
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
         
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
         
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
         
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
         
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
         
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
         
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
         
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
         
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
         
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
