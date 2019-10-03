using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    #region Private Variables

    [SerializeField]
    private GameObject _main = null;

    [SerializeField]
    private GameObject _add = null;

    [SerializeField]
    private GameObject _reset = null;

    [SerializeField]
    private GameObject _forgot = null;

    #endregion

    #region MonoBehaviour Functions

    private void Start()
    {
        ChangeMenu(MenuTypes.Main);
    }

    #endregion

    #region Public Functions

    public enum MenuTypes
    {
        Main,
        AddAccount,
        PasswordReset,
        ForgotPassword,
    }

    public void ChangeMenu(MenuTypes menuType)
    {
        // When changing menus, just deactivate all and then activate the one specified
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

    #endregion

    #region Private Functions

    private void DisableAll()
    {
        _main.SetActive(false);
        _add.SetActive(false);
        _reset.SetActive(false);
        _forgot.SetActive(false);
    }

    #endregion
}
