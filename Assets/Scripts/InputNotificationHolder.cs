using UnityEngine;
using UnityEngine.UI;

public class InputNotificationHolder : MonoBehaviour
{
    #region Private Variables

    [SerializeField]
    private Text _notificationText = null;

    [SerializeField]
    private GameObject _notification = null;

    private string _code = "";

    #endregion

    #region Public Functions

    // Function connected to the OK button
    public void ClearNotification(InputField codeCheck)
    {
        _notificationText.text = "";

        Destroy(transform.root.gameObject);

        // Check if the verification code is correct, and respond accordingly
        if (codeCheck.text.Equals(_code))
        {
            Utils.LoadMenu(MenuHandler.MenuTypes.PasswordReset);
        }
        else
        {
            Utils.DisplayNotification("Incorrect verification code, resend mail and try again");
        }
    }

    // The setup for this panel does not use the parameter as the message, rather it uses it as the code to verify
    public void SetNotification(string code)
    {
        _code = code;
        _notificationText.text = "Please enter verification code sent by email";
        _notification.gameObject.SetActive(true);
    }

    #endregion
}
