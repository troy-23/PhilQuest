using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;  // ✅ Required for new Input System

public class PauseMenuHandler : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject quitConfirmUI;
    public AudioSource bgMusic;

    private bool isPaused = false;

    // ✅ InputAction reference
    private InputAction pauseAction;

    void Awake()
    {
        // 🔑 Bind ESC key (or Back button on Android)
        pauseAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/escape");
#if UNITY_ANDROID
        pauseAction.AddBinding("<Gamepad>/start"); // optionally for Android gamepads
        pauseAction.AddBinding("<AndroidBackButton>/back"); // Android back button (fallback)
#endif
    }

    void OnEnable()
    {
        pauseAction.Enable();
        pauseAction.performed += ctx => TogglePause(); // Bind once
    }

    void OnDisable()
    {
        pauseAction.Disable();
        pauseAction.performed -= ctx => TogglePause();
    }

    private void TogglePause()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        if (bgMusic != null) bgMusic.Pause();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        quitConfirmUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        if (bgMusic != null) bgMusic.Play();
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowQuitConfirm()
    {
        quitConfirmUI.SetActive(true);
    }

    public void QuitGameConfirmed()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void CancelQuit()
    {
        quitConfirmUI.SetActive(false);
    }
}
