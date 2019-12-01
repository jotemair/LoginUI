using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentUserManager : MonoBehaviour
{
    public static CurrentUserManager Instance = null;

    // Name, Code
    private List<(string, int)> _characters = new List<(string, int)>();

    public List<(string, int)> Characters { get { return _characters; } }


    [SerializeField]
    private string _currentUser = "";

    public string User
    {
        get { return _currentUser; }
        set { _currentUser = value; }
    }

    private string _selectedName = "";

    public string SelectedName
    {
        get { return _selectedName; }
        set { _selectedName = value; }
    }

    public int SelectedCode
    {
        get
        {
            int code = 0;

            foreach (var character in _characters)
            {
                if (character.Item1.Equals(_selectedName))
                {
                    code = character.Item2;
                    break;
                }
            }

            return code;
        }
    }

    public void LoadCharacters()
    {
        const string characterLoadUrl = "http://localhost/nsirpg/loadcharacters.php";

        Dictionary<string, string> fields = new Dictionary<string, string>();
        fields.Add("username", _currentUser);

        StartCoroutine(Utils.SendWebRequest(characterLoadUrl, fields, LoadCharactersDone));
    }

    private void LoadCharactersDone(string response)
    {
        if (response.Contains("|") || (response.Equals("")))
        {
            _characters.Clear();

            int idx = 0;
            string name = "";
            foreach(var data in response.Split('|'))
            {
                if (idx % 2 == 0)
                {
                    name = data;
                }
                else
                {
                    AddCharacter(name, System.Int32.Parse(data), false);
                }
                idx = (idx + 1) % 2;
            }
        }
        else
        {
            Utils.DisplayNotification(response);
        }
    }

    public void SaveCharacters()
    {
        Debug.Log("Saving");
        const string characterSaveUrl = "http://localhost/nsirpg/savecharacters.php";

        Dictionary<string, string> fields = new Dictionary<string, string>();
        fields.Add("username", _currentUser);
        fields.Add("charNum", _characters.Count.ToString());

        for (int i = 0; i < _characters.Count; ++i)
        {
            fields.Add("charName_" + i.ToString(), _characters[i].Item1);
            fields.Add("charCode_" + i.ToString(), _characters[i].Item2.ToString());
        }

        StartCoroutine(Utils.SendWebRequest(characterSaveUrl, fields, SaveCharactersDone));
    }

    private void SaveCharactersDone(string response)
    {
        if (response.Equals("Success"))
        {
            //
        }
        else
        {
            Utils.DisplayNotification(response);
        }
    }

    public void AddCharacter(string name, int code, bool save = true)
    {
        _characters.Add((name, code));
        Debug.Log("Added character " + name);
        if (save)
        {
            SaveCharacters();
        }
    }

    public void DeleteCharacter(string name)
    {
        int idx = -1;

        for (int i = 0; i < _characters.Count; ++i)
        {
            if (_characters[i].Item1 == name)
            {
                idx = i;
                break;
            }
        }

        if (-1 != idx)
        {
            _characters.RemoveAt(idx);

            SaveCharacters();
        }
    }

    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
}
