using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Linq;

public class ForgotAccount : MonoBehaviour
{
    [SerializeField]
    private InputField _email = null;

    public void OnSendButtonClicked()
    {
        Debug.Log("Send clicked with email: " + _email.text);

        SendEmail(_email.text, "<USER NAME HERE>");

        Utils.LoadScene("Scenes/ChangePassword");
    }

    public void OnBackButtonClicked()
    {
        Debug.Log("Back button clicked");
        Utils.LoadScene("Scenes/MainMenu");
    }

    public void SendEmail(string email, string username)
    {
        string code = GenerateCode();

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
}
