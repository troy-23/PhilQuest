using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

[System.Serializable]
public class QuizQuestion
{
    public string question;
    public string[] options;
    public int correctIndex;
}

public class QuizManager : MonoBehaviour
{
    [Header("Quiz UI")]
    public GameObject quizPanel;
    public TextMeshProUGUI title;
    public TextMeshProUGUI questionText;
    public Button[] optionButtons;
    public TextMeshProUGUI feedbackText;
    public GameObject tipPanel;

    [Header("Feedback Images")]
    public GameObject correctImage;
    public GameObject wrongImage;

    [Header("Health UI")]
    public GameObject healthPanel;

    [Header("Game Over")]
    public GameOverUI gameOverUI;

    [Header("Congrats Panel")]
    public CongratsPanelManager congratsPanelManager;

    [Header("NPC")]
    public GameObject npc; // just rename for clarity

    private int currentQuestion = 0;
    private bool answered = false;

    public QuizQuestion[] questions;

    private bool isCooldownActive = false;
    private float cooldownDuration = 30f;

    void Start()
    {
        if (gameOverUI != null)
        {
            gameOverUI.panel.SetActive(false);
            gameOverUI.quizManager = this;
        }

        quizPanel?.SetActive(false);
        healthPanel?.SetActive(false);
    }

    public void StartQuiz()
    {
        if (isCooldownActive)
        {
            Debug.Log("⛔ Quiz is locked. Please wait.");
            return;
        }

        currentQuestion = 0;
        quizPanel?.SetActive(true);
        healthPanel?.SetActive(true);
        PlayerStats.Instance?.ResetHealth();
        LoadQuestion();
    }

    void LoadQuestion()
    {
        answered = false;
        feedbackText.text = "";
        tipPanel.SetActive(false);
        HideFeedbackImages();

        QuizQuestion q = questions[currentQuestion];
        title.text = $"Quiz #{currentQuestion + 1}";
        questionText.text = q.question;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            int idx = i;
            optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = q.options[i];
            optionButtons[i].interactable = true;
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => CheckAnswer(idx));
        }
    }

    void CheckAnswer(int index)
    {
        if (answered) return;
        answered = true;

        foreach (Button btn in optionButtons)
            btn.interactable = false;

        bool isCorrect = index == questions[currentQuestion].correctIndex;

        if (isCorrect)
        {
            feedbackText.text = "Correct!";
            ShowFeedbackImage(true);
            StartCoroutine(NextQuestionAfterDelay(true));
        }
        else
        {
            feedbackText.text = "Wrong!";
            ShowFeedbackImage(false);
            tipPanel.SetActive(true);

            PlayerStats.Instance?.TakeDamage(1);

            if (PlayerStats.Instance != null && PlayerStats.Instance.currentHealth <= 0)
            {
                HandleGameOver();
            }
            else
            {
                StartCoroutine(NextQuestionAfterDelay(false));
            }
        }
    }

    IEnumerator NextQuestionAfterDelay(bool isCorrect)
    {
        yield return new WaitForSeconds(1.5f);
        feedbackText.text = "";
        tipPanel.SetActive(false);
        HideFeedbackImages();

        if (isCorrect)
        {
            currentQuestion++;
            if (currentQuestion < questions.Length)
            {
                LoadQuestion();
            }
            else
            {
                quizPanel?.SetActive(false);
                healthPanel?.SetActive(false);

                if (congratsPanelManager != null)
                {
                    Debug.Log("🎉 Showing Congrats Panel...");
                    congratsPanelManager.ShowCongratsPanel();
                }
            }
        }
        else
        {
            answered = false;
            EnableButtons();
        }
    }

    void HandleGameOver()
    {
        Debug.Log("☠️ GameOver triggered.");
        quizPanel?.SetActive(false);
        healthPanel?.SetActive(false);
        gameOverUI?.Show();

        StartCoroutine(StartCooldown());
    }

    IEnumerator StartCooldown()
    {
        isCooldownActive = true;
        Debug.Log("⏳ Starting quiz cooldown...");
        yield return new WaitForSeconds(cooldownDuration);
        isCooldownActive = false;
        Debug.Log("✅ Quiz unlocked.");
    }

    public void RestartQuiz()
    {
        currentQuestion = 0;
        quizPanel?.SetActive(true);
        healthPanel?.SetActive(true);
        PlayerStats.Instance?.ResetHealth();
        LoadQuestion();
    }

    void EnableButtons()
    {
        foreach (Button btn in optionButtons)
            btn.interactable = true;
    }

    void ShowFeedbackImage(bool correct)
    {
        correctImage?.SetActive(correct);
        wrongImage?.SetActive(!correct);
    }

    void HideFeedbackImages()
    {
        correctImage?.SetActive(false);
        wrongImage?.SetActive(false);
    }

    public bool IsCooldownActive()
    {
        return isCooldownActive;
    }
}
