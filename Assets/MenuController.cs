using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas; // This should be your MenuPanel

    void Start()
    {
        menuCanvas.SetActive(false); // Hide menu when game starts
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // Optional for keyboard toggle
        {
            ToggleMenu();
        }
    }

    // ✅ Call this from your MenuButton (settings icon)
    public void ToggleMenu()
    {
        menuCanvas.SetActive(!menuCanvas.activeSelf);
    }
}
