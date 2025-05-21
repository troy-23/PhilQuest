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

    // Re-encoded Android-friendly video
    private string videoFileName = "new_cutscene_android.mp4"; // ← ✅ Use the fixed/resized version

    void Awake()
    {
        if (cutscenePlayer != null)
        {
            cutscenePlayer.errorReceived += OnVideoError;
        }
    }

    void Start()
    {
        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);

#if UNITY_ANDROID
        videoPath = "file://" + videoPath;
#endif

        Debug.Log("📼 Video Path: " + videoPath); // ✅ Log for confirmation
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

            // ✅ Wait until video is prepared
            while (!cutscenePlayer.isPrepared)
            {
                yield return null;
            }

            cutscenePlayer.Play();

            // ✅ Wait until video finishes
            while (cutscenePlayer.isPlaying)
            {
                yield return null;
            }
        }

        // ✅ Optional: fade transition
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
}
