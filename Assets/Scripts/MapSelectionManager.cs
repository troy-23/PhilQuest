using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectionManager : MonoBehaviour
{
    public void LoadLibrary()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadTabingDagat()
    {
        SceneManager.LoadScene("BattleOfMactanScene");
    }

    public void LoadPalengke()
    {
        Debug.Log("Palengke is not available yet.");
    }

    public void LoadGubatYantok()
    {
        Debug.Log("Gubat Yantok is still in development.");
    }
}
