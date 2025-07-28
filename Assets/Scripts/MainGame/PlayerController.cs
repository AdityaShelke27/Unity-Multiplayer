using Fusion;
using TMPro;
using UnityEngine;

public class PlayerController : NetworkBehaviour, IBeforeUpdate
{
    [Networked] private NetworkButtons buttonsPrev { get; set; }
    [SerializeField] TMP_Text NicknameText;
    private Rigidbody2D rigidBody;
    private PlayerWeaponController weaponController;
    private PlayerVisualController playerVisualController;
    private float horizontal;
    [Networked(OnChanged = nameof(OnNicknameChanged))] NetworkString<_8> NetworkedName { get; set; }
    [SerializeField] private GameObject Cam;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    public enum PlayerInputButtons
    {
        None,
        Jump
    }
    public override void Spawned()
    {
        base.Spawned();

        rigidBody = GetComponent<Rigidbody2D>();
        weaponController = GetComponent<PlayerWeaponController>();
        playerVisualController = GetComponent<PlayerVisualController>();
        SetLocalObjects();
    }
    public void BeforeUpdate()
    {
        if(Object.HasInputAuthority)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }
    }
    private static void OnNicknameChanged(Changed<PlayerController> changed)
    {
        changed.Behaviour.SetPlayerNickname(changed.Behaviour.NetworkedName);
    }
    void SetPlayerNickname(NetworkString<_8> name)
    {
        NicknameText.text = name + " " + Runner.LocalPlayer.PlayerId;
    }
    private void SetLocalObjects()
    {
        if(Object.HasInputAuthority)
        {
            RpcSetNickname(GlobalManagers.Instance.NetworkRunnerController.PlayerNickname);
        }
        else
        {
            Destroy(Cam);
        }
    }
    [Rpc(sources: RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RpcSetNickname(NetworkString<_8> name)
    {
        NetworkedName = name;
    }
    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if(Runner.TryGetInputForPlayer(Object.InputAuthority, out PlayerData input))
        {
            rigidBody.linearVelocity = new Vector2(input.HorizontalInput * moveSpeed, rigidBody.linearVelocityY);
            CheckJumpInput(input);
        }

        playerVisualController.UpdateScaleTransform(rigidBody.linearVelocity);
    }
    public override void Render()
    {
        base.Render();

        playerVisualController.RenderVisuals(rigidBody.linearVelocity);
    }
    private void CheckJumpInput(PlayerData input)
    {
        NetworkButtons pressed = input.NetworkButtons.GetPressed(buttonsPrev);
        if(pressed.WasPressed(buttonsPrev, PlayerInputButtons.Jump))
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
        }

        buttonsPrev = input.NetworkButtons;
    }
    public PlayerData GetPlayerNetworkInput()
    {
        PlayerData data = new();
        data.HorizontalInput = horizontal;
        data.GunPivotRotation = weaponController.LocalPivotRot;
        data.NetworkButtons.Set(PlayerInputButtons.Jump, Input.GetKey(KeyCode.Space));

        return data;
    }
}
