using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("StrinovaScene");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("∞‘¿” ¡æ∑·µ ");
    }
}
