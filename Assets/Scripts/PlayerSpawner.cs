using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;

    void Awake()
    {
        if (PlayerPrefs.GetInt("ComingFromLibrary", 0) != 1) return;

        var spawnPoint = GameObject.Find("PlayerSpawnPoint");
        if (spawnPoint == null || GameObject.FindWithTag("Player") != null) return;

        // ✅ Spawn player
        var player = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);

        // ✅ Just find existing joystick (already part of UIRoot)
        var joystick = FindObjectOfType<Joystick>();

        // ✅ Assign joystick to PlayerController
        var controller = player.GetComponent<PlayerController>();
        if (controller != null && joystick != null)
            controller.joystick = joystick;

        // ✅ Reset flag
        PlayerPrefs.SetInt("ComingFromLibrary", 0);
    }
}
