using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public Image fadePanel;

    public IEnumerator FadeIn(float duration)
    {
        fadePanel.gameObject.SetActive(true);

        Color c = fadePanel.color;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            c.a = Mathf.Lerp(0f, 1f, t / duration);
            fadePanel.color = c;
            yield return null;
        }

        c.a = 1f;
        fadePanel.color = c;
    }
}
