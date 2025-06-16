using UnityEngine;

public class HeadInteraction : MonoBehaviour
{
    [Header("References")]
    public GameObject interactionPrompt; // Assign your Headmarker icon here
    public QuizManager quizManager;      // Assign your QuizManager here

    private bool playerNearby = false;

    void Start()
    {
        // Hide icon at start
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("✅ Z pressed – starting quiz");

            if (interactionPrompt != null)
                interactionPrompt.SetActive(false);

            if (quizManager != null)
                quizManager.StartQuiz();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🔥 Trigger entered by: " + other.name);

            playerNearby = true;

            if (interactionPrompt != null)
                interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🚪 Trigger exited by: " + other.name);

            playerNearby = false;

            if (interactionPrompt != null)
                interactionPrompt.SetActive(false);
        }
    }
}
