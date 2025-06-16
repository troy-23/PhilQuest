using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoginManager : MonoBehaviour
{
    [Header("Input Fields")]
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;

    [Header("Message Display")]
    public TMP_Text messageText;

    [Header("Panels")]
    public GameObject loginPanel;
    public GameObject registerPanel;

    private string allowedEmail = "user@test.com";
    private string allowedPassword = "1234";

    void Start()
    {
        messageText.text = "";
        emailInput.text = "";
        passwordInput.text = "";
    }

    // This method is wired to the Login button in the UI
    public void OnLoginClick()
    {
        string email = emailInput.text.Trim();
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            StartCoroutine(ShowMessage("Please enter email and password.", 2f, Color.red));
            return;
        }

        if (email == allowedEmail && password == allowedPassword)
        {
            StartCoroutine(HandleSuccessfulLogin("Login successful!", 1f));
        }
        else
        {
            StartCoroutine(ShowMessage("Invalid email or password.", 2f, Color.red));
        }
    }

    public void OnRegisterClick()
    {
        messageText.text = "";

        loginPanel.SetActive(false);
        registerPanel.SetActive(true);

        RegisterManager registerManager = FindObjectOfType<RegisterManager>();
        if (registerManager != null)
        {
            registerManager.ResetRegisterForm();
        }
    }

    private IEnumerator HandleSuccessfulLogin(string message, float delay)
    {
        messageText.text = message;
        messageText.color = Color.green;

        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator ShowMessage(string message, float delay, Color color)
    {
        messageText.text = message;
        messageText.color = color;

        yield return new WaitForSeconds(delay);
        messageText.text = "";
    }
}
