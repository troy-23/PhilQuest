using UnityEngine;

public class ExclamationTrigger : MonoBehaviour
{
    public GameObject exclamationUI;

    void Start()


    {
        if (exclamationUI != null)
            exclamationUI.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger!");
            if (exclamationUI != null)
                exclamationUI.SetActive(true);
            else
                Debug.LogWarning("❌ Exclamation UI is not assigned.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger!");
            if (exclamationUI != null)
                exclamationUI.SetActive(false);
        }
    }


}
