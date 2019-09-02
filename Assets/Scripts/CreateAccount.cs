﻿using System.Collections;
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

    public void OnCreateButtonClicked()
    {
        Debug.Log("Create, username: " + _username.text + ", email: " + _email.text);

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
            StartCoroutine(CreateUser(_username.text, _email.text, _password.text));
            Utils.DisplayNotification("Created account");
            // Utils.LoadScene("Scenes/MainMenu");
        }
    }

    public void OnBackButtonClicked()
    {
        Debug.Log("Back");
        Utils.LoadMenu(MenuTypes.Main);
    }

    IEnumerator CreateUser(string username, string email, string password)
    {
        string createUserUrl = "http://localhost/nsirpg/insertuser.php";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("email", email);
        form.AddField("password", password);


        UnityWebRequest webRequest = UnityWebRequest.Post(createUserUrl, form);

        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log("Sad " + webRequest.error);
        }
        else
        {
            string response = webRequest.downloadHandler.text;

            Debug.Log(response);
        }
    }
}
