using UnityEngine;

public class Character : MonoBehaviour
{
    private string _name = "";

    public enum CharacterClass
    {
        Warrior = 0,
        Wizard,
        Wanderer,
        Count,
    }

    public enum CharacterColor
    {
        Red = 0,
        Green,
        Blue,
        Count,
    }

    public enum CharacterSize
    {
        Small = 0,
        Medium,
        Large,
        Count,
    }

    private const int MAX_STAT = 64;

    private CharacterClass _characterClass = CharacterClass.Warrior;
    private CharacterColor _characterColor = CharacterColor.Red;
    private CharacterSize _characterSize = CharacterSize.Medium;
    private bool _hasHat = false;
    private bool _hasCape = false;

    private int _constitution = 1;
    private int _intelligence = 1;
    private int _endurance = 1;
    private int _strength = 1;

    private int _maxHealth;
    private int _maxMana;
    private int _maxStamina;

    private GameObject _body = null;
    private GameObject _hat = null;
    private GameObject _cape = null;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public CharacterClass CharacterClassValue
    {
        get { return _characterClass; }
        set { SetCharacterClass(value); }
    }

    public CharacterColor CharacterColorValue
    {
        get { return _characterColor; }
        set { SetCharacterColor(value); }
    }

    public CharacterSize CharacterSizeValue
    {
        get { return _characterSize; }
        set { SetCharacterSize(value); }
    }

    public bool HasHat
    {
        get { return _hasHat; }
        set { SetHat(value); }
    }

    public bool HasCape
    {
        get { return _hasCape; }
        set { SetCape(value); }
    }

    public int Constitution
    {
        get { return _constitution; }
        set { _constitution = Mathf.Clamp(value, 1, MAX_STAT); UpdateSecondaryStats(); }
    }

    public int Intelligence
    {
        get { return _intelligence; }
        set { _intelligence = Mathf.Clamp(value, 1, MAX_STAT); UpdateSecondaryStats(); }
    }

    public int Endurance
    {
        get { return _endurance; }
        set { _endurance = Mathf.Clamp(value, 1, MAX_STAT); UpdateSecondaryStats(); }
    }

    public int Strength
    {
        get { return _strength; }
        set { _strength = Mathf.Clamp(value, 1, MAX_STAT); UpdateSecondaryStats(); }
    }

    public int MaxHealth
    {
        get { return _maxHealth; }
    }

    public int MaxMana
    {
        get { return _maxMana; }
    }

    public int MaxStamina
    {
        get { return _maxStamina; }
    }

    public int SerializedCode
    {
        get
        {
            int code = 0;
            code += (int)_characterClass;

            code *= (int)CharacterColor.Count;
            code += (int)_characterColor;

            code *= (int)CharacterSize.Count;
            code += (int)_characterSize;

            code *= 2;
            code += (_hasHat ? 1 : 0);

            code *= 2;
            code += (_hasCape ? 1 : 0);

            code *= MAX_STAT;
            code += _constitution;

            code *= MAX_STAT;
            code += _intelligence;

            code *= MAX_STAT;
            code += _endurance;

            code *= MAX_STAT;
            code += _strength;

            return code;
        }

        set
        {
            int inputCode = value;
            _strength = inputCode % MAX_STAT;
            inputCode = inputCode / MAX_STAT;

            _endurance = inputCode % MAX_STAT;
            inputCode = inputCode / MAX_STAT;

            _intelligence = inputCode % MAX_STAT;
            inputCode = inputCode / MAX_STAT;

            _constitution = inputCode % MAX_STAT;
            inputCode = inputCode / MAX_STAT;

            _hasCape = (inputCode % 2 == 1);
            inputCode = inputCode / 2;

            _hasHat = (inputCode % 2 == 1);
            inputCode = inputCode / 2;

            _characterSize = (CharacterSize)(inputCode % (int)(CharacterSize.Count));
            inputCode = inputCode / (int)(CharacterSize.Count);

            _characterColor = (CharacterColor)(inputCode % (int)(CharacterColor.Count));
            inputCode = inputCode / (int)(CharacterColor.Count);

            _characterClass = (CharacterClass)(inputCode);

            UpdateLook();
        }
    }

    public void UpdateLook()
    {
        SetCharacterClass(_characterClass);
        SetCharacterColor(_characterColor);
        SetCharacterSize(_characterSize);
        SetHat(_hasHat);
        SetCape(_hasCape);
    }

    private void SetCharacterClass(CharacterClass characterClass)
    {
        _characterClass = characterClass;

        if (null != _body)
        {
            Destroy(_body);
            _body = null;
        }

        GameObject bodyPrefab = Resources.Load<GameObject>("Prefabs/" + _characterClass.ToString());
        _body = Instantiate<GameObject>(bodyPrefab, transform);

        SetCharacterColor(_characterColor);
        SetCharacterSize(_characterSize);
    }

    private void SetCharacterColor(CharacterColor characterColor)
    {
        _characterColor = characterColor;

        Material mat = Resources.Load<Material>("Materials/" + _characterColor.ToString());

        foreach (var renderer in _body.GetComponentsInChildren<Renderer>())
        {
            renderer.material = mat;
        }
    }

    private void SetCharacterSize(CharacterSize size)
    {
        _characterSize = size;

        transform.localScale = new Vector3(1f, 1f, 1f) * (((float)(_characterSize) + 1f) / 2f);
    }

    private void SetHat(bool hasHat)
    {
        _hasHat = hasHat;
        if ((_hasHat) && (null == _hat))
        {
            GameObject hatPrefab = Resources.Load<GameObject>("Prefabs/Hat");
            _hat = Instantiate<GameObject>(hatPrefab, transform);
        }
        else if ((!_hasHat) && (null != _hat))
        {
            Destroy(_hat);
            _hat = null;
        }
    }

    private void SetCape(bool hasCape)
    {
        _hasCape = hasCape;
        if ((_hasCape) && (null == _cape))
        {
            GameObject capePrefab = Resources.Load<GameObject>("Prefabs/Cape");
            _cape = Instantiate<GameObject>(capePrefab, transform);
        }
        else if ((!_hasCape) && (null != _cape))
        {
            Destroy(_cape);
            _cape = null;
        }
    }

    private void UpdateSecondaryStats()
    {
        _maxHealth = _constitution * 2 + _endurance / 2 + _strength / 2 + 50;
        _maxMana = _intelligence * 3 + _endurance / 2 + 10;
        _maxStamina = _endurance * 2 + _strength / 2 + _constitution / 2 + 30;
    }
}
