using UnityEngine;

public class Vulnerable : MonoBehaviour
{
    public bool isVulnerable = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Vulnerable"))
        {
            isVulnerable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Vulnerable"))
        {
            isVulnerable = false;
        }
    }

}