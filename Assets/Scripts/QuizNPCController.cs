using UnityEngine;
using System.Collections;

public class QuizNPCController : MonoBehaviour, Interactable
{
    [Header("Quiz Logic")]
    [SerializeField] private QuizManager quizManager;

    [Header("UI")]
    [SerializeField] private GameObject quizLockedPanel; // 🔒 Shown if quiz is locked

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    public Transform[] waypoints;

    private int currentWaypointIndex = 0;
    private bool isMoving = true;

    private Animator animator;
    private Vector2 previousPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        previousPosition = transform.position;

        if (waypoints != null && waypoints.Length > 0)
        {
            StartCoroutine(MoveToNextWaypoint());
        }
        else
        {
            Debug.LogWarning("🚫 No waypoints assigned to Quiz NPC. Movement disabled.");
        }
    }

    IEnumerator MoveToNextWaypoint()
    {
        while (true)
        {
            if (!isMoving)
            {
                SetAnimation(Vector2.zero);
                yield return null;
                continue;
            }

            if (waypoints.Length == 0) yield break;

            Transform targetWaypoint = waypoints[currentWaypointIndex];

            while (Vector2.Distance(transform.position, targetWaypoint.position) > 0.1f)
            {
                if (!isMoving) break;

                Vector2 direction = (targetWaypoint.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);
                SetAnimation(direction);
                yield return null;
            }

            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            yield return new WaitForSeconds(1f);
        }
    }

    public void Interact()
    {
        Debug.Log("🎯 Player interacted with Quiz NPC");

        if (quizManager != null && quizManager.IsCooldownActive())
        {
            Debug.Log("⛔ Quiz is locked. Please wait.");

            if (quizLockedPanel != null)
            {
                quizLockedPanel.SetActive(true);
                StartCoroutine(HideLockedPanelAfterSeconds(2f));
            }

            return;
        }

        quizManager?.StartQuiz();
        Debug.Log("✅ Starting Quiz via QuizManager");
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

    void SetAnimation(Vector2 direction)
    {
        if (animator == null) return;

        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
        animator.SetBool("IsMoving", direction != Vector2.zero);
    }
}
