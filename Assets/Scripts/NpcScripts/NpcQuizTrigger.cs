using UnityEngine;
<<<<<<< HEAD
using System.Collections;
=======
>>>>>>> c24b1a07e585ccda977bb888e024ad6aeb0c6132

public class NpcQuizTrigger : MonoBehaviour
{
    public GameObject quizPanel;
<<<<<<< HEAD
    public QuizManager quizManager;
    public GameObject quizLockedPanel; // ✅ Drag your UI panel here in Inspector

=======
>>>>>>> c24b1a07e585ccda977bb888e024ad6aeb0c6132
    private bool isPlayerNearby = false;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("🎯 Z key pressed - trying to open panel");
<<<<<<< HEAD

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

=======
>>>>>>> c24b1a07e585ccda977bb888e024ad6aeb0c6132
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

<<<<<<< HEAD
=======
            // ✅ Restore player health on interaction
>>>>>>> c24b1a07e585ccda977bb888e024ad6aeb0c6132
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
<<<<<<< HEAD

    IEnumerator HideLockedPanelAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (quizLockedPanel != null)
        {
            quizLockedPanel.SetActive(false);
            Debug.Log("🔕 QuizLockedPanel hidden.");
        }
    }
=======
>>>>>>> c24b1a07e585ccda977bb888e024ad6aeb0c6132
}
