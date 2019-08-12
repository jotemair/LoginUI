using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static void LoadScene(string path)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(path);
    }

    public static void DisplayNotification(string msg)
    {
        GameObject notificationPrefab = Resources.Load<GameObject>("Prefabs/NotificationHolder");
        GameObject instance = Object.Instantiate(notificationPrefab);
        Notification notification = instance.GetComponentInChildren<Notification>();
        notification.SetNotification(msg);
    }
}
