using UnityEngine;

public class LoggedIn : MonoBehaviour
{
    public void OnBackButtonClicked()
    {
        Debug.Log("Back");
        Utils.LoadScene("Scenes/Menu");
    }
}
