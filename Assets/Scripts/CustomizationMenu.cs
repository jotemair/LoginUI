using UnityEngine;
using UnityEngine.UI;

public class CustomizationMenu : MonoBehaviour
{
    [SerializeField]
    private Character _char = null;

    [SerializeField]
    private Toggle _hatToggle = null;

    [SerializeField]
    private Toggle _capeToggle = null;

    [SerializeField]
    private Text _constitutionValueText = null;

    [SerializeField]
    private Text _intelligenceValueText = null;

    [SerializeField]
    private Text _enduranceValueText = null;

    [SerializeField]
    private Text _strengthValueText = null;

    [SerializeField]
    private Text _pointLeftValueText = null;

    private const int POINTS_TO_SPEND = 10;

    [SerializeField]
    private Text _healthValueText = null;

    [SerializeField]
    private Text _manaValueText = null;

    [SerializeField]
    private Text _staminaValueText = null;

    [SerializeField]
    private InputField _nameField = null;

    [SerializeField]
    private Text _nameText = null;

    [SerializeField]
    private bool _justDisplay = false;

    public bool HatToggle
    {
        get { return _hatToggle.isOn; }
        set { _hatToggle.isOn = value; }
    }

    public void HatToggleChanged(bool value)
    {
        _char.HasHat = value;
    }

    public bool CapeToggle
    {
        get { return _capeToggle.isOn; }
        set { _capeToggle.isOn = value; }
    }

    public void CapeToggleChanged(bool value)
    {
        _char.HasCape = value;
    }

    public void SetCharacterClass(int idx)
    {
        _char.CharacterClassValue = (Character.CharacterClass)(idx);
    }

    public void SetCharacterColor(int idx)
    {
        _char.CharacterColorValue = (Character.CharacterColor)(idx);
    }

    public void SetCharacterSize(int idx)
    {
        _char.CharacterSizeValue = (Character.CharacterSize)(idx);
    }

    public void DecreaseStat(int idx)
    {
        ChangeStat(idx, -1);
    }

    public void IncreaseStat(int idx)
    {
        if (HasPoints())
        {
            ChangeStat(idx, 1);
        }
    }

    private bool HasPoints()
    {
        return ((_char.Constitution + _char.Intelligence + _char.Endurance + _char.Strength) < (4 + POINTS_TO_SPEND));
    }

    private void ChangeStat(int idx, int change)
    {
        switch(idx)
        {
            case 0:
                _char.Constitution = _char.Constitution + change;
                _constitutionValueText.text = _char.Constitution.ToString();
                break;
            case 1:
                _char.Intelligence = _char.Intelligence + change;
                _intelligenceValueText.text = _char.Intelligence.ToString();
                break;
            case 2:
                _char.Endurance = _char.Endurance + change;
                _enduranceValueText.text = _char.Endurance.ToString();
                break;
            case 3:
                _char.Strength = _char.Strength + change;
                _strengthValueText.text = _char.Strength.ToString();
                break;
        }

        _pointLeftValueText.text = (4 - (_char.Constitution + _char.Intelligence + _char.Endurance + _char.Strength) + POINTS_TO_SPEND).ToString();
        _healthValueText.text = _char.MaxHealth.ToString();
        _manaValueText.text = _char.MaxMana.ToString();
        _staminaValueText.text = _char.MaxStamina.ToString();
    }

    public void UpdateName()
    {
        _char.Name = _nameField.text;
    }

    public void Save()
    {
        if ("" == _nameField.text)
        {
            Utils.DisplayNotification("Please enter a name");
        }
        else if (_nameField.text.Length > 15)
        {
            Utils.DisplayNotification("Name can be maximum 15 characters long");
        }
        else if (HasPoints())
        {
            Utils.DisplayNotification("Please spend initial stat points");
        }
        else
        {
            CurrentUserManager.Instance.AddCharacter(_char.Name, _char.SerializedCode);
            Utils.LoadScene("Scenes/LoggedIn");
        }
    }

    public void BackToLoggedIn()
    {
        Utils.LoadScene("Scenes/LoggedIn");
    }

    public void Start()
    {
        if (!_justDisplay)
        {
            _char.UpdateLook();

            _hatToggle.onValueChanged.AddListener(HatToggleChanged);
            _hatToggle.isOn = _char.HasHat;

            _capeToggle.onValueChanged.AddListener(CapeToggleChanged);
            _capeToggle.isOn = _char.HasCape;

            for (int i = 0; i < 4; ++i)
            {
                ChangeStat(i, 0);
            }
        }
        else
        {
            _char.SerializedCode = CurrentUserManager.Instance.SelectedCode;
            _char.Name = CurrentUserManager.Instance.SelectedName;

            for (int i = 0; i < 4; ++i)
            {
                ChangeStat(i, 0);
            }

            _nameText.text = _char.Name;
        }
    }
}
