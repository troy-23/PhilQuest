using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject tutorialPanel;
    public GameObject welcomePanel;
    public GameObject healthPanel; // 🆕 Add this

    [Header("Controls")]
    public GameObject fixedJoystick;
    public GameObject interactButton;

    void Start()
    {
        tutorialPanel.SetActive(true);
        welcomePanel.SetActive(false);
        healthPanel.SetActive(false); // 🆕 Hide health bar during tutorial
        fixedJoystick.SetActive(false);
        interactButton.SetActive(false);
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
        welcomePanel.SetActive(true);
        healthPanel.SetActive(true); // 🆕 Show health bar after tutorial
        fixedJoystick.SetActive(true);
        interactButton.SetActive(true);
    }
}
