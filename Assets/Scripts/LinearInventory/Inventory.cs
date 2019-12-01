using System.Collections.Generic;
using UnityEngine;

namespace LinearInventory
{

    public class Inventory : MonoBehaviour
    {
        public GUISkin invSkin = null;
        public GUIStyle boxStyle = null;

        #region Variables

        public List<Item> inv = new List<Item>();

        public List<Item> filteredInv = new List<Item>();

        public Item selectedItem = null;

        public bool showInv = false;

        public Vector2 scr = Vector2.zero;

        public Vector2 scrollPos = Vector2.zero;

        public int money;

        public Transform dropLocation = null;

        [System.Serializable]
        public struct Equipment
        {
            public string name;
            public Transform location;
            public GameObject curItemObj;
            public Item curItem;
        };

        public Equipment[] equipmentSlots = null;

        private enum EquipSlots
        {
            Weapon = 0,
            Armour = 1,
        }

        private Item.ItemType filterType = Item.ItemType.Undefined;

        #endregion

        #region MonoBehaviour Functions

        private void Start()
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            for (int i = 0; i < 5; ++i)
            {
                inv.Add(ItemData.CreateItem(i));
            }
            inv.Add(ItemData.CreateItem(402));
            inv.Add(ItemData.CreateItem(502));
            inv.Add(ItemData.CreateItem(401));
            inv.Add(ItemData.CreateItem(400));

            filteredInv = new List<Item>(inv);
        }

        private void Update()
        {
            // showInv = showInv != Input.GetKeyDown(KeyCode.Tab);

            if (Input.GetKey(KeyCode.Q))
            {
                inv.Add(ItemData.CreateItem(Random.Range(0, 11) * 100 + Random.Range(0, 3)));
                SetFilter(filterType);
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                showInv = !showInv;

                if (showInv)
                {
                    Time.timeScale = 0f;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    Time.timeScale = 1f;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }

        private void OnGUI()
        {
            if (showInv)
            {
                scr.x = Screen.width / 16f;
                scr.y = Screen.height / 9f;

                GUI.skin = invSkin;

                GUI.Box(new Rect(0f, 0f, scr.x * 8f, Screen.height), "");

                Display();

                if (null != selectedItem)
                {
                    GUI.Box(new Rect(4.25f * scr.x, 0.25f * scr.y, 3f * scr.x, 0.25f * scr.y), selectedItem.Name);

                    GUI.DrawTexture(new Rect(4.25f * scr.x, 0.5f * scr.y, 3f * scr.x, 3f * scr.y), selectedItem.Icon.texture);

                    GUI.Box(new Rect(4.25f * scr.x, 3.5f * scr.y, 3f * scr.x, 2f * scr.y), selectedItem.Description);

                    ItemUse(selectedItem);
                }
            }
        }

        private void Display()
        {
            for (int i = 0; i < ((int)Item.ItemType.NUMBER_OF_ITEM_TYPES); ++i)
            {
                Item.ItemType type = ((Item.ItemType)(i));
                string name = ((type == Item.ItemType.Undefined) ? "All" : type.ToString());
                if (GUI.Button(new Rect(9.5f * scr.x, (3.25f + i * 0.3f) * scr.y, 3f * scr.x, 0.25f * scr.y), name))
                {
                    SetFilter(type);
                }
            }

            if (filteredInv.Count < 35)
            {
                for (int i = 0; i < filteredInv.Count; ++i)
                {
                    if (GUI.Button(new Rect(0.5f * scr.x, (i + 1) * 0.25f * scr.y, 3f * scr.x, 0.25f * scr.y), filteredInv[i].Name))
                    {
                        selectedItem = filteredInv[i];
                    }
                }
            }
            else
            {
                // Movable scroll position
                scrollPos =
                    // Start of the viewable area
                    GUI.BeginScrollView
                        (
                            // Our View Window
                            new Rect(0f, 0.25f * scr.y, 3.75f * scr.x, 8.5f * scr.y),

                            // Our current scroll position
                            scrollPos,

                            // Scroll Zone (extra space)
                            new Rect(0f, 0f, 0f, 8.5f * scr.y + ((filteredInv.Count - 34) * (0.25f * scr.y))),

                            // Can we see the horizontal bar?
                            false,

                            // Can we see the vertical bar?
                            true
                        );

                for (int i = 0; i < filteredInv.Count; ++i)
                {
                    if (GUI.Button(new Rect(0.5f * scr.x, i * 0.25f * scr.y, 3f * scr.x, 0.25f * scr.y), filteredInv[i].Name))
                    {
                        selectedItem = filteredInv[i];
                    }
                }

                // End the scroll space
                GUI.EndScrollView();
            }
        }

        private void ItemUse(Item item)
        {
            switch (item.Type)
            {
                case (Item.ItemType.Ingredient):
                    break;
                case (Item.ItemType.Potion):
                    break;
                case (Item.ItemType.Scroll):
                    break;
                case (Item.ItemType.Food):
                    break;
                case (Item.ItemType.Armour):
                    ShowEquipmentButton(((int)EquipSlots.Armour), item);
                    break;
                case (Item.ItemType.Weapon):
                    ShowEquipmentButton(((int)EquipSlots.Weapon), item);
                    break;
                case (Item.ItemType.Craftable):
                    break;
                case (Item.ItemType.Currency):
                    break;
                case (Item.ItemType.Quest):
                    break;
                case (Item.ItemType.Misc):
                    break;
                default:
                    break;
            }

            if (GUI.Button(new Rect(9.5f * scr.x, 0.25f * scr.y, 3f * scr.x, 0.25f * scr.y), "Discard"))
            {
                for (int i = 0; i < equipmentSlots.Length; ++i)
                {
                    if ((null != equipmentSlots) && (item == equipmentSlots[i].curItem))
                    {
                        Destroy(equipmentSlots[i].curItemObj);
                        equipmentSlots[i].curItem = null;
                        equipmentSlots[i].curItemObj = null;
                    }
                }

                // Spawn at drop location
                // Code goes here

                GameObject discard = ItemData.SpawnItem(item);
                discard.transform.position = dropLocation.position;
                discard.transform.rotation = dropLocation.rotation;
                discard.AddComponent<Rigidbody>().useGravity = true;

                if (item.Amount > 1)
                {
                    --item.Amount;
                }
                else
                {
                    inv.Remove(item);
                    filteredInv.Remove(item);

                    if (selectedItem == item)
                    {
                        selectedItem = null;
                    }
                }
            }
        }

        private void ShowEquipmentButton(int slot, Item item)
        {
            if (equipmentSlots[slot].curItem != item)
            {
                if (GUI.Button(new Rect(9.5f * scr.x, 1.25f * scr.y, 3f * scr.x, 0.25f * scr.y), "Equip"))
                {
                    if (null != equipmentSlots[slot].curItem)
                    {
                        Destroy(equipmentSlots[slot].curItemObj);
                    }
                    equipmentSlots[slot].curItem = item;
                    equipmentSlots[slot].curItemObj = ItemData.SpawnItem(item, equipmentSlots[slot].location);
                }
            }
            else
            {
                if (GUI.Button(new Rect(9.5f * scr.x, 1.25f * scr.y, 3f * scr.x, 0.25f * scr.y), "Unequip"))
                {
                    Destroy(equipmentSlots[slot].curItemObj);
                    equipmentSlots[slot].curItem = null;
                    equipmentSlots[slot].curItemObj = null;
                }
            }
        }

        private void SetFilter(Item.ItemType type)
        {
            filterType = type;

            filteredInv.Clear();
            if (Item.ItemType.Undefined == type)
            {
                filteredInv = new List<Item>(inv);
            }
            else
            {
                filteredInv = inv.FindAll(item => (item.Type == filterType));
            }
        }

        #endregion
    }


}