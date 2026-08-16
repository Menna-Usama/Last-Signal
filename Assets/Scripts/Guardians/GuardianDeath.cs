using UnityEngine;
using System;

public class GuardianDeath : MonoBehaviour
{
    [SerializeField] private int guardianHealth = 3;
    [SerializeField] private GuardianAnim guardianAnim;
    [SerializeField] private GuardianFire guardianFire;
    public  bool _isDead = false;
    public int _hitsTaken;
    private Vulnerable vulnerableScript;
    public event Action OnGuardianDefeated;
    public event Action onHitTaken;

    private void Awake()
    {
        vulnerableScript = GetComponent<Vulnerable>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Dash dash = collision.gameObject.GetComponent<Dash>();
        if (dash != null && dash.isDashing)
        {
            TakeHit();
        }
    }

    private void TakeHit()
    {
        if (_isDead) return;

        onHitTaken?.Invoke();
        _hitsTaken++;
        Debug.Log("hits" +_hitsTaken);

        if (_hitsTaken >= guardianHealth)
        {

            _isDead = true;
            GetComponent<Collider2D>().enabled = false; //to avoid guardian from being hit while dying

            OnGuardianDefeated?.Invoke(); 

            Destroy(gameObject, 1f);
        }
        else
        {
            guardianAnim.PlayHurtAnim(); // only play hurtAnim if the guardian isnt dying this hit
            guardianFire.InterruptAttack(); //so that the guardian doesnt hit the player when the guardian is getting hit

        }
    }
}