using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private InputField _username = null;

    [SerializeField]
    private InputField _password = null;

    public void OnLoginButtonClicked()
    {
        Debug.Log("Login, username: " + _username.text);
        Utils.LoadScene("Scenes/LoggedIn");
    }

    public void OnForgotAccountButtonClicked()
    {
        Debug.Log("Forgot");
        Utils.LoadScene("Scenes/ForgotGetEmail");
    }

    public void OnCreateAccountButtonClicked()
    {
        Debug.Log("Create");
        Utils.LoadScene("Scenes/CreateAccount");
    }
}
