using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [Header("Heart UI Images")]
    public Image[] hearts;             // Drag Heart1, Heart2, Heart3

    [Header("Heart Sprites")]
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Start()
    {
        if (PlayerStats.Instance != null)
        {
            PlayerStats.Instance.onHealthChangedCallback += UpdateHeartsHUD;
            UpdateHeartsHUD();
        }
        else
        {
            Debug.LogWarning("⚠️ PlayerStats.Instance is null. HealthBarManager will not update.");
        }
    }

    public void UpdateHeartsHUD()
    {
        if (PlayerStats.Instance == null) return;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < PlayerStats.Instance.currentHealth)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }
}
