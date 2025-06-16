using UnityEngine;

public class GameSession : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject canvasPrefab;
    public GameObject pauseMenuCanvasPrefab;

    private void Awake()
    {
        // ✅ Ensure GameObject is root before applying DontDestroyOnLoad
        if (transform.parent == null)
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log("✅ GameSession marked as DontDestroyOnLoad.");
        }
        else
        {
            Debug.LogWarning("❗ GameSession is not a root GameObject. DontDestroyOnLoad will be ignored.");
        }
    }

    private void Start()
    {
        // Example instantiations — ensure these prefabs are set in the Inspector
        if (playerPrefab != null && GameObject.FindWithTag("Player") == null)
        {
            Instantiate(playerPrefab);
        }

        if (canvasPrefab != null && GameObject.Find("UIRoot") == null)
        {
            Instantiate(canvasPrefab);
        }

        if (pauseMenuCanvasPrefab != null && GameObject.Find("PauseMenuCanvas") == null)
        {
            Instantiate(pauseMenuCanvasPrefab);
        }
    }
}
