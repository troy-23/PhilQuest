using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [Header("Game Over UI")]
    public GameObject panel;
    public Button closeButton;

    [Header("Quiz Logic")]
    public QuizManager quizManager;

    private void Start()
    {
        if (panel != null)
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
    }

    public void Show()
    {
        if (panel != null)
        {
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
    }

    public void OnClose()
    {
        if (panel != null)
        {
            panel.SetActive(false);
            Debug.Log("❎ GameOverPanel closed.");
        }
    }
}
