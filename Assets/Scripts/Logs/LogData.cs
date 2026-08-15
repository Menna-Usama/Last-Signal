using UnityEngine;


[CreateAssetMenu(fileName = "NewLog", menuName = "LogsSo/Log")]

public class LogData : ScriptableObject
{
    [TextArea(1, 6)]//Don't think we'll need 6 lines but we'll see
    public string logText;
    public int segmentNumber;
    public string logID; //string so that the name could be self explanatory sorta

}
