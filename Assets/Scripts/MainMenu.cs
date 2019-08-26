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

    public void OnLoginButtonClicked()
    {
        Debug.Log("Login, username: " + _username.text);
        StartCoroutine(UserLogin(_username.text, _password.text));
        //Utils.LoadScene("Scenes/LoggedIn");
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

    IEnumerator UserLogin(string username, string password)
    {
        string createUserUrl = "http://localhost/nsirpg/userlogin.php";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
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
}
