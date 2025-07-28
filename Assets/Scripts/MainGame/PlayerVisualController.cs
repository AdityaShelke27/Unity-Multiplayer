using UnityEngine;

public class PlayerVisualController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject body;
    bool isFacingRight;
    Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }
    public void RenderVisuals(Vector2 velocity)
    {
        bool isWalking = velocity.x != 0;
        animator.SetBool("IsWalking", isWalking);
    }
    public void UpdateScaleTransform(Vector2 velocity)
    {
        if(velocity.x > 0.1f)
        {
            isFacingRight = true;
        }
        else if(velocity.x < -0.1f)
        {
            isFacingRight = false;
        }

        body.transform.localScale = new Vector3(isFacingRight ? 1 : -1, originalScale.y, originalScale.z);
    }
}
