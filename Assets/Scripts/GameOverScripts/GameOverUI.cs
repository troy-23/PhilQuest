using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [Header("Game Over UI")]
    public GameObject panel;
<<<<<<< HEAD
=======
    public Button retryButton;
>>>>>>> c24b1a07e585ccda977bb888e024ad6aeb0c6132
    public Button closeButton;

    [Header("Quiz Logic")]
    public QuizManager quizManager;

    private void Start()
    {
        if (panel != null)
<<<<<<< HEAD
        {
            panel.SetActive(false);
            Debug.Log("🟢 GameOver panel initialized as hidden.");
        }

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(OnClose);
        }
        else
        {
            Debug.LogWarning("❌ CloseButton not assigned in GameOverUI.");
        }
=======
            panel.SetActive(false);

        if (retryButton != null)
            retryButton.onClick.AddListener(OnRetry);

        if (closeButton != null)
            closeButton.onClick.AddListener(OnClose);
>>>>>>> c24b1a07e585ccda977bb888e024ad6aeb0c6132
    }

    public void Show()
    {
        if (panel != null)
        {
<<<<<<< HEAD
            Debug.Log($"📣 Showing GameOverPanel... Current Active State: {panel.activeSelf}");
            panel.SetActive(true);

            // Optional visual debug
            Image img = panel.GetComponent<Image>();
            if (img != null)
                img.color = new Color(1, 0, 0, 0.4f); // red tint to confirm it's visible

            Debug.Log($"✅ GameOverPanel should now be visible. Active State: {panel.activeSelf}");
        }
        else
        {
            Debug.LogWarning("❌ GameOver panel reference is missing!");
        }
=======
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
>>>>>>> c24b1a07e585ccda977bb888e024ad6aeb0c6132
    }

    public void OnClose()
    {
        if (panel != null)
<<<<<<< HEAD
        {
            panel.SetActive(false);
            Debug.Log("❎ GameOverPanel closed.");
        }
=======
            panel.SetActive(false);
>>>>>>> c24b1a07e585ccda977bb888e024ad6aeb0c6132
    }
}
