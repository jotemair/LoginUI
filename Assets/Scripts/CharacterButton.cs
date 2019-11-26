using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    public int Code = 0;

    private string _name = "";

    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            _nameText.text = _name;
        }
    }

    [SerializeField]
    private Text _nameText = null;

    public void SelectButtonClicked()
    {
        CurrentUserManager.Instance.SelectedName = _name;
        Utils.LoadScene("Scenes/Game");
    }

    public void DeleteButtonClicked()
    {
        CurrentUserManager.Instance.DeleteCharacter(_name);
    }
}
