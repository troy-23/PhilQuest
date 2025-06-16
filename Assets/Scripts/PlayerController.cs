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

    public Joystick joystick;

<<<<<<< HEAD
    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("❌ Rigidbody2D is missing on Player!");
        }
=======
    private void Awake()
    {
        animator = GetComponent<Animator>();
>>>>>>> c24b1a07e585ccda977bb888e024ad6aeb0c6132
    }

    void Update()
    {
        if (joystick == null)
        {
<<<<<<< HEAD
            joystick = FindFirstObjectByType<Joystick>();
=======
            joystick = GameObject.FindObjectOfType<Joystick>();
>>>>>>> c24b1a07e585ccda977bb888e024ad6aeb0c6132
            if (joystick == null)
            {
                Debug.LogWarning("⚠️ Joystick is not assigned to PlayerController.");
                return;
            }
        }

        HandleUpdate();
    }

    public void HandleUpdate()
    {
        if (!isMoving)
        {
            input = new Vector2(joystick.Horizontal, joystick.Vertical);

            // Snap to 4-directional input
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                input = new Vector2(Mathf.Sign(input.x), 0);
            else if (Mathf.Abs(input.y) > 0)
                input = new Vector2(0, Mathf.Sign(input.y));
            else
                input = Vector2.zero;

            if (input != Vector2.zero)
            {
                if (animator != null)
                {
                    if (animator.HasParameter("moveX")) animator.SetFloat("moveX", input.x);
                    if (animator.HasParameter("moveY")) animator.SetFloat("moveY", input.y);
                }

<<<<<<< HEAD
                Vector2 targetPos = rb.position + input;
=======
                var targetPos = transform.position + (Vector3)input;
>>>>>>> c24b1a07e585ccda977bb888e024ad6aeb0c6132

                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }

        if (animator != null && animator.HasParameter("isMoving"))
            animator.SetBool("isMoving", isMoving);
    }

<<<<<<< HEAD
    IEnumerator Move(Vector2 targetPos)
    {
        isMoving = true;

        while ((targetPos - rb.position).sqrMagnitude > Mathf.Epsilon)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, targetPos, moveSpeed * Time.fixedDeltaTime));
            yield return new WaitForFixedUpdate(); // Physics-timed movement
        }

        rb.MovePosition(targetPos);
        isMoving = false;
    }

    private bool IsWalkable(Vector2 targetPos)
=======
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
>>>>>>> c24b1a07e585ccda977bb888e024ad6aeb0c6132
    {
        return Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectLayer | interactableLayer) == null;
    }

    public void Interact()
    {
        if (animator == null) return;

<<<<<<< HEAD
        var facingDir = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = rb.position + facingDir;
=======
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;
>>>>>>> c24b1a07e585ccda977bb888e024ad6aeb0c6132
        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    public void OnInteractButtonPressed()
    {
        Interact();
    }
}

// Extension method to check if Animator has a parameter
public static class AnimatorExtensions
{
    public static bool HasParameter(this Animator animator, string paramName)
    {
        foreach (var param in animator.parameters)
        {
            if (param.name == paramName)
                return true;
        }
        return false;
    }
}
