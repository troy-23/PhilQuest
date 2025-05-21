using System;
using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private Text dialogText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image portraitImage;
    [SerializeField] private int lettersPerSecond = 20;

    public static DialogManager Instance { get; private set; }

    public event Action OnShowDialog;
    public event Action OnHideDialog;

    private Dialog dialog;
    private int currentLine = 0;
    private bool isTyping;
    private Coroutine typingCoroutine;
    public bool IsDialogActive { get; private set; } = false;

    private InputAction nextLineAction;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        nextLineAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/z");
        nextLineAction.performed += ctx => {
            if (IsDialogActive) OnDialogButtonPressed();
        };
    }

    private void OnEnable() => nextLineAction.Enable();
    private void OnDisable() => nextLineAction.Disable();

    public IEnumerator ShowDialog(Dialog dialog)
    {
        yield return new WaitForEndOfFrame();

        IsDialogActive = true;
        this.dialog = dialog;
        OnShowDialog?.Invoke();
        dialogBox.SetActive(true);

        currentLine = 0;
        typingCoroutine = StartCoroutine(TypeDialog(dialog.Lines[currentLine]));

        while (IsDialogActive)
            yield return null;

        // Cleanup after dialog ends
        HideDialogBox();
    }

    public void HideDialogBox()
    {
        dialogBox.SetActive(false);
        dialogText.text = "";
        nameText.text = "";
        nameText.gameObject.SetActive(false);

        if (portraitImage != null)
        {
            portraitImage.sprite = null;
            portraitImage.enabled = false;
        }
    }

    public void OnDialogButtonPressed()
    {
        if (!IsDialogActive) return;

        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogText.text = dialog.Lines[currentLine].text;
            isTyping = false;
        }
        else
        {
            currentLine++;
            if (currentLine < dialog.Lines.Count)
            {
                typingCoroutine = StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else
            {
                IsDialogActive = false;
                currentLine = 0;
                OnHideDialog?.Invoke();
                // 👇 Hide handled in ShowDialog's while loop
            }
        }
    }

    private IEnumerator TypeDialog(DialogLine line)
    {
        isTyping = true;
        dialogText.text = "";

        if (!string.IsNullOrWhiteSpace(line.speakerName))
        {
            nameText.text = line.speakerName;
            nameText.gameObject.SetActive(true);
        }
        else
        {
            nameText.gameObject.SetActive(false);
        }

        if (line.speakerPortrait != null)
        {
            portraitImage.sprite = line.speakerPortrait;
            portraitImage.enabled = true;
        }
        else
        {
            portraitImage.enabled = false;
        }

        foreach (char letter in line.text.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        isTyping = false;
    }
}
