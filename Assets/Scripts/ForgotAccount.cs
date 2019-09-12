using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Linq;

public class ForgotAccount : MonoBehaviour
{
    [SerializeField]
    private ChangePassword _resetMenu = null;

    [SerializeField]
    private InputField _email = null;

    private void OnEnable()
    {
        _email.text = "";
    }

    public void OnBackButtonClicked()
    {
        Debug.Log("Back button clicked");
        Utils.LoadMenu(MenuTypes.Main);
    }

    public void SendEmail(string email, string username)
    {
        string code = GenerateCode();
        _resetMenu.Username = username;

        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("sqlunityclasssydney@gmail.com");
        mail.To.Add(email);
        mail.Subject = "NSI RPG Password Reset";
        mail.Body = "Hello " + username + "!\nReset using this code: " + code;

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 25;
        smtpServer.Credentials = new NetworkCredential("sqlunityclasssydney@gmail.com", "sqlpassword");
        smtpServer.EnableSsl = true;

        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors) { return true; };

        smtpServer.Send(mail);
        Debug.Log("Sending Email with code " + code);

        GameObject inputHolderPrefab = Resources.Load<GameObject>("Prefabs/InputHolder");
        GameObject instance = Instantiate(inputHolderPrefab);
        InputNotificationHolder inputHolder = instance.GetComponentInChildren<InputNotificationHolder>();
        inputHolder.SetNotification(code);
    }

    private string GenerateCode()
    {
        string randomCharList = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string randomCode = "";

        for(int i = 0; i < 6; ++i)
        {
            randomCode += randomCharList[UnityEngine.Random.Range(0, randomCharList.Length)];
        }

        return randomCode;
    }

    public void OnSendButtonClicked()
    {
        // Since the email isn't passed to the function that processes the webrequest response, we disable interaction to make sure it's unchanged during the process
        _email.interactable = false;
        Debug.Log("Get username for email: " + _email.text);
        GetUsername();
        _email.interactable = true;
    }

    private void GetUsername()
    {
        const string checkEmailUrl = "http://localhost/nsirpg/checkemail.php";

        Dictionary<string, string> fields = new Dictionary<string, string>();
        fields.Add("email_Post", _email.text);

        StartCoroutine(Utils.SendWebRequest(checkEmailUrl, fields, GetUsernameDone));
    }

    private void GetUsernameDone(string response)
    {
        if (response.Equals("Email does not exists"))
        {
            Utils.DisplayNotification(response);
        }
        else
        {
            SendEmail(_email.text, response);
        }
    }
}
