using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNotificationHolder : MonoBehaviour
{
    [SerializeField]
    private Text _notificationText = null;

    [SerializeField]
    private GameObject _notification = null;

    private string _code = "";

    public void ClearNotification(InputField codeCheck)
    {
        _notificationText.text = "";

        Destroy(transform.root.gameObject);

        if (codeCheck.text.Equals(_code))
        {
            Utils.LoadMenu(MenuTypes.PasswordReset);
        }
        else
        {
            Utils.DisplayNotification("Incorrect verification code, resend mail and try again");
        }
    }

    public void SetNotification(string code)
    {
        _code = code;
        _notificationText.text = "Please enter verification code sent by email";
        _notification.gameObject.SetActive(true);
    }
}
