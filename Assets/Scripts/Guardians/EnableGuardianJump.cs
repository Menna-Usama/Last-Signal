using UnityEngine;

public class EnableGuardianJump : MonoBehaviour
{

    private GuardianJump guardianJumpScript;


    private void Awake()
    {
        guardianJumpScript = GetComponent<GuardianJump>();
    }


    private void OnEnable()
    {
        WhenPlayerSeeGuardian.OnPlayerSeeGuardian += EnableJump;
    }
    private void OnDisable()
    {
        WhenPlayerSeeGuardian.OnPlayerSeeGuardian -= EnableJump;
    }


    private void EnableJump()
    {
        guardianJumpScript.enabled = true;
    }

}
