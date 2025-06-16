using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [Header("Game Over UI")]
    public GameObject panel;
    public Button retryButton;
    public Button closeButton;

    [Header("Quiz Logic")]
    public QuizManager quizManager;

    private void Start()
    {
        if (panel != null)
            panel.SetActive(false);

        if (retryButton != null)
            retryButton.onClick.AddListener(OnRetry);

        if (closeButton != null)
            closeButton.onClick.AddListener(OnClose);
    }

    public void Show()
    {
        if (panel != null)
        {
            panel.SetActive(true);
            Debug.Log("✅ GameOverPanel shown.");
        }
    }

    public void OnRetry()
    {
        Debug.Log("🔁 Retry clicked.");

        if (PlayerStats.Instance != null)
        {
            PlayerStats.Instance.ResetHealth();
        }

        if (quizManager != null)
        {
            quizManager.RestartQuiz();
        }

        if (panel != null)
            panel.SetActive(false);
    }

    public void OnClose()
    {
        if (panel != null)
            panel.SetActive(false);
    }
}
