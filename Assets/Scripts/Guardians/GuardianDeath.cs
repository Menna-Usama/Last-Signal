using UnityEngine;
using System;

public class GuardianDeath : MonoBehaviour
{

    [SerializeField] private int guardianHealth = 3;

    public int _hitsTaken;
    public static event Action OnGuardianDefeated;




    private void OnCollisionEnter2D(Collision2D collision)
    {
        Dash dash = collision.gameObject.GetComponent<Dash>();//fetch the Dash script on the player
        if (dash != null && dash.isDashing)
        {
            TakeHit();
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
