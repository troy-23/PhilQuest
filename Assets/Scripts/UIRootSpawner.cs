using UnityEngine;

public class UIRootSpawner : MonoBehaviour
{
    public GameObject uiRootPrefab;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("UIRoot") == null)
        {
            Instantiate(uiRootPrefab);
        }
    }
}
