using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDetailPanel : MonoBehaviour
{
    public static ItemDetailPanel Instance;

    [Header("UI References")]
    public GameObject panel;                        // Root panel object
    public Image itemImage;                         // Icon/Image of the item
    public TMP_Text itemNameText;                   // Name of the item
    public TMP_Text itemDescriptionText;            // Description of the item

    [Header("Optional Fade-in Effect")]
    public CanvasGroup canvasGroup;                 // Optional fade control

    void Awake()
    {
        Instance = this;
        Hide(); // Hide panel on start
    }

    /// <summary>
    /// Call this to show the detail panel with item data.
    /// </summary>
    public void ShowItem(Sprite sprite, string itemName, string description)
    {
        itemImage.sprite = sprite;
        itemNameText.text = itemName;
        itemDescriptionText.text = description;

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }

        panel.SetActive(true);
    }

    /// <summary>
    /// Hides the panel and optionally fades it out.
    /// </summary>
    public void Hide()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }

        panel.SetActive(false);
    }

    /// <summary>
    /// Called by the close button's OnClick.
    /// </summary>
    public void OnCloseButtonClicked()
    {
        Hide();
    }
}
