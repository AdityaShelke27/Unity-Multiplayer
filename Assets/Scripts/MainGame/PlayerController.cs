using Fusion;
using UnityEngine;

public class PlayerController : NetworkBehaviour, IBeforeUpdate
{
    [Networked] private NetworkButtons buttonsPrev { get; set; }
    private Rigidbody2D rigidBody;
    private float horizontal;
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
    }
    public void BeforeUpdate()
    {
        if(Runner.LocalPlayer.IsValid == Object.HasInputAuthority)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if(Runner.TryGetInputForPlayer(Object.InputAuthority, out PlayerData input))
        {
            rigidBody.linearVelocity = new Vector2(input.HorizontalInput * moveSpeed, rigidBody.linearVelocityY);
            CheckJumpInput(input);
        }
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
        data.NetworkButtons.Set(PlayerInputButtons.Jump, Input.GetKeyDown(KeyCode.Space));

        return data;
    }
}
