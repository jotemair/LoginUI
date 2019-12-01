using UnityEngine;

public static class ItemData
{
    public static Item CreateItem(int itemID)
    {
        string _name = "";
        string _description = "";
        int _amount = 0;
        int _value = 0;
        string _icon = "";
        string _mesh = "";
        int _damage = 0;
        int _armour = 0;
        Item.ItemType _type = Item.ItemType.Misc;

        switch(itemID)
        {
            #region Ingredient 0 - 99
            case (0):
                _name = "Mushroom";
                _description = "A mushroom, if eaten will poison you, looks delicious.";
                _amount = 16;
                _value = 3;
                _icon = "Ingredients/m_10";
                _icon = "Ingredients/m_10";
                _damage = 100;
                _armour = 0;
                _type = Item.ItemType.Ingredient;
                break;
            case (1):
                _name = "Bark";
                _description = "A piece of treebark. The ents will have your head.";
                _amount = 16;
                _value = 5;
                _icon = "Ingredients/b_21";
                _mesh = "Ingredients/b_21";
                _damage = 10;
                _armour = 20;
                _type = Item.ItemType.Ingredient;
                break;
            case (2):
                _name = "Blueleaf";
                _description = "A mystic blue leaf from an enchanted tree. Brings good luck";
                _amount = 8;
                _value = 30;
                _icon = "Ingredients/g_27";
                _mesh = "Ingredients/g_27";
                _damage = 10;
                _armour = 20;
                _type = Item.ItemType.Ingredient;
                break;
            #endregion

            #region Potion 100 - 199
            case (100):
                _name = "Potion of shining";
                _description = "A yellow potion to make you glow in the dark. You're easier to spot and hit, but you are soo shiny!";
                _amount = 4;
                _value = 300;
                _icon = "Potions/30";
                _mesh = "Potions/30";
                _damage = 0;
                _armour = -30;
                _type = Item.ItemType.Potion;
                break;
            case (101):
                _name = "Potion of mud";
                _description = "Mud in a bottle. You can use it to make your clothes dirty! You should probably not drink this";
                _amount = 64;
                _value = 1;
                _icon = "Potions/24";
                _mesh = "Potions/24";
                _damage = 0;
                _armour = 0;
                _type = Item.ItemType.Potion;
                break;
            case (102):
                _name = "Potion of purple";
                _description = "You're not quite sure what this is, or what it does, but it sure smells nice";
                _amount = 8;
                _value = 100;
                _icon = "Potions/24";
                _mesh = "Potions/24";
                _damage = 0;
                _armour = 0;
                _type = Item.ItemType.Potion;
                break;
            #endregion

            #region Scroll 200 - 299
            case (200):
                _name = "Scroll of House";
                _description = "This scroll will give you a house. It's not magic, it's a fake ownership paper";
                _amount = 2;
                _value = 10000;
                _icon = "Scrolls/scroll_06_t";
                _mesh = "Scrolls/scroll_06_t";
                _damage = 0;
                _armour = 0;
                _type = Item.ItemType.Scroll;
                break;
            case (201):
                _name = "Scroll of Frost";
                _description = "This scroll will freeze everything in a 30 meter radius. Including the user";
                _amount = 4;
                _value = 200;
                _icon = "Scrolls/scroll_01_t";
                _mesh = "Scrolls/scroll_01_t";
                _damage = 0;
                _armour = 0;
                _type = Item.ItemType.Scroll;
                break;
            case (202):
                _name = "Scroll of scrolls";
                _description = "This scroll will summon more scroll of scrolls. It's use is banned all civilizations";
                _amount = 100000;
                _value = 0;
                _icon = "Scrolls/scroll_09_t";
                _mesh = "Scrolls/scroll_09_t";
                _damage = 0;
                _armour = 0;
                _type = Item.ItemType.Scroll;
                break;
            #endregion

            #region Food 300 - 399
            case (300):
                _name = "Cheese";
                _description = "The only method of healing in the land";
                _amount = 50;
                _value = 10;
                _icon = "Foods/cheese_01";
                _mesh = "Foods/cheese_01";
                _damage = 0;
                _armour = 0;
                _type = Item.ItemType.Food;
                break;
            case (301):
                _name = "Apple";
                _description = "Valuable throwing weapon against people who keep trying to tell you that cheese is not the answer to all medical issues";
                _amount = 20;
                _value = 60;
                _icon = "Foods/apple";
                _mesh = "Foods/apple";
                _damage = 0;
                _armour = 0;
                _type = Item.ItemType.Food;
                break;
            case (302):
                _name = "Bread";
                _description = "It's so fluffy it doubles as a pillow";
                _amount = 40;
                _value = 5;
                _icon = "Foods/baking_04";
                _mesh = "Foods/baking_04";
                _damage = 0;
                _armour = 0;
                _type = Item.ItemType.Food;
                break;
            #endregion

            #region Armour 400 - 499
            case (400):
                _name = "Rags";
                _description = "They don't protect much more than your dignity.";
                _amount = 5;
                _value = 2;
                _icon = "Armour/armor_19";
                _mesh = "Armour/armor_19";
                _damage = 0;
                _armour = 3;
                _type = Item.ItemType.Armour;
                break;
            case (401):
                _name = "Bracers";
                _description = "Keeps your arm warm";
                _amount = 3;
                _value = 20;
                _icon = "Armour/bracers_11";
                _mesh = "Armour/bracers_11";
                _damage = 0;
                _armour = 10;
                _type = Item.ItemType.Armour;
                break;
            case (402):
                _name = "Iron Helmet";
                _description = "There are a lt of holes on the visor. Is this made for spiders?";
                _amount = 3;
                _value = 50;
                _icon = "Armour/helmets_23";
                _mesh = "Armour/helmets_23";
                _damage = 0;
                _armour = 3;
                _type = Item.ItemType.Armour;
                break;
            #endregion

            #region Weapon 500 - 599
            case (500):
                _name = "Sword";
                _description = "A trusty sword, good for stabbing";
                _amount = 4;
                _value = 100;
                _icon = "Weapons/swords_t_04";
                _mesh = "Weapons/swords_t_04";
                _damage = 50;
                _armour = 6;
                _type = Item.ItemType.Weapon;
                break;
            case (501):
                _name = "Staff";
                _description = "A staff, good for magic tricks";
                _amount = 2;
                _value = 300;
                _icon = "Weapons/staff_t_03";
                _mesh = "Weapons/staff_t_03";
                _damage = 100;
                _armour = 0;
                _type = Item.ItemType.Weapon;
                break;
            case (502):
                _name = "Pointy stick";
                _description = "A stick with a pointy end. Just throw it at stuff I guess?";
                _amount = 1;
                _value = 666;
                _icon = "Weapons/ar_t_02";
                _mesh = "Weapons/ar_t_02";
                _damage = 666;
                _armour = 0;
                _type = Item.ItemType.Weapon;
                break;
            #endregion

            #region Craftable 600 -699
            case (600):
                _name = "Sticks";
                _description = "A bunch of sticks. Crafted from other sticks";
                _amount = 100;
                _value = 2;
                _icon = "Craftable/wd_t_05";
                _mesh = "Craftable/wd_t_05";
                _damage = 666;
                _armour = 0;
                _type = Item.ItemType.Craftable;
                break;
            case (601):
                _name = "Bigger stick";
                _description = "A bigger stick made from taping a lot of smaller sticks together";
                _amount = 50;
                _value = 20;
                _icon = "Craftable/wd_t_04";
                _mesh = "Craftable/wd_t_04";
                _damage = 0;
                _armour = 0;
                _type = Item.ItemType.Craftable;
                break;
            case (602):
                _name = "Biggest stick";
                _description = "The biggest stick, made from fusing bigger sticks in an arcane furnace";
                _amount = 10;
                _value = 300;
                _icon = "Craftable/wd_t_03";
                _mesh = "Craftable/wd_t_03";
                _damage = 0;
                _armour = 0;
                _type = Item.ItemType.Craftable;
                break;
            #endregion

            #region Currency 700 - 799
            case (700):
                _name = "Copper Pieces";
                _description = "Money made of copper, not worth much";
                _amount = 1000;
                _value = 1;
                _icon = "Currency/coins_t_02";
                _mesh = "Currency/coins_t_02";
                _damage = 0;
                _armour = 0;
                _type = Item.ItemType.Currency;
                break;
            case (701):
                _name = "Silver Pieces";
                _description = "Money made of silver, worth something";
                _amount = 500;
                _value = 100;
                _icon = "Currency/coins_t_01";
                _mesh = "Currency/coins_t_01";
                _damage = 0;
                _armour = 0;
                _type = Item.ItemType.Currency;
                break;
            case (702):
                _name = "Gold Pieces";
                _description = "Money made of gold, worth a lot";
                _amount = 100;
                _value = 1000;
                _icon = "Currency/coins_t_03";
                _mesh = "Currency/coins_t_03";
                _damage = 0;
                _armour = 0;
                _type = Item.ItemType.Currency;
                break;
            #endregion

            #region Quest 800 - 899
            case (800):
                _name = "Bone Die";
                _description = "Dice made from the bones of a God. Throw them to alter the fate of all reality. What could possibly go wrong?";
                _amount = 1;
                _value = 9999999;
                _icon = "Quest/dice_t_01";
                _mesh = "Quest/dice_t_01";
                _damage = 0;
                _armour = 0;
                _type = Item.ItemType.Quest;
                break;
            case (801):
                _name = "Demon Horn";
                _description = "Proof that you killed the demon. Now who's going to deliver the mail, huh?";
                _amount = 1;
                _value = 0;
                _icon = "Quest/demon_horn_t";
                _mesh = "Quest/demon_horn_t";
                _damage = 0;
                _armour = 0;
                _type = Item.ItemType.Quest;
                break;
            case (802):
                _name = "Book of Secrets";
                _description = "Book containing the recepie for the cookies the grandma down the road usually makes";
                _amount = 1;
                _value = 999999999;
                _icon = "Quest/book_t_03";
                _mesh = "Quest/book_t_03";
                _damage = 0;
                _armour = 0;
                _type = Item.ItemType.Quest;
                break;
            #endregion

            #region Misc 900 - 999
            case (900):
                _name = "Flute";
                _description = "A simple flute";
                _amount = 10;
                _value = 50;
                _icon = "Misc/mi_t_01";
                _mesh = "Misc/mi_t_01";
                _damage = 0;
                _armour = 0;
                _type = Item.ItemType.Misc;
                break;
            case (901):
                _name = "Lute";
                _description = "A simple lute";
                _amount = 10;
                _value = 50;
                _icon = "Misc/mi_t_03";
                _mesh = "Misc/mi_t_03";
                _damage = 0;
                _armour = 0;
                _type = Item.ItemType.Misc;
                break;
            case (902):
                _name = "Red Rock";
                _description = "A shiny red rock. Probably worthless but you like it because it's shinys";
                _amount = 100;
                _value = 0;
                _icon = "Misc/gm_t_03";
                _mesh = "Misc/gm_t_03";
                _damage = 0;
                _armour = 0;
                _type = Item.ItemType.Misc;
                break;
            #endregion

            default:
                itemID = 1000;
                _name = "Default Mushroom";
                _description = "A default mushroom";
                _amount = 5;
                _value = 0;
                _icon = "Ingredients/m_10";
                _mesh = "Ingredients/m_10";
                _damage = 0;
                _armour = 0;
                _type = Item.ItemType.Misc;
                break;
        }

        Item temp = new Item
        {
            ID = itemID,
            Name = _name,
            Description = _description,
            Amount = _amount,
            Value = _value,
            Icon = Resources.Load<Sprite>("Sprites/Icons/" + _icon),
            ItemMesh = Resources.Load<GameObject>("Meshes/Items/" + _mesh),
            Damage = _damage,
            Armour = _armour,
            Type = _type, 
        };

        return temp;
    }

    public static GameObject SpawnItem(Item item, Transform parent = null)
    {
        GameObject itemInstance = null;

        GameObject itemPrefab = item.ItemMesh;

        bool setImage = (null == itemPrefab);
        if (null == itemPrefab)
        {
            itemPrefab = Resources.Load<GameObject>("Meshes/Items/Ingredients/m_10");
        }

        if (null == parent)
        {
            itemInstance = Object.Instantiate<GameObject>(itemPrefab);
        }
        else
        {
            itemInstance = Object.Instantiate<GameObject>(itemPrefab, parent);
        }

        if (setImage)
        {
            itemInstance.GetComponentInChildren<SpriteRenderer>().sprite = item.Icon;
        }

        return itemInstance;
    }

    public static GameObject SpawnItem(Item item, Vector3 position, Quaternion rotation)
    {
        GameObject itemInstance = null;

        GameObject itemPrefab = item.ItemMesh;

        bool setImage = (null == itemPrefab);
        if (null == itemPrefab)
        {
            itemPrefab = Resources.Load<GameObject>("Meshes/Items/Ingredients/m_10");
        }

        itemInstance = Object.Instantiate<GameObject>(itemPrefab, position, rotation);

        if (setImage)
        {
            itemInstance.GetComponentInChildren<SpriteRenderer>().sprite = item.Icon;
        }

        return itemInstance;
    }
}
