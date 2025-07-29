using Fusion;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float moveSpeed = 20;
    [SerializeField] private float lifeTimeAmount = 0.8f;
    [Networked] private TickTimer lifeTimeTimer { get; set; }
    [Networked] private NetworkBool didHitSomething { get; set; }
    BoxCollider2D col;

    public override void Spawned()
    {
        base.Spawned();

        col = GetComponent<BoxCollider2D>();
        lifeTimeTimer = TickTimer.CreateFromSeconds(Runner, lifeTimeAmount);
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        CheckIfHitGround();

        if (!lifeTimeTimer.ExpiredOrNotRunning(Runner) && !didHitSomething)
        {
            transform.Translate(moveSpeed * Runner.DeltaTime * transform.right, Space.World);
        }
        else
        {
            Runner.Despawn(Object);
        }
    }

    void CheckIfHitGround()
    {
        Collider2D groundCol = Runner.GetPhysicsScene2D().OverlapBox(transform.position, col.bounds.size, 0, groundLayerMask);

        if (groundCol != default)
        {
            didHitSomething = true;
        }
    }
}
