using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public GameObject howToPlayPanel; //** 게임 방법 패널

    void Start()
    {
        //** 시작 시 How to Play 패널 비활성화
        if (howToPlayPanel != null)
        {
            howToPlayPanel.SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("StrinovaScene"); //** StrinovaScene으로 이동
    }

    public void ShowHowToPlay()
    {
        if (howToPlayPanel != null)
        {
            howToPlayPanel.SetActive(true); //** How to Play 패널 활성화
        }
    }

    public void HideHowToPlay()
    {
        if (howToPlayPanel != null)
        {
            howToPlayPanel.SetActive(false); //** How to Play 패널 비활성화
        }
    }

    public void ExitGame()
    {
        Application.Quit(); //** 게임 종료
        Debug.Log("게임 종료됨"); //** 에디터에서 확인용 로그
    }
}
