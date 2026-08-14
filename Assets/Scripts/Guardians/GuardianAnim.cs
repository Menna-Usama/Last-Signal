using System;
using UnityEditor;
using UnityEngine;

public class GuardianAnim : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Update()
    {
        // flip sprite when moved

    }

    private void OnEnable()
    {
        GuardianJump.OnGuardianJump += PlayJumpAnim;
    }
    private void OnDisable()
    {
        GuardianJump.OnGuardianJump -= PlayJumpAnim;
    }
    private void PlayJumpAnim()
    {
        animator.SetTrigger("Jump");
    }
}
