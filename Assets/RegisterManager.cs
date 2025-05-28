using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class RegisterManager : MonoBehaviour
{
    [Header("Input Fields")]
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField confirmPasswordInput;
    public TMP_Dropdown gradeDropdown;

    [Header("Message Display")]
    public TMP_Text messageText;

    [Header("Buttons")]
    public Button registerButton;
    public Button backToLoginButton;

    [Header("Panels")]
    public GameObject loginPanel;
    public GameObject registerPanel;

    private bool isRegistering = false;

    void Start()
    {
        ResetRegisterForm();
        registerButton.onClick.AddListener(OnRegisterClick);
        backToLoginButton.onClick.AddListener(SwitchToLogin);
    }

    public void OnRegisterClick()
    {
        if (isRegistering) return;
        isRegistering = true;

        messageText.text = "";

        string email = emailInput.text.Trim();
        string password = passwordInput.text;
        string confirmPassword = confirmPasswordInput.text;
        string grade = gradeDropdown.options[gradeDropdown.value].text;

        // --- Validation Checks ---
        if (string.IsNullOrEmpty(email))
        {
            ShowImmediateMessage("Please enter an email.");
            isRegistering = false;
            return;
        }

        if (email.Length < 5)
        {
            ShowImmediateMessage("Email must be at least 5 characters.");
            isRegistering = false;
            return;
        }

        if (string.IsNullOrEmpty(password))
        {
            ShowImmediateMessage("Please enter a password.");
            isRegistering = false;
            return;
        }

        if (password.Length < 5)
        {
            ShowImmediateMessage("Password must be at least 5 characters.");
            isRegistering = false;
            return;
        }

        if (string.IsNullOrEmpty(confirmPassword))
        {
            ShowImmediateMessage("Please confirm your password.");
            isRegistering = false;
            return;
        }

        if (confirmPassword.Length < 5)
        {
            ShowImmediateMessage("Confirm Password must be at least 5 characters.");
            isRegistering = false;
            return;
        }

        if (gradeDropdown.value == 0)
        {
            ShowImmediateMessage("Please select a grade level.");
            isRegistering = false;
            return;
        }

        if (password != confirmPassword)
        {
            ShowImmediateMessage("Passwords do not match.");
            isRegistering = false;
            return;
        }

        // --- All good, continue registration ---
        Debug.Log("Registered! Email: " + email + " | Grade: " + grade);
        StartCoroutine(SuccessRegister("Registration successful!", 2f));
    }

    private void ShowImmediateMessage(string message)
    {
        StopAllCoroutines(); // Cancel previous fades
        messageText.text = message;
        StartCoroutine(ClearMessageAfterDelay(2f));
    }

    private IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.text = "";
    }

    private IEnumerator SuccessRegister(string message, float delay)
    {
        messageText.text = message;
        yield return new WaitForSeconds(delay);

        registerPanel.SetActive(false);
        loginPanel.SetActive(true);
        ResetRegisterForm();
    }

    public void SwitchToLogin()
    {
        registerPanel.SetActive(false);
        loginPanel.SetActive(true);
    }

    public void ClearInputFields()
    {
        emailInput.text = "";
        passwordInput.text = "";
        confirmPasswordInput.text = "";
        gradeDropdown.value = 0;
    }

    public void ClearMessageText()
    {
        messageText.text = "";
    }

    public void ResetRegisterForm()
    {
        ClearInputFields();
        ClearMessageText();
        isRegistering = false;
    }
}
