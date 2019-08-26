using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuTypes
{
    Main,
    AddAccount,
    PasswordReset,
    ForgotPassword,
}

public class MenuHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _main = null;

    [SerializeField]
    private GameObject _add = null;

    [SerializeField]
    private GameObject _reset = null;

    [SerializeField]
    private GameObject _forgot = null;

    public void ChangeMenu(MenuTypes menuType)
    {
        DisableAll();

        switch (menuType)
        {
            case (MenuTypes.Main):
                _main.SetActive(true);
                break;
            case (MenuTypes.AddAccount):
                _add.SetActive(true);
                break;
            case (MenuTypes.PasswordReset):
                _reset.SetActive(true);
                break;
            case (MenuTypes.ForgotPassword):
                _forgot.SetActive(true);
                break;
        }
    }

    private void DisableAll()
    {
        _main.SetActive(false);
        _add.SetActive(false);
        _reset.SetActive(false);
        _forgot.SetActive(false);
    }

    public void Start()
    {
        ChangeMenu(MenuTypes.Main);
    }
}
