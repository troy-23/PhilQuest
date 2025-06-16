using UnityEngine;
using System.Collections;

public class NpcQuizTrigger : MonoBehaviour
{
    public GameObject quizPanel;
    public QuizManager quizManager;
    public GameObject quizLockedPanel; // ✅ Drag your UI panel here in Inspector

    private bool isPlayerNearby = false;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("🎯 Z key pressed - trying to open panel");

            if (quizManager != null && quizManager.IsCooldownActive())
            {
                Debug.Log("⛔ Quiz is locked. Please wait.");

                if (quizLockedPanel != null)
                {
                    quizLockedPanel.SetActive(true);
                    StartCoroutine(HideLockedPanelAfterSeconds(2f)); // auto-hide after 2 seconds
                }

                return;
            }

            if (quizPanel != null)
            {
                quizPanel.SetActive(true);
                Debug.Log("✅ QuizPanel activated!");
            }
            else
            {
                Debug.Log("❌ QuizPanel is not assigned.");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("🟢 Player entered NPC zone");

            if (PlayerStats.Instance != null)
            {
                PlayerStats.Instance.ResetHealth();
                Debug.Log("💖 Health restored upon NPC interaction.");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("🔴 Player left NPC zone");
        }
    }

    IEnumerator HideLockedPanelAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (quizLockedPanel != null)
        {
            quizLockedPanel.SetActive(false);
            Debug.Log("🔕 QuizLockedPanel hidden.");
        }
    }
}
