using System;
using UnityEngine;

public class WhenPlayerSeeGuardian : MonoBehaviour
{

    public static Action OnPlayerSeeGuardian;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SeeGuardian"))
        {
            OnPlayerSeeGuardian?.Invoke();
        }
    }

}
