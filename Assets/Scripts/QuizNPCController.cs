using UnityEngine;
using System.Collections;

public class QuizNPCController : MonoBehaviour, Interactable
{
    [Header("Quiz UI Panel")]
    [SerializeField] private GameObject quizPanel;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    public Transform[] waypoints;

    private int currentWaypointIndex = 0;
    private bool isMoving = true;

    private Animator animator;

    // Quiz key to track completion
    private string quizKey = "Quiz1Completed";

    void Start()
    {
        animator = GetComponent<Animator>();

        // ✅ Disable interaction if quiz is already done
        if (PlayerPrefs.GetInt(quizKey, 0) == 1)
        {
            // Option 1: disable NPC completely
            // gameObject.SetActive(false);

            // Option 2: disable just movement + interaction
            isMoving = false;
            this.enabled = false;
            return;
        }

        // ✅ Start moving if allowed
        if (waypoints != null && waypoints.Length > 0)
        {
            StartCoroutine(MoveToNextWaypoint());
        }
        else
        {
            SetAnimation(Vector2.zero); // stay idle
        }
    }

    IEnumerator MoveToNextWaypoint()
    {
        while (true)
        {
            if (!isMoving || waypoints.Length == 0)
            {
                SetAnimation(Vector2.zero);
                yield return null;
                continue;
            }

            Transform target = waypoints[currentWaypointIndex];

            while (Vector2.Distance(transform.position, target.position) > 0.1f)
            {
                if (!isMoving) break;

                Vector2 direction = (target.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                SetAnimation(direction);
                yield return null;
            }

            SetAnimation(Vector2.zero);
            yield return new WaitForSeconds(1f);

            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    public void Interact()
    {
        // ✅ Prevent interaction if already completed
        if (PlayerPrefs.GetInt(quizKey, 0) == 1)
        {
            Debug.Log("✅ Quiz already completed. NPC won't trigger.");
            return;
        }

        StartCoroutine(HandleQuizInteraction());
    }

    private IEnumerator HandleQuizInteraction()
    {
        isMoving = false;

        if (quizPanel != null)
        {
            quizPanel.SetActive(true);
            Debug.Log("✅ QuizPanel opened");
        }
        else
        {
            Debug.LogWarning("⚠️ QuizPanel is not assigned.");
        }

        // Wait until quiz panel is closed
        while (quizPanel != null && quizPanel.activeSelf)
        {
            yield return null;
        }

        isMoving = true;
    }

    private void SetAnimation(Vector2 direction)
    {
        if (animator == null) return;

        animator.SetBool("isWalking", direction != Vector2.zero);
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
    }
}
