using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private InputField _username = null;

    [SerializeField]
    private InputField _password = null;

    private void OnEnable()
    {
        _username.text = "";
        _password.text = "";
    }

    public void OnLoginButtonClicked()
    {
        Debug.Log("Login, username: " + _username.text);
        UserLogin();
    }

    public void OnForgotAccountButtonClicked()
    {
        Debug.Log("Forgot");
        Utils.LoadMenu(MenuTypes.ForgotPassword);
    }

    public void OnCreateAccountButtonClicked()
    {
        Debug.Log("Create");
        Utils.LoadMenu(MenuTypes.AddAccount);
    }

    private void UserLogin()
    {
        const string userLoginUrl = "http://localhost/nsirpg/userlogin.php";

        Dictionary<string, string> fields = new Dictionary<string, string>();
        fields.Add("username", _username.text);
        fields.Add("password", _password.text);

        StartCoroutine(Utils.SendWebRequest(userLoginUrl, fields, UserLoginDone));
    }

    private void UserLoginDone(string response)
    {
        if (response.Equals("Success"))
        {
            Utils.LoadScene("Scenes/LoggedIn");
        }
        else
        {
            Utils.DisplayNotification(response);
        }
    }
}
