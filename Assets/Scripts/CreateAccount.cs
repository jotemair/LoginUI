using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CreateAccount : MonoBehaviour
{
    [SerializeField]
    private InputField _username = null;

    [SerializeField]
    private InputField _email = null;

    [SerializeField]
    private InputField _password = null;

    [SerializeField]
    private InputField _confirm = null;

    private void OnEnable()
    {
        _username.text = "";
        _email.text = "";
        _password.text = "";
        _confirm.text = "";
    }

    public void OnCreateButtonClicked()
    {
        Debug.Log("Create, username: " + _username.text + ", email: " + _email.text);

        // Before trying to create a new user, make some basic checks on the input
        if (_username.text.Length > 30)
        {
            Utils.DisplayNotification("Username can be maximum 30 characters long.");
        }
        else if (("").Equals(_username.text))
        {
            Utils.DisplayNotification("Username must be at least 1 character long. I'm not asking for much am I?");
        }
        else if (("").Equals(_email.text))
        {
            Utils.DisplayNotification("I may not be checking if that's a valid email. But THAT is not a valid email.");
        }
        else if (_email.text.Length > 90)
        {
            Utils.DisplayNotification("Realy? Please use an email that's less than 90 characters long");
        }
        else if (_password.text.Length > 50)
        {
            Utils.DisplayNotification("Password can't be more than 50 characters long.");
        }
        else if (!_password.text.Equals(_confirm.text))
        {
            Utils.DisplayNotification("Password and confirmation do not match. make sure you type the same password in twice.");
        }
        else
        {
            CreateUser();
        }
    }

    public void OnBackButtonClicked()
    {
        Debug.Log("Back");
        Utils.LoadMenu(MenuTypes.Main);
    }

    private void CreateUser()
    {
        const string createUserUrl = "http://localhost/nsirpg/insertuser.php";

        Dictionary<string, string> fields = new Dictionary<string, string>();
        fields.Add("username", _username.text);
        fields.Add("email", _email.text);
        fields.Add("password", _password.text);

        StartCoroutine(Utils.SendWebRequest(createUserUrl, fields, CreateUserDone));
    }

    private void CreateUserDone(string response)
    {
        if (response.Equals("Success"))
        {
            Utils.LoadMenu(MenuTypes.Main);
            Utils.DisplayNotification("Created account");
        }
        else
        {
            Utils.DisplayNotification("Failed to create account:\n" + response);
        }
    }
}
