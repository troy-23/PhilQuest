using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    public void OnInteractOrAdvance()
    {
        if (DialogManager.Instance.IsDialogActive)
        {
            DialogManager.Instance.OnDialogButtonPressed();
        }
        else
        {
            playerController.OnInteractButtonPressed();
        }
    }
}
