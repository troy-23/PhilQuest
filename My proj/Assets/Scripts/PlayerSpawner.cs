using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject joystickPrefab;

    void Awake()
    {
        // Only spawn if coming from the Library scene
        if (PlayerPrefs.GetInt("ComingFromLibrary", 0) == 1)
        {
            GameObject spawnPoint = GameObject.Find("PlayerSpawnPoint");
            if (spawnPoint != null)
            {
                GameObject player = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);

                GameObject canvas = new GameObject("UICanvas");
                canvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.AddComponent<CanvasScaler>();
                canvas.AddComponent<GraphicRaycaster>();
                GameObject joystick = Instantiate(joystickPrefab, canvas.transform);

                // Assign joystick
                PlayerController controller = player.GetComponent<PlayerController>();
                controller.joystick = joystick.GetComponent<Joystick>();
            }

            // Clear flag so it doesn't spawn again unexpectedly
            PlayerPrefs.SetInt("ComingFromLibrary", 0);
        }
    }
}
