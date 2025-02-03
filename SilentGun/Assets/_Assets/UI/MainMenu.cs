using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button button;
    public Sprite ActionExit;
    public Sprite ActionStart;
    
    public void StartGame()
    {
        SceneManager.LoadScene("DefGameScene");
    }

    public void ExitGame()
    {
        //Debug.Log("ExitGame çalıştı");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
