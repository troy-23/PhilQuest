using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;
    [SerializeField] float moveSpeed = 2f;
    public Transform[] waypoints;

    private int currentWaypointIndex = 0;
    private bool isMoving = true;

    private Animator animator;
    private Vector2 previousPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        previousPosition = transform.position;
        StartCoroutine(MoveToNextWaypoint());
    }

    IEnumerator MoveToNextWaypoint()
    {
        while (true)
        {
            if (!isMoving)
            {
                SetAnimation(Vector2.zero); // Stay idle
                yield return null;
                continue;
            }

            Transform targetWaypoint = waypoints[currentWaypointIndex];

            while (Vector2.Distance(transform.position, targetWaypoint.position) > 0.1f)
            {
                if (!isMoving)
                {
                    SetAnimation(Vector2.zero);
                    break;
                }

                Vector2 direction = (targetWaypoint.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);

                SetAnimation(direction);

                yield return null;
            }

            SetAnimation(Vector2.zero); // Stop at waypoint
            yield return new WaitForSeconds(1f);
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    public void Interact()
    {
        StartCoroutine(HandleInteraction());
    }

    private IEnumerator HandleInteraction()
    {
        isMoving = false;

        yield return DialogManager.Instance.ShowDialog(dialog);

        while (DialogManager.Instance.IsDialogActive)
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
