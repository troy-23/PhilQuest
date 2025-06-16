using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeInput : MonoBehaviour
{
    private EventSystem system;

    public Selectable firstInput;
    public Button submitButton;

    void Start()
    {
        system = EventSystem.current;

        if (firstInput != null)
        {
            firstInput.Select();
        }
        else
        {
            Debug.LogWarning("⚠️ First input field is not assigned.");
        }
    }

    void Update()
    {
        // Use old input system safely (make sure 'Both' input modes enabled)
        if (system == null || system.currentSelectedGameObject == null) return;

        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (previous != null)
                previous.Select();
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
                next.Select();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (submitButton != null)
            {
                submitButton.onClick.Invoke();
                Debug.Log("✔️ Enter key triggered the submit button.");
            }
            else
            {
                Debug.LogWarning("⚠️ Submit button is not assigned.");
            }
        }
    }
}
