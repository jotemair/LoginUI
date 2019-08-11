using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePassword : MonoBehaviour
{
    [SerializeField]
    private InputField _password = null;

    [SerializeField]
    private InputField _confirm = null;

    public void OnBackButtonClicked()
    {
        Debug.Log("Back button clicked");
        Utils.LoadScene("Scenes/MainMenu");
    }

    public void OnResetButtonClicked()
    {
        Debug.Log("Reset button clicked");
        Utils.LoadScene("Scenes/MainMenu");
    }
}
