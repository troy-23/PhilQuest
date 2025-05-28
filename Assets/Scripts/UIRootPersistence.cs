using UnityEngine;

public class UIRootPersistence : MonoBehaviour
{
    public static GameObject instance;

    void Awake()
    {
        // Ensure this GameObject is tagged correctly
        if (gameObject.tag != "UIRoot")
        {
            gameObject.tag = "UIRoot";
        }

        // Singleton pattern to prevent duplicates
        if (instance == null)
        {
            instance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != gameObject)
        {
            Destroy(gameObject); // destroy duplicate
        }
    }
}
