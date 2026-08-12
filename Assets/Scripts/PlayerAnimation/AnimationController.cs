using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Move moveScript;
    private Jump jumpScript;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveScript = GetComponent<Move>();
        jumpScript = GetComponent<Jump>();
    }
    private void OnEnable()
    {
        Jump.OnPlayerJumped += JumpAnim;
    }

    private void OnDisable()
    {
        Jump.OnPlayerJumped -= JumpAnim;
    }

    private void Update()
    {
        if (moveScript.direction.x > 0f)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveScript.direction.x < 0f)
        {
            spriteRenderer.flipX = true;
        }

        RunAnim();
        animator.SetBool("onGround", jumpScript.OnGround);
        animator.SetFloat("yVelocity", jumpScript.Velocity.y);

    }
    private void RunAnim()
    {
        bool isMoving = Mathf.Abs(moveScript.Velocity.x) > 0.05f;
        animator.SetBool("isRunning", isMoving);
    }
    private void JumpAnim()
    {
        animator.SetTrigger("Jump");
    }
   
}
