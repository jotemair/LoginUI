using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region Private Variables

    [SerializeField]
    private InputField _username = null;

    [SerializeField]
    private InputField _password = null;

    #endregion

    #region MonoBehaviour Functions

    private void OnEnable()
    {
        _username.text = "";
        _password.text = "";
    }

    #endregion

    #region Public Functions

    public void OnLoginButtonClicked()
    {
        Debug.Log("Login, username: " + _username.text);
        UserLogin();
    }

    public void OnForgotAccountButtonClicked()
    {
        Debug.Log("Forgot");
        Utils.LoadMenu(MenuHandler.MenuTypes.ForgotPassword);
    }

    public void OnCreateAccountButtonClicked()
    {
        Debug.Log("Create");
        Utils.LoadMenu(MenuHandler.MenuTypes.AddAccount);
    }

    #endregion

    #region Private Functions

    private void UserLogin()
    {
        CurrentUserManager.Instance.User = _username.text;

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
            CurrentUserManager.Instance.LoadCharacters();
            Utils.LoadScene("Scenes/LoggedIn");
        }
        else
        {
            Utils.DisplayNotification(response);
        }
    }

    #endregion
}
