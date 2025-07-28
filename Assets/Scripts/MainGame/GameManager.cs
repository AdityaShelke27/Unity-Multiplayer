using Fusion;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private GameObject Cam;
    public override void Spawned()
    {
        base.Spawned();
        Cam.SetActive(false);
    }
}
