using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[System.Serializable]
public class QuizGameData
{
    public string currentScene;
    public float playerX;
    public float playerY;
    public float playerZ;
    public int heartsRemaining;
    public List<string> completedQuizzes;
}

public class SaveSystem : MonoBehaviour
{
    [Header("References")]
    public Transform playerTransform; // Drag your student object here in Inspector

    [Header("Game Data")]
    public int heartsRemaining = 3;
    public List<string> completedQuizzes = new List<string>();

    private void Awake()
    {
        if (PlayerPrefs.HasKey("QuizGameSave"))
        {
            LoadGame(autoLoadScene: true);
        }
    }

    public void SaveGame()
    {
        QuizGameData data = new QuizGameData
        {
            currentScene = SceneManager.GetActiveScene().name,
            playerX = playerTransform.position.x,
            playerY = playerTransform.position.y,
            playerZ = playerTransform.position.z,
            heartsRemaining = heartsRemaining,
            completedQuizzes = completedQuizzes
        };

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("QuizGameSave", json);
        PlayerPrefs.Save();

        Debug.Log("✅ Game saved.");
    }

    public void LoadGame(bool autoLoadScene = false)
    {
        if (!PlayerPrefs.HasKey("QuizGameSave"))
        {
            Debug.LogWarning("⚠ No saved quiz data found.");
            return;
        }

        string json = PlayerPrefs.GetString("QuizGameSave");
        QuizGameData data = JsonUtility.FromJson<QuizGameData>(json);

        heartsRemaining = data.heartsRemaining;
        completedQuizzes = data.completedQuizzes;

        string currentScene = SceneManager.GetActiveScene().name;

        if (autoLoadScene && data.currentScene != currentScene)
        {
            PlayerPrefs.SetString("ResumeAfterLoad", "1");
            SceneManager.LoadScene(data.currentScene);
            return;
        }

        // ✅ Always restore state, even if scene is the same
        RestoreAfterSceneLoad(data);
    }

    private void RestoreAfterSceneLoad(QuizGameData data)
    {
        if (playerTransform != null)
        {
            playerTransform.position = new Vector3(data.playerX, data.playerY, data.playerZ);
            Debug.Log("📦 Restored player position.");
        }
        else
        {
            Debug.LogWarning("⚠ Player Transform is not assigned.");
        }

        Debug.Log($"❤️ Restored hearts: {heartsRemaining}, Quizzes: {completedQuizzes.Count}");
    }

    public void ResetSave()
    {
        PlayerPrefs.DeleteKey("QuizGameSave");
        PlayerPrefs.DeleteKey("ResumeAfterLoad");
        Debug.Log("🧹 Save data has been cleared.");
    }
}
