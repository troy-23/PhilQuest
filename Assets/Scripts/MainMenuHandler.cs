using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;         // Canvas > Background
    public GameObject mapSelectionPanel;     // Canvas > MapSelectionPanel

    [Header("Locked Map Popup")]
    public GameObject lockedPopup;           // Assign: Map3 > LockedPopup

    private void Start()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);

        if (mapSelectionPanel != null)
            mapSelectionPanel.SetActive(false);

        if (lockedPopup != null)
            lockedPopup.SetActive(false); // hide locked message on start
    }

    // Button: Start Game
    public void StartGame()
    {
        PlayerPrefs.SetInt("HasGameSession", 1);
        SceneManager.LoadScene("SampleScene");
    }

    // Button: Resume Game
    public void ResumeGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Button: Load Specific Scenes from Map Buttons
    public void LoadSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadBattleOfMactanScene()
    {
        SceneManager.LoadScene("BattleOfMactanScene");
    }

    // Button: Locked Map (Intramuros)
    public void ShowLockedMapMessage()
    {
        if (lockedPopup != null)
        {
            lockedPopup.SetActive(true);
            CancelInvoke(nameof(HideLockedMapMessage)); // ensure reset
            Invoke(nameof(HideLockedMapMessage), 1.5f);    // auto-hide after 3 seconds
        }
    }

    private void HideLockedMapMessage()
    {
        if (lockedPopup != null)
            lockedPopup.SetActive(false);
    }

    // Button: Select Map
    public void ShowMapSelection()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);

        if (mapSelectionPanel != null)
            mapSelectionPanel.SetActive(true);
    }

    // Button: Back from Map Selection
    public void BackToMainMenu()
    {
        if (mapSelectionPanel != null)
            mapSelectionPanel.SetActive(false);

        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
    }

    // Button: Quit Game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit button clicked");
    }

    public void OpenSettings()
    {
        Debug.Log("Settings button clicked");
    }
}
