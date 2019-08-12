using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    [SerializeField]
    private Text _notificationText = null;

    [SerializeField]
    private GameObject _notification = null;

    public void ClearNotification()
    {
        _notificationText.text = "";
        _notification.gameObject.SetActive(false);
    }

    public void SetNotification(string msg)
    {
        Debug.Log("hi");
        _notificationText.text = msg;
        _notification.gameObject.SetActive(true);
    }
}
