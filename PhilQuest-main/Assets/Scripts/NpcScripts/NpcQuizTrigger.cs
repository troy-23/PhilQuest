using UnityEngine;

public class NpcQuizTrigger : MonoBehaviour
{
    public GameObject quizPanel;
    private bool isPlayerNearby = false;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("🎯 Z key pressed - trying to open panel");
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

            // ✅ Restore player health on interaction
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
}
