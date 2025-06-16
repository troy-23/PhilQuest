using UnityEngine;
using System.Collections;

public class QuizNPCController : MonoBehaviour, Interactable
{
    [Header("Quiz Manager Reference")]
    [SerializeField] private QuizManager quizManager;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    public Transform[] waypoints;

    private int currentWaypointIndex = 0;
    private bool isMoving = true;

    private Animator animator;
    private string quizKey = "Quiz1Completed";

    void Start()
    {
        animator = GetComponent<Animator>();

        if (PlayerPrefs.GetInt(quizKey, 0) == 1)
        {
            isMoving = false;
        }

        if (waypoints != null && waypoints.Length > 0 && isMoving)
        {
            StartCoroutine(MoveToNextWaypoint());
        }
        else
        {
            SetAnimation(Vector2.zero);
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
        if (PlayerPrefs.GetInt(quizKey, 0) == 1)
        {
            Debug.Log("✅ Quiz already completed.");
            return;
        }

        Debug.Log("✅ Interacted with NPC");
        if (quizManager != null)
        {
            quizManager.StartQuiz();
        }
        else
        {
            Debug.LogWarning("⚠️ QuizManager not assigned.");
        }
    }

    private void SetAnimation(Vector2 direction)
    {
        if (animator == null) return;

        animator.SetBool("isWalking", direction != Vector2.zero);
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
    }
}
