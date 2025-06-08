using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI enemyCountText;

    public GameObject escMenu;
    public GameObject settingsMenu;
    public Button exitButton;
    public Button settingsButton;
    public Button applyButton;
    public Button restartButton;

    private CameraController[] cameraControllers;

    public Slider mouseSensitivitySlider;
    public TextMeshProUGUI mouseSensitivityText; // 숫자 표시용

    public Slider enemyCountSlider;
    public TextMeshProUGUI enemyCountTextUI; // 숫자 표시용

    private float elapsedTime = 0f;
    private int enemyCount;
    private bool gameStarted = false;
    private bool isPaused = false;
    private bool isTimerRunning = false;

    private EnemyPool enemyPool;
    private CameraController cameraController;

    void Start()
    {
        enemyPool = FindObjectOfType<EnemyPool>();
        cameraControllers = FindObjectsOfType<CameraController>();

        exitButton.onClick.AddListener(QuitGame);
        settingsButton.onClick.AddListener(OpenSettings);
        applyButton.onClick.AddListener(ApplySettings);
        restartButton.onClick.AddListener(RestartGame);

        escMenu.SetActive(false);
        settingsMenu.SetActive(false);

        // 슬라이더 값 변경 시 즉시 반영
        mouseSensitivitySlider.onValueChanged.AddListener(UpdateMouseSensitivityText);
        enemyCountSlider.onValueChanged.AddListener(UpdateEnemyCountText);

        enemyCount = enemyPool.maxEnemies;
        UpdateEnemyCountUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = $"Time: {elapsedTime:F2}";
        }
    }

    public void StartGame()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            isTimerRunning = true;
            elapsedTime = 0f;

            enemyCount = enemyPool.maxEnemies; // 게임 시작 전에 정확한 적 개수 설정
            UpdateEnemyCountUI();

            enemyPool.enabled = true;
            enemyPool.SpawnFirstEnemy();

            Time.timeScale = 1; // 게임이 시작되면 타임스케일 정상화
            Cursor.lockState = CursorLockMode.Locked; // 마우스 입력 활성화
            Cursor.visible = false; // 마우스 숨김
        }
    }
    public void RestartGame() // 게임 다시 시작 기능 추가
    {
        Time.timeScale = 1; //  다시 시작할 때 시간 흐름 정상화
        Cursor.lockState = CursorLockMode.Locked; //  마우스 잠금
        Cursor.visible = false; //  마우스 숨기기


        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 리로드
    }

    public void EnemyKilled()
    {
        enemyCount--;
        UpdateEnemyCountUI();

        if(enemyCount <= -1)
        {
            StopGame();
        }
    }
    public void UpdateEnemyCountUI()
    {
        enemyCountText.text = $"Remain: {enemyCount+1}"; // UI 갱신
    }

    void StopGame()
    {
        gameStarted = false;
        isTimerRunning = false;
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        escMenu.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Camera.main.GetComponent<CameraController>().isPaused = true;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Camera.main.GetComponent<CameraController>().isPaused = false;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("게임 종료");
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        escMenu.SetActive(false);

        mouseSensitivitySlider.value = cameraControllers[0].mouseSensitivity;
        enemyCountSlider.value = enemyPool.maxEnemies;

        // 슬라이더 옆 숫자 업데이트
        UpdateMouseSensitivityText(mouseSensitivitySlider.value);
        UpdateEnemyCountText(enemyCountSlider.value);
    }

    public void ApplySettings()
    {
        foreach (CameraController camController in cameraControllers) //** 모든 카메라 컨트롤러에 감도 적용
        {
            camController.SetMouseSensitivity(mouseSensitivitySlider.value);
        }

        enemyPool.maxEnemies = (int)enemyCountSlider.value;

        CloseSettings();
    }
    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
        escMenu.SetActive(true);
    }

    // 슬라이더 값 변경 시 숫자 업데이트
    public void UpdateMouseSensitivityText(float value)
    {
        mouseSensitivityText.text = value.ToString("F1");
    }

    public void UpdateEnemyCountText(float value)
    {
        enemyCountTextUI.text = ((int)value).ToString();
    }
}
