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
        // The prefab that holds the notification script is instanced without being parented to anything, so this will only destroy the notification related gameobjects
        Destroy(transform.root.gameObject);
    }

    public void SetNotification(string msg)
    {
        _notificationText.text = msg;
        _notification.gameObject.SetActive(true);
    }
}
