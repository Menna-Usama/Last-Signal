using UnityEngine;

public class UIButtonHandler : MonoBehaviour
{
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
            
            Application.Quit();
#endif
    }
}
