using UnityEngine;

public class LoggedIn : MonoBehaviour
{
    public void OnBackButtonClicked()
    {
        Debug.Log("Back");
        Destroy(CurrentUserManager.Instance.gameObject);
        Utils.LoadScene("Scenes/Menu");
    }

    public void OnAddButtonClicked()
    {
        if (CurrentUserManager.Instance.Characters.Count < 4)
        {
            Utils.LoadScene("Scenes/CharacterCreation");
        }
        else
        {
            Utils.DisplayNotification("Four characters max");
        }
    }
}
