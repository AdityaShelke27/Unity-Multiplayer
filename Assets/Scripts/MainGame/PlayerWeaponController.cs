using Fusion;
using UnityEngine;

public class PlayerWeaponController : NetworkBehaviour, IBeforeUpdate
{
    public Quaternion LocalPivotRot { get; set; }

    [SerializeField] private Camera localCam;
    [SerializeField] private Transform pivotToRotate;

    [Networked] Quaternion currentPlayerPivotRotation { get; set; }

    public void BeforeUpdate()
    {
        if(Object.HasInputAuthority)
        {
            Vector2 dir = localCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            LocalPivotRot = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if(Runner.TryGetInputForPlayer<PlayerData>(Object.InputAuthority, out PlayerData input))
        {
            currentPlayerPivotRotation = input.GunPivotRotation;
        }

        pivotToRotate.rotation = currentPlayerPivotRotation;
    }
}
