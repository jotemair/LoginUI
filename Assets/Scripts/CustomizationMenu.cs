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

    public void Start()
    {
        _char.UpdateLook();

        _hatToggle.onValueChanged.AddListener(HatToggleChanged);
        _hatToggle.isOn = _char.HasHat;

        _capeToggle.onValueChanged.AddListener(CapeToggleChanged);
        _capeToggle.isOn = _char.HasCape;
    }
}
