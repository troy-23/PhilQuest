using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Image[] tabImages;
    public GameObject[] pages;

    public Color activeColor = Color.white;
    public Color inactiveColor = Color.grey;

    void Start()
    {
        ActivateTab(0); // Show first tab by default
    }

    // THIS is the method your buttons must call
    public void ActivateTab(int tabNo)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            bool isActive = (i == tabNo);
            pages[i].SetActive(isActive);
            tabImages[i].color = isActive ? activeColor : inactiveColor;
        }
    }
}
