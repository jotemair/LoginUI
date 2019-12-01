using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Ingredient = 0,
        Potion,
        Scroll,
        Food,
        Armour,
        Weapon,
        Craftable,
        Currency,
        Quest,
        Misc,
        Undefined,
        NUMBER_OF_ITEM_TYPES,
    };

    #region Private Variables

    private int _id = 0;

    private string _name = "";

    private string _description = "";

    private int _amount = 0;

    private int _value = 0;

    private Sprite _icon = null;

    private GameObject _mesh = null;

    private int _damage = 0;

    private int _armour = 0;

    private ItemType _type = ItemType.Misc;

    #endregion

    #region Public Properties

    public int ID
    {
        get { return _id; }
        set { _id = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public int Amount
    {
        get { return _amount; }
        set { _amount = value; }
    }

    public int Value
    {
        get { return _value; }
        set { _value = value; }
    }

    public Sprite Icon
    {
        get { return _icon; }
        set { _icon = value; }
    }

    public GameObject ItemMesh
    {
        get { return _mesh; }
        set { _mesh = value; }
    }

    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public int Armour
    {
        get { return _armour; }
        set { _armour = value; }
    }

    public ItemType Type
    {
        get { return _type; }
        set { _type = value; }
    }

    #endregion
}
