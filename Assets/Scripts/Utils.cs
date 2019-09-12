using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Utils
{
    // A simple function to make menu change calls cleaner
    public static void LoadMenu(MenuTypes menuType)
    {
        GameObject.Find("MenuHandler").GetComponent<MenuHandler>().ChangeMenu(menuType);
    }

    // A simple function to make scene load calls cleaner
    public static void LoadScene(string path)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(path);
    }

    // A function to create a notification popup
    public static void DisplayNotification(string msg)
    {
        // Instantiate a notification prefab that's already set up with an ok button that closes and deletes it when pressed, then set the notification message
        GameObject notificationPrefab = Resources.Load<GameObject>("Prefabs/NotificationHolder");
        GameObject instance = Object.Instantiate(notificationPrefab);
        Notification notification = instance.GetComponentInChildren<Notification>();
        notification.SetNotification(msg);
    }

    // A helper function to send web requests
    public static IEnumerator SendWebRequest(string page, Dictionary<string, string> fields, System.Action<string> doneAction)
    {
        WWWForm form = new WWWForm();

        // Fills in the webrequest form data with the information in the fields dictionary parameter
        foreach (var field in fields)
        {
            form.AddField(field.Key, field.Value);
        }

        // Send the request to the given page
        UnityWebRequest webRequest = UnityWebRequest.Post(page, form);

        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            // Display the error message is there's a network issue
            Debug.Log("Network error: " + webRequest.error);
            Utils.DisplayNotification(webRequest.error);
        }
        else
        {
            string response = webRequest.downloadHandler.text;
            Debug.Log(response);

            // If the request returns with a response, have the doneAction function given as a parameter process it
            doneAction(response);
        }
    }
}
