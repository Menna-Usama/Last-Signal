using UnityEngine;
using System;

public class GuardianDeath : MonoBehaviour
{

    [SerializeField] private int guardianHealth = 3;

    public int _hitsTaken;
    private Vulnerable vulnerableScript;
    public static event Action OnGuardianDefeated;

    private void Awake()
    {
        Debug.Log("Awake");
        vulnerableScript = GetComponent<Vulnerable>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Dash dash = collision.gameObject.GetComponent<Dash>();//fetch the Dash script on the player
        if (dash != null && dash.isDashing && vulnerableScript.isVulnerable)
        {
            TakeHit();
            Debug.Log("Collision");
        }

    }

    private void TakeHit()
    {
        _hitsTaken++;
        if (_hitsTaken >= guardianHealth)
        {
            OnGuardianDefeated?.Invoke();
            Destroy(gameObject);
        }
    }
}
