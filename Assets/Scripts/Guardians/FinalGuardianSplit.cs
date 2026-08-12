using UnityEngine;

public class FinalGuardianSplit : MonoBehaviour
{

    private GuardianDeath guardianDeathScript;
    public GameObject smallGuardian;
    private bool isSplit;
    [SerializeField] private Vector3 rightGuardianPos;
    [SerializeField] private Vector3 leftGuardianPos;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        guardianDeathScript = GetComponent<GuardianDeath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (guardianDeathScript._hitsTaken == 1 && !isSplit)
        {
            Split();
        }

    }

    private void Split()
    {
        isSplit = true;

        Instantiate(smallGuardian, transform.position, Quaternion.identity);
        GameObject rightGuardian = Instantiate(smallGuardian, rightGuardianPos, Quaternion.identity);
        GameObject leftGuardian = Instantiate(smallGuardian, leftGuardianPos, Quaternion.identity);

        Destroy(gameObject);
    }

}
