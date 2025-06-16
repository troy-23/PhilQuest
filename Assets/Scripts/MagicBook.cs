using UnityEngine;
using System.Collections;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MagicBook : MonoBehaviour, Interactable
{
    [Header("Dialog and UI")]
    public Dialog bookDialog;
    public GameObject choicePanel;

    [Header("Cutscene Settings")]
    public VideoPlayer cutscenePlayer;
    public GameObject gameplayRoot;
    public AudioSource bgmSource;

    private string videoFileName = "new_cutscene_android.mp4";

    void Awake()
    {
        if (cutscenePlayer != null)
        {
            cutscenePlayer.errorReceived += OnVideoError;
        }
    }

    void Start()
    {
        // Hide this book if the quiz was not passed
        if (PlayerPrefs.GetInt("MapUnlocked", 0) == 0)
        {
            SetVisible(false);
        }
        else
        {
            SetVisible(true);
        }

        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);

#if UNITY_ANDROID
        videoPath = "file://" + videoPath;
#endif

        cutscenePlayer.source = VideoSource.Url;
        cutscenePlayer.url = videoPath;
    }

    public void Interact()
    {
        StartCoroutine(StartBookSequence());
    }

    private IEnumerator StartBookSequence()
    {
        if (bookDialog != null)
            yield return DialogManager.Instance.ShowDialog(bookDialog);

        if (choicePanel != null)
            choicePanel.SetActive(true);
    }

    public void OnAccept()
    {
        if (choicePanel != null)
            choicePanel.SetActive(false);

        StartCoroutine(PlayTransitionAnimation());
    }

    public void OnDecline()
    {
        if (choicePanel != null)
            choicePanel.SetActive(false);
    }

    private IEnumerator PlayTransitionAnimation()
    {
        DialogManager.Instance?.HideDialogBox();
        if (gameplayRoot != null) gameplayRoot.SetActive(false);
        if (bgmSource != null) bgmSource.Stop();

        if (cutscenePlayer != null)
        {
            cutscenePlayer.Prepare();
            while (!cutscenePlayer.isPrepared)
            {
                yield return null;
            }

            cutscenePlayer.Play();
            while (cutscenePlayer.isPlaying)
            {
                yield return null;
            }
        }

        ScreenFader fader = FindFirstObjectByType<ScreenFader>();
        if (fader != null)
            yield return fader.FadeIn(2f);

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("BattleOfMactanScene");
    }

    private void OnVideoError(VideoPlayer vp, string message)
    {
        Debug.LogError("❌ VideoPlayer Error: " + message);
    }

    public void SetVisible(bool visible)
    {
        this.gameObject.SetActive(visible);

        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = visible;

        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = visible;
    }
}
