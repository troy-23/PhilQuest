using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CongratsPanelManager : MonoBehaviour
{
    [Header("Congrats Panel UI")]
    public GameObject congratsPanel;
    public TMP_Text congratsTitle;
    public TMP_Text congratsText;
    public TMP_Text congratsDescription;
    public Image congratsImage;
    public Button closeButton;

    [Header("Reveal After Panel")]
    public GameObject magicBook;

    private void Start()
    {
        if (congratsPanel != null)
            congratsPanel.SetActive(false);

        if (closeButton != null)
            closeButton.onClick.AddListener(HideCongratsPanel);
    }

    // Overload for setting custom text (optional)
    public void ShowCongratsPanel(string title, string text, string description, Sprite image = null)
    {
        if (congratsPanel != null)
        {
            congratsPanel.SetActive(true);
            Debug.Log("✅ CongratsPanel shown with custom content.");
        }

        if (congratsTitle != null) congratsTitle.text = title;
        if (congratsText != null) congratsText.text = text;
        if (congratsDescription != null) congratsDescription.text = description;
        if (congratsImage != null && image != null) congratsImage.sprite = image;
    }

    // Basic show function — used by QuizManager
    public void ShowCongratsPanel()
    {
        if (congratsPanel != null)
        {
            congratsPanel.SetActive(true);
            Debug.Log("✅ CongratsPanel shown (default content).");
        }
    }

    public void HideCongratsPanel()
    {
        if (congratsPanel != null)
            congratsPanel.SetActive(false);

        if (magicBook != null)
            magicBook.SetActive(true);
    }
}
