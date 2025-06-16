using UnityEngine;

public class WelcomePanelUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject panel;

    [Header("Game Controls")]
    public GameObject fixedJoystick;
    public GameObject interactablePanel;

    void Start()
    {
        panel.SetActive(false);               // Do NOT auto-show on start
        fixedJoystick.SetActive(false);       // Hide Joystick
        interactablePanel.SetActive(false);   // Hide Interactable UI
    }

    public void ClosePanel()
    {
        panel.SetActive(false);               // Hide Welcome Panel
        fixedJoystick.SetActive(true);        // Show Joystick
        interactablePanel.SetActive(true);    // Show Interactable UI
    }
}
