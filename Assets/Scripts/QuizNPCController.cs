using UnityEngine;
using System.Collections;

public class QuizNPCController : MonoBehaviour, Interactable
{
<<<<<<< HEAD
    [Header("Quiz Logic")]
    [SerializeField] private QuizManager quizManager;

    [Header("UI")]
    [SerializeField] private GameObject quizLockedPanel; // 🔒 Shown if quiz is locked
=======
    [Header("Quiz UI Panel")]
    [SerializeField] private GameObject quizPanel;
>>>>>>> c24b1a07e585ccda977bb888e024ad6aeb0c6132

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    public Transform[] waypoints;

    private int currentWaypointIndex = 0;
    private bool isMoving = true;

    private Animator animator;
<<<<<<< HEAD
    private Vector2 previousPosition;
=======

    // Quiz key to track completion
    private string quizKey = "Quiz1Completed";
>>>>>>> c24b1a07e585ccda977bb888e024ad6aeb0c6132

    void Start()
    {
        animator = GetComponent<Animator>();
<<<<<<< HEAD
        previousPosition = transform.position;

=======

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
>>>>>>> c24b1a07e585ccda977bb888e024ad6aeb0c6132
        if (waypoints != null && waypoints.Length > 0)
        {
            StartCoroutine(MoveToNextWaypoint());
        }
        else
        {
<<<<<<< HEAD
            Debug.LogWarning("🚫 No waypoints assigned to Quiz NPC. Movement disabled.");
=======
            SetAnimation(Vector2.zero); // stay idle
>>>>>>> c24b1a07e585ccda977bb888e024ad6aeb0c6132
        }
    }

    IEnumerator MoveToNextWaypoint()
    {
        while (true)
        {
<<<<<<< HEAD
            if (!isMoving)
=======
            if (!isMoving || waypoints.Length == 0)
>>>>>>> c24b1a07e585ccda977bb888e024ad6aeb0c6132
            {
                SetAnimation(Vector2.zero);
                yield return null;
                continue;
            }

<<<<<<< HEAD
            if (waypoints.Length == 0) yield break;

            Transform targetWaypoint = waypoints[currentWaypointIndex];

            while (Vector2.Distance(transform.position, targetWaypoint.position) > 0.1f)
            {
                if (!isMoving) break;

                Vector2 direction = (targetWaypoint.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);
=======
            Transform target = waypoints[currentWaypointIndex];

            while (Vector2.Distance(transform.position, target.position) > 0.1f)
            {
                if (!isMoving) break;

                Vector2 direction = (target.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

>>>>>>> c24b1a07e585ccda977bb888e024ad6aeb0c6132
                SetAnimation(direction);
                yield return null;
            }

<<<<<<< HEAD
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            yield return new WaitForSeconds(1f);
=======
            SetAnimation(Vector2.zero);
            yield return new WaitForSeconds(1f);

            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
>>>>>>> c24b1a07e585ccda977bb888e024ad6aeb0c6132
        }
    }

    public void Interact()
    {
<<<<<<< HEAD
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
=======
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
>>>>>>> c24b1a07e585ccda977bb888e024ad6aeb0c6132
    }
}
