using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ChangePassword : MonoBehaviour
{
    [SerializeField]
    private InputField _password = null;

    [SerializeField]
    private InputField _confirm = null;

    private string _username = "";

    public string Username
    {
        set { _username = value; }
    }

    private void OnEnable()
    {
        _password.text = "";
        _confirm.text = "";
    }

    public void OnBackButtonClicked()
    {
        Debug.Log("Back button clicked");
        Utils.LoadMenu(MenuTypes.Main);
    }

    public void OnResetButtonClicked()
    {
        Debug.Log("Reset button clicked for user " + _username);

        if (_password.text.Equals(_confirm.text))
        {
            ChangePasswordStart();
        }
        else
        {
            Utils.DisplayNotification("Passwords do not match");
        }
    }

    private void ChangePasswordStart()
    {
        const string changePasswordUrl = "http://localhost/nsirpg/changepassword.php";

        Dictionary<string, string> fields = new Dictionary<string, string>();
        fields.Add("username_Post", _username);
        fields.Add("password_Post", _password.text);

        StartCoroutine(Utils.SendWebRequest(changePasswordUrl, fields, ChangePasswordDone));
    }

    private void ChangePasswordDone(string response)
    {
        if (response.Equals("Password Changed"))
        {
            Utils.LoadMenu(MenuTypes.Main);
            Utils.DisplayNotification(response);
        }
        else
        {
            Utils.DisplayNotification("Password reset failed:\n" + response);
        }
    }
}
