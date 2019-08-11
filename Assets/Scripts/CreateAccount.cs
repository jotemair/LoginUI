using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateAccount : MonoBehaviour
{
    [SerializeField]
    private InputField _username = null;

    [SerializeField]
    private InputField _email = null;

    [SerializeField]
    private InputField _password = null;

    public void OnCreateButtonClicked()
    {
        Debug.Log("Create, username: " + _username.text + ", email: " + _email.text);
        Utils.LoadScene("Scenes/MainMenu");
    }

    public void OnBackButtonClicked()
    {
        Debug.Log("Back");
        Utils.LoadScene("Scenes/MainMenu");
    }
}
