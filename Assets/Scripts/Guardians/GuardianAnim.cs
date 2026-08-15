using System;
using UnityEngine;

public class GuardianAnim : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GuardianJump guardianJump;
    private void OnEnable()
    {
        GuardianJump.OnGuardianJump += PlayJumpAnim;

        GuardianMovement.OnGuardianMoveStateChanged += SetMovingState;

        GuardianFire.onProjectileFired += AttackAnim;

        GuardianDeath.OnGuardianDefeated += PlayDeathAnim;
    }

    private void OnDisable()
    {
        GuardianJump.OnGuardianJump -= PlayJumpAnim;

        GuardianMovement.OnGuardianMoveStateChanged -= SetMovingState;

        GuardianFire.onProjectileFired -= AttackAnim;

        GuardianDeath.OnGuardianDefeated -= PlayDeathAnim;

    }
    private void Update()
    {
        if (guardianJump != null)
        {
            animator.SetBool("isGrounded", !guardianJump.IsJumping);
        }
    }

    private void PlayJumpAnim()
    {
        animator.SetTrigger("Jump");
    }

    private void SetMovingState(bool isMoving)
    {
        animator.SetBool("isMoving", isMoving);
    }

    private void AttackAnim()
    {
        animator.SetTrigger("Attack");
    }
    public void PlayHurtAnim()
    {
        Debug.Log("PlayHurtAnim executing, setting Hurt trigger");

        animator.SetTrigger("Hurt");
    }
    public void PlayDeathAnim()
    {
        animator.SetTrigger("Death");
    }
}