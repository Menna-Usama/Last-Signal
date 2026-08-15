using UnityEngine;

public class FinalGuardianSplit : MonoBehaviour
{

    private GuardianDeath guardianDeathScript;
    public GameObject smallGuardianMiddle;
    public GameObject smallGuardianRight;
    public GameObject smallGuardianLeft;
    private bool isSplit;

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

        smallGuardianMiddle.SetActive(true);
        smallGuardianRight.SetActive(true);
        smallGuardianLeft.SetActive(true);

        Destroy(gameObject);
    }

}