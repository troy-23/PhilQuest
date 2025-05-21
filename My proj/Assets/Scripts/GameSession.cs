using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject joystickPrefab;
    public GameObject interactButtonPrefab;
    public GameObject pauseMenuCanvasPrefab;
    public GameObject settingsButtonPrefab;

    private static GameSession instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("🔄 Scene Loaded: " + scene.name);
        TrySpawnPlayerAndUI();
    }

    void TrySpawnPlayerAndUI()
    {
        if (GameObject.FindWithTag("Player") != null)
        {
            Debug.Log("✅ Player already exists, skipping spawn.");
            return;
        }

        GameObject spawnPoint = GameObject.Find("PlayerSpawnPoint");
        if (spawnPoint == null)
        {
            Debug.LogWarning("❌ Could not find PlayerSpawnPoint in scene!");
            return;
        }

        GameObject player = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
        player.name = "Player";
        player.tag = "Player";

        // Assign camera follow
        CameraFollow cameraFollow = Camera.main?.GetComponent<CameraFollow>();
        if (cameraFollow != null)
        {
            cameraFollow.target = player.transform;
        }

        // Create UI Canvas
        GameObject canvas = new GameObject("UICanvas");
        Canvas canvasComponent = canvas.AddComponent<Canvas>();
        canvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();
        DontDestroyOnLoad(canvas);

        // Spawn Joystick
        GameObject joystick = Instantiate(joystickPrefab, canvas.transform);
        joystick.name = "Joystick";

        // 🔍 Resize joystick only in BattleOfMactanScene
        if (SceneManager.GetActiveScene().name == "BattleOfMactanScene")
        {
            RectTransform rt = joystick.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.sizeDelta *= 1.5f; // Resize (you can adjust the multiplier)
                rt.localScale = Vector3.one; // Ensure scale is consistent
                Debug.Log("📏 Joystick resized for BattleOfMactanScene.");
            }
        }

        // Spawn Interact Button
        if (interactButtonPrefab != null)
        {
            GameObject interactButton = Instantiate(interactButtonPrefab, canvas.transform);
            interactButton.name = "InteractButton";

            var button = interactButton.GetComponent<Button>();
            if (button != null)
            {
                var controller = player.GetComponent<PlayerController>();
                if (controller != null)
                {
                    button.onClick.AddListener(controller.OnInteractButtonPressed);
                }
            }
        }

        // Spawn Settings Button
        if (settingsButtonPrefab != null)
        {
            GameObject settingsBtn = Instantiate(settingsButtonPrefab, canvas.transform);
            settingsBtn.name = "SettingsButton";
        }

        // Spawn Pause Menu Canvas
        if (pauseMenuCanvasPrefab != null)
        {
            GameObject pauseCanvas = Instantiate(pauseMenuCanvasPrefab, canvas.transform);
            pauseCanvas.name = "PauseMenuCanvas";
        }

        // Assign joystick to PlayerController
        var pc = player.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.joystick = joystick.GetComponent<Joystick>();
        }

        Debug.Log("✅ Player, Joystick, Interact Button, SettingsButton, and PauseMenuCanvas spawned.");
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
