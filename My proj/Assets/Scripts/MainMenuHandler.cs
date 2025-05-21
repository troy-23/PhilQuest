using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    private void Start()
    {
        // Removed resumeButton logic since it is not used
    }

    // START Button
    public void StartGame()
    {
        PlayerPrefs.SetInt("HasGameSession", 1);  // ✅ remember that a game session exists
        SceneManager.LoadScene("SampleScene");
    }

    // RESUME Button
    public void ResumeGame()
    {
        SceneManager.LoadScene("SampleScene");  // 👈 go back to the game scene
    }

    public void OpenSettings()
    {
        Debug.Log("Settings button clicked");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit button clicked");
    }
}
