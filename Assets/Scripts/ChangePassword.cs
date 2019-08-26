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
            StartCoroutine(ChangePasword(_password.text));
        }
        else
        {
            Utils.DisplayNotification("Passwords do not match");
        }
    }

    private IEnumerator ChangePasword(string password)
    {
        string createUserUrl = "http://localhost/nsirpg/changepassword.php";
        WWWForm form = new WWWForm();

        form.AddField("username_Post", _username);
        form.AddField("password_Post", password);

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
}
