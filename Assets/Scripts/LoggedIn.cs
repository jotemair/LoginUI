using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoggedIn : MonoBehaviour
{
    public void OnBackButtonClicked()
    {
        Debug.Log("Back");
        Utils.LoadScene("Scenes/Menu");
    }
}
