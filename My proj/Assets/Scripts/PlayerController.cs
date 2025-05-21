using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f;
    public bool isMoving;
    private Vector2 input;
    private Animator animator;

    public LayerMask solidObjectLayer;
    public LayerMask interactableLayer;

    public Joystick joystick; // Changed from FloatingJoystick to base Joystick class

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void HandleUpdate()
    {
        if (!isMoving)
        {
            // Use joystick input instead of keyboard
            input = new Vector2(joystick.Horizontal, joystick.Vertical);

            // Snap input to 4 directions only
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                input = new Vector2(Mathf.Sign(input.x), 0);
            else if (Mathf.Abs(input.y) > 0)
                input = new Vector2(0, Mathf.Sign(input.y));
            else
                input = Vector2.zero;

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }

        animator.SetBool("isMoving", isMoving);
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        return Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectLayer | interactableLayer) == null;
    }

    public void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;
        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }
    public void OnInteractButtonPressed() // ← Add this
    {
        Interact();
    }
    void Update()
    {
        if (joystick == null)
        {
            Debug.LogWarning("⚠️ Joystick is not assigned to PlayerController.");
            return;
        }

        HandleUpdate();
    }
}
