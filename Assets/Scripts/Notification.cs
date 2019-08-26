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

        Destroy(transform.root.gameObject);
    }

    public void SetNotification(string msg)
    {
        _notificationText.text = msg;
        _notification.gameObject.SetActive(true);
    }
}
