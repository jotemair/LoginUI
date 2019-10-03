using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// For sending email
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class ForgotAccount : MonoBehaviour
{
    #region Private Variables

    [SerializeField]
    private ChangePassword _resetMenu = null;

    [SerializeField]
    private InputField _email = null;

    #endregion

    #region MonoBehaviour Functions

    private void OnEnable()
    {
        _email.text = "";
    }

    #endregion

    #region Public Functions

    public void OnBackButtonClicked()
    {
        Debug.Log("Back button clicked");
        Utils.LoadMenu(MenuHandler.MenuTypes.Main);
    }

    public void OnSendButtonClicked()
    {
        // Since the email isn't passed to the function that processes the webrequest response, we disable interaction to make sure it's unchanged during the process
        _email.interactable = false;
        Debug.Log("Get username for email: " + _email.text);
        GetUsername();
        _email.interactable = true;
    }

    #endregion

    #region Private Functions

    // Helper function to generate a random code
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

    private void SendEmail(string email, string username)
    {
        // Generate new random code
        string code = GenerateCode();

        // Pass the username to the reset menu
        _resetMenu.Username = username;

        // Create the email
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("sqlunityclasssydney@gmail.com");
        mail.To.Add(email);
        mail.Subject = "NSI RPG Password Reset";
        mail.Body = "Hello " + username + "!\nReset using this code: " + code;

        // Connect to mail server
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 25;
        smtpServer.Credentials = new NetworkCredential("sqlunityclasssydney@gmail.com", "sqlpassword");
        smtpServer.EnableSsl = true;

        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors) { return true; };

        // Send Mail
        Debug.Log("Sending Email");
        smtpServer.Send(mail);

        // Load special notification for code verification
        GameObject inputHolderPrefab = Resources.Load<GameObject>("Prefabs/InputHolder");
        GameObject instance = Instantiate(inputHolderPrefab);
        InputNotificationHolder inputHolder = instance.GetComponentInChildren<InputNotificationHolder>();
        inputHolder.SetNotification(code);
    }

    #endregion
}
