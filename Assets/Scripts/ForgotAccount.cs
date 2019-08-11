using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ForgotAccount : MonoBehaviour
{
    [SerializeField]
    private InputField _email = null;

    public void OnSendButtonClicked()
    {
        Debug.Log("Send clicked with email: " + _email.text);
        Utils.LoadScene("Scenes/ChangePassword");
    }

    public void OnBackButtonClicked()
    {
        Debug.Log("Back button clicked");
        Utils.LoadScene("Scenes/MainMenu");
    }
}
