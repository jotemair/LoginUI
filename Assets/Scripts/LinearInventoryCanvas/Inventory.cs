using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LinearInventoryCanvas
{

    public class Inventory : MonoBehaviour
    {
        public GUISkin invSkin = null;
        public GUIStyle boxStyle = null;

        #region Variables

        public List<Item> inv = new List<Item>();

        public Item selectedItem = null;

        public bool showInv = false;

        public Vector2 scr = Vector2.zero;

        public Vector2 scrollPos = Vector2.zero;

        public int money;

        public GameObject invParent = null;

        public Transform dropLocation = null;

        public GameObject buttonPrefab = null;

        private List<GameObject> itemButtons = new List<GameObject>();

        public GameObject contentArea = null;

        public GameObject scrollBar = null;

        public Button discardButton = null;

        public Button equipButton = null;

        public Text nameText = null;

        public Text descriptionText = null;

        public Image itemImage = null;

        public GameObject sortButtonArea = null;

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
                AddItem(i);
            }
            AddItem(402);
            AddItem(502);
            AddItem(401);
            AddItem(400);

            SetFilter(filterType);

            DisplaySortButtons();
        }

        private void Update()
        {
            // showInv = showInv != Input.GetKeyDown(KeyCode.Tab);

            if (Input.GetKey(KeyCode.Q))
            {
                AddItem(Random.Range(0, 11) * 100 + Random.Range(0, 3));
                SetFilter(filterType);
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                showInv = !showInv;

                invParent.SetActive(showInv);
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

        private void DisplaySortButtons()
        {
            Rect sortButtonAreaSize = sortButtonArea.GetComponent<RectTransform>().rect;
            float buttonHeight = sortButtonAreaSize.height / ((float)((int)Item.ItemType.NUMBER_OF_ITEM_TYPES));

            for (int i = 0; i < ((int)Item.ItemType.NUMBER_OF_ITEM_TYPES); ++i)
            {
                GameObject sortButton = Instantiate<GameObject>(buttonPrefab, sortButtonArea.transform);
                RectTransform rt = sortButton.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(0f, -(i * buttonHeight));
                rt.sizeDelta = new Vector2(sortButtonAreaSize.width, buttonHeight);
                Item.ItemType type = ((Item.ItemType)(i));
                string name = ((type == Item.ItemType.Undefined) ? "All" : type.ToString());
                sortButton.GetComponentInChildren<Text>().text = name;
                sortButton.GetComponent<Button>().onClick.AddListener(() => SetFilter(type));
            }
        }

        public void SetFilter(Item.ItemType type)
        {
            if (type != filterType)
            {
                Deselect();
            }

            filterType = type;

            int activeCount = 0;
            if (Item.ItemType.Undefined == type)
            {
                foreach (var itemButton in itemButtons)
                {
                    itemButton.SetActive(true);
                    ++activeCount;
                }
            }
            else
            {
                for (int i = 0; i < inv.Count; ++i)
                {
                    itemButtons[i].SetActive(inv[i].Type == type);
                    activeCount += ((inv[i].Type == type) ? 1 : 0);
                }
            }

            // Set Content area size
            contentArea.GetComponent<RectTransform>().sizeDelta = new Vector2(contentArea.GetComponent<RectTransform>().sizeDelta.x, activeCount * 30f + 5f);
        }

        public void SelectItem(Item item)
        {
            selectedItem = item;
            nameText.text = selectedItem.Name;
            itemImage.sprite = selectedItem.Icon;
            descriptionText.text = selectedItem.Description;

            discardButton.gameObject.SetActive(true);

            equipButton.gameObject.SetActive(false);
            bool equipped = false;
            if ((selectedItem.Type == Item.ItemType.Armour) || (selectedItem.Type == Item.ItemType.Weapon))
            {
                int slot = ((selectedItem.Type == Item.ItemType.Armour) ? (int)EquipSlots.Armour : (int)EquipSlots.Weapon);

                if (equipmentSlots[slot].curItem == selectedItem)
                {
                    equipped = true;
                    equipButton.gameObject.SetActive(true);
                    equipButton.GetComponentInChildren<Text>().text = "Unequip";
                }
            }

            if (!equipped)
            {
                if ((selectedItem.Type == Item.ItemType.Armour) || (selectedItem.Type == Item.ItemType.Weapon))
                {
                    equipButton.gameObject.SetActive(true);
                    equipButton.GetComponentInChildren<Text>().text = "Equip";
                }
            }
        }

        public void Discard()
        {
            for (int i = 0; i < equipmentSlots.Length; ++i)
            {
                if ((null != equipmentSlots) && (selectedItem == equipmentSlots[i].curItem))
                {
                    Destroy(equipmentSlots[i].curItemObj);
                    equipmentSlots[i].curItem = null;
                    equipmentSlots[i].curItemObj = null;
                }
            }

            // Spawn at drop location
            // Code goes here

            GameObject discard = ItemData.SpawnItem(selectedItem);
            discard.transform.position = dropLocation.position;
            discard.transform.rotation = dropLocation.rotation;
            discard.AddComponent<Rigidbody>().useGravity = true;

            if (selectedItem.Amount > 1)
            {
                --selectedItem.Amount;
            }
            else
            {
                RemoveSelectedItem();
            }
        }

        public void Equip()
        {
            int slot = ((selectedItem.Type == Item.ItemType.Armour) ? (int)EquipSlots.Armour : (int)EquipSlots.Weapon);

            if (equipmentSlots[slot].curItem != selectedItem)
            {
                if (null != equipmentSlots[slot].curItem)
                {
                    Destroy(equipmentSlots[slot].curItemObj);
                }
                equipmentSlots[slot].curItem = selectedItem;
                equipmentSlots[slot].curItemObj = ItemData.SpawnItem(selectedItem, equipmentSlots[slot].location);
            }
            else
            {
                Destroy(equipmentSlots[slot].curItemObj);
                equipmentSlots[slot].curItem = null;
                equipmentSlots[slot].curItemObj = null;
            }
        }

        private void Deselect()
        {
            selectedItem = null;
            nameText.text = "";
            itemImage.sprite = null;
            descriptionText.text = "";
            discardButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(false);
        }

        private void AddItem(int itemID)
        {
            Item item = ItemData.CreateItem(itemID);
            inv.Add(item);

            GameObject buttonObj = Instantiate<GameObject>(buttonPrefab, contentArea.transform);
            itemButtons.Add(buttonObj);
            buttonObj.GetComponentInChildren<Text>().text = item.Name;
            buttonObj.GetComponent<Button>().onClick.AddListener(() => SelectItem(item));

            SetFilter(filterType);
        }

        private void RemoveSelectedItem()
        {
            int idx = 0;
            for (; idx < inv.Count; ++idx)
            {
                if (inv[idx] == selectedItem)
                {
                    break;
                }
            }

            inv.RemoveAt(idx);

            GameObject buttonToRemove = itemButtons[idx];
            itemButtons.RemoveAt(idx);

            Destroy(buttonToRemove);

            Deselect();
        }

        #endregion
    }

}