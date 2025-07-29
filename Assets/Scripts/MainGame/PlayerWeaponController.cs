using Fusion;
using UnityEngine;

public class PlayerWeaponController : NetworkBehaviour, IBeforeUpdate
{
    public Quaternion LocalPivotRot { get; set; }

    [SerializeField] private Camera localCam;
    [SerializeField] private Transform pivotToRotate;
    [SerializeField] private float delayBetweenShots = 0.18f;
    [SerializeField] private ParticleSystem muzzleEffect;
    [SerializeField] private Transform firePoint;
    [SerializeField] private NetworkPrefabRef bulletPrefab = NetworkPrefabRef.Empty;

    [Networked] Quaternion currentPlayerPivotRotation { get; set; }
    [Networked] private NetworkButtons buttonsPrev { get; set; }
    [Networked] private TickTimer shootTimer { get; set; }
    [Networked(OnChanged = nameof(OnMuzzleEffectStateChanged))] NetworkBool playMuzzleEffect { get; set; }
    [Networked, HideInInspector] public NetworkBool isFirePressed { get; private set; }

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
            CheckShootInput(input);
            currentPlayerPivotRotation = input.GunPivotRotation;

            buttonsPrev = input.NetworkButtons;
        }

        pivotToRotate.rotation = currentPlayerPivotRotation;
    }

    private void CheckShootInput(PlayerData input)
    {
        NetworkButtons currentButtons = input.NetworkButtons.GetPressed(buttonsPrev);
        isFirePressed = currentButtons.WasReleased(buttonsPrev, PlayerController.PlayerInputButtons.Shoot);

        if (currentButtons.WasReleased(buttonsPrev, PlayerController.PlayerInputButtons.Shoot) && shootTimer.ExpiredOrNotRunning(Runner))
        {
            shootTimer = TickTimer.CreateFromSeconds(Runner, delayBetweenShots);
            playMuzzleEffect = true;
            Runner.Spawn(bulletPrefab, firePoint.position, firePoint.rotation, Object.InputAuthority);
            Debug.Log("Shoot");
        }
        else
        {
            playMuzzleEffect = false;
        }
    }
    private static void OnMuzzleEffectStateChanged(Changed<PlayerWeaponController> changed)
    {
        bool currentState = changed.Behaviour.playMuzzleEffect;
        changed.LoadOld();
        bool previousState = changed.Behaviour.playMuzzleEffect;

        if(currentState != previousState)
        {
            changed.Behaviour.PlayOrStopMuzzleEffect(currentState);
        }
    }
    private void PlayOrStopMuzzleEffect(bool play)
    {
        if(play)
        {
            muzzleEffect.Play();
        }
        else
        {
            muzzleEffect.Stop();
        }
    }
}
