using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DragNDropInventory
{

    public class Inventory : MonoBehaviour
    {
        const float invWidthMult = 9f;
        const float invHeightMult = 5.2f;

        #region Variables

        [Header("Inventory")]
        public bool showInv = false;
        public List<Item> inv = new List<Item>();
        public int slotX = 7;
        public int slotY = 5;
        public Rect invSize;

        [System.Serializable]
        public struct Equipment
        {
            public string name;
            public Item.ItemType slotType;
            public Transform location;
            public GameObject curItemObj;
            public Item curItem;
        };

        public Equipment[] equipmentSlots = null;

        [Header("Dragging")]
        public bool isDragging = false;
        public int draggedFrom;
        public Item draggedItem = null;
        public GameObject droppedItem = null;

        [Header("ToolTip")]
        public Item toolTipItem;
        public bool showToolTip;
        public Rect toolTipRect;

        [Header("References and Locations")]
        public Vector2 scr;

        #endregion

        #region Clamp to Screen

        private Rect ClampToScreen(Rect rect)
        {
            rect.x = Mathf.Clamp(rect.x, 0f, Screen.width - rect.width);
            rect.y = Mathf.Clamp(rect.y, 0f, Screen.height - rect.height);

            return rect;
        }

        #endregion

        #region Add Item

        public void AddItem(int itemID)
        {
            for (int i = 0; i < inv.Count; ++i)
            {
                if (null == inv[i])
                {
                    inv[i] = ItemData.CreateItem(itemID);
                    return;
                }
            }

            Debug.LogError("No inventory room");
        }

        #endregion

        #region Drop Item

        public void DropDraggedItem()
        {
            GameObject droppedItem = ItemData.SpawnItem(draggedItem, transform.position + transform.forward * 3f, transform.rotation);
            droppedItem.name = draggedItem.Name;
            droppedItem.AddComponent<Rigidbody>().useGravity = true;
            
            draggedItem = null;
            isDragging = false;
        }

        #endregion

        #region Draw Item

        private void DrawItem(int windowID)
        {
            if (null != draggedItem.Icon)
            {
                GUI.DrawTexture(new Rect(0f, 0f, scr.x * 0.5f, scr.y * 0.5f), draggedItem.Icon.texture);
            }
        }

        #endregion

        #region ToolTip

        private string ToolTipText(Item item)
        {
            string toolTipText = "";

            if (null != item)
            {
                toolTipText = item.Name + "\n" + item.Description;
            }

            return toolTipText;
        }

        private void DrawToolTip(int windowID)
        {
            GUI.Box(new Rect(0f, 0f, scr.x * 6f, scr.y * 2f), ToolTipText(toolTipItem));
        }

        #endregion

        #region Toggle Inventory

        private void Toggle()
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

        #endregion

        #region Drag Inventory

        private void DragInventory(int windowID)
        {
            GUI.Box(new Rect(scr.x * 0.1f, scr.y * 0.5f, scr.x * (invWidthMult - 0.2f), scr.y * 0.5f), "Banner");
            GUI.Box(new Rect(scr.x * 0.1f, scr.y * 4.5f, scr.x * (invWidthMult - 0.2f), scr.y * 0.5f), "Gold Display");
            showToolTip = false;

            toolTipItem = null;

            // Item slots
            Event currentEvent = Event.current;
            int idx = 0;
            for (int y = 0; y < slotY; ++y)
            {
                for (int x = 0; x < slotX; ++x)
                {
                    Rect slotLocation = new Rect(scr.x * 0.375f + x * (scr.x * 0.75f), scr.y * 1.1f + y * (scr.y * 0.65f), scr.x * 0.75f, scr.y * 0.65f);
                    GUI.Box(slotLocation, "");

                    // Pick Up Item
                    if (
                        (0 == currentEvent.button) &&
                        (currentEvent.type == EventType.MouseDown) &&
                        slotLocation.Contains(currentEvent.mousePosition) &&
                        !isDragging &&
                        (null != inv[idx]) &&
                        !Input.GetKey(KeyCode.LeftShift)
                    )
                    {
                        draggedItem = inv[idx];
                        inv[idx] = null;
                        isDragging = true;
                        draggedFrom = idx;
                    }

                    // Swap Item
                    if (
                        (0 == currentEvent.button) &&
                        (currentEvent.type == EventType.MouseUp) &&
                        slotLocation.Contains(currentEvent.mousePosition) &&
                        isDragging // &&
                        // (null != inv[idx]) // Do we need this? // I don't think so
                    )
                    {
                        if (draggedFrom > 0)
                        {
                            inv[draggedFrom] = inv[idx];
                        }
                        else
                        {
                            equipmentSlots[-draggedFrom].curItem = inv[idx];
                        }
                        inv[idx] = draggedItem;
                        draggedItem = null;
                        isDragging = false;
                    }

                    // Place Item
                    // Swapping handles this case as well

                    // Return Item

                    // Draw Item Icon
                    if (null != inv[idx])
                    {
                        GUI.DrawTexture(slotLocation, inv[idx].Icon.texture);

                        if (!isDragging && showInv && (slotLocation.Contains(currentEvent.mousePosition)))
                        {
                            toolTipItem = inv[idx];
                            showToolTip = true;
                        }
                    }

                    ++idx;
                }
            }

            for (int i = 0; i < Mathf.Max(equipmentSlots.Length, 4); ++i)
            {
                Rect equipSlotLocation = new Rect(scr.x * 6.2f + scr.x * ((i / 2) * 1.5f), scr.y * 1.4f + scr.y * ((i % 2) * 1.8f), scr.x * 0.75f, scr.y * 0.65f);
                GUI.Box(equipSlotLocation, "");
                Rect labelLocation = new Rect(scr.x * 6f + scr.x * ((i / 2) * 1.5f), scr.y * 2.1f + scr.y * ((i % 2) * 1.8f), scr.x * 1.15f, scr.y * 0.65f);
                GUI.Label(labelLocation, equipmentSlots[i].name);

                // Pick Up Item
                if (
                    (0 == currentEvent.button) &&
                    (currentEvent.type == EventType.MouseDown) &&
                    equipSlotLocation.Contains(currentEvent.mousePosition) &&
                    !isDragging &&
                    (null != equipmentSlots[i].curItem) &&
                    !Input.GetKey(KeyCode.LeftShift)
                )
                {
                    draggedItem = equipmentSlots[i].curItem;
                    equipmentSlots[i].curItem = null;
                    isDragging = true;
                    draggedFrom = -i;

                    Destroy(equipmentSlots[i].curItemObj);
                }

                // Swap Item
                if (
                    (0 == currentEvent.button) &&
                    (currentEvent.type == EventType.MouseUp) &&
                    equipSlotLocation.Contains(currentEvent.mousePosition) &&
                    isDragging &&
                    draggedItem.Type == equipmentSlots[i].slotType
                )
                {
                    if (draggedFrom > 0)
                    {
                        inv[draggedFrom] = equipmentSlots[i].curItem;
                    }
                    else
                    {
                        equipmentSlots[-draggedFrom].curItem = equipmentSlots[i].curItem;
                    }
                    equipmentSlots[i].curItem = draggedItem;
                    draggedItem = null;
                    isDragging = false;

                    if (null != equipmentSlots[i].location)
                    {
                        Destroy(equipmentSlots[i].curItemObj);
                        equipmentSlots[i].curItemObj = ItemData.SpawnItem(equipmentSlots[i].curItem, equipmentSlots[i].location);
                    }
                }

                // Place Item
                // Swapping handles this case as well

                // Return Item

                // Draw Item Icon
                if (null != equipmentSlots[i].curItem)
                {
                    GUI.DrawTexture(equipSlotLocation, equipmentSlots[i].curItem.Icon.texture);

                    if (!isDragging && showInv && (equipSlotLocation.Contains(currentEvent.mousePosition)))
                    {
                        toolTipItem = equipmentSlots[i].curItem;
                        showToolTip = true;
                    }
                }
            }

            // Drag Points
            GUI.DragWindow(new Rect(0f, 0f, scr.x * invWidthMult, scr.y * 0.25f)); // Top
            GUI.DragWindow(new Rect(0f, scr.y * 0.25f, scr.x * 0.25f, scr.y * (invHeightMult - 0.5f))); // Left
            GUI.DragWindow(new Rect(scr.x * (invWidthMult - 0.25f), scr.y * 0.25f, scr.x * 0.25f, scr.y * (invHeightMult - 0.5f))); // Right
            GUI.DragWindow(new Rect(0f, scr.y * (invHeightMult - 0.25f), scr.x * invWidthMult, scr.y * 0.25f)); // Bottom
        }

        #endregion

        #region Start

        private void Start()
        {
            scr.x = Screen.width / 16f;
            scr.y = Screen.height / 9f;

            invSize = new Rect(scr.x, scr.y, scr.x * invWidthMult, scr.y * invHeightMult);

            for (int i = 0; i < (slotX * slotY); ++i)
            {
                inv.Add(null);
            }

            for (int i = 0; i < 5; ++i)
            {
                inv[i] =ItemData.CreateItem(i);
            }
            inv[7] = ItemData.CreateItem(402);
            inv[11] = ItemData.CreateItem(502);
            inv[13] = ItemData.CreateItem(401);
            inv[17] = ItemData.CreateItem(400);
        }

        #endregion

        #region Update

        private void Update()
        {
            if (Input.GetKey(KeyCode.Q))
            {
                for (int i = 0; i < inv.Count; ++i)
                {
                    if (null == inv[i])
                    {
                        inv[i] = ItemData.CreateItem(Random.Range(0, 11) * 100 + Random.Range(0, 3));
                        break;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Toggle();
            }

            if (!Mathf.Approximately(scr.x, (Screen.width / 16f)) || !Mathf.Approximately(scr.y, (Screen.height / 9f)))
            {
                scr.x = Screen.width / 16f;
                scr.y = Screen.height / 9f;
                invSize = new Rect(scr.x, scr.y, scr.x * invWidthMult, scr.y * invHeightMult);
            }
        }

        #endregion

        #region OnGUI

        private void OnGUI()
        {
            Event currentEvent = Event.current;

            // Show Inventory
            if (showInv)
            {
                // That 1 is the window ID, and should be unique for different windows
                invSize = ClampToScreen(GUI.Window(1, invSize, DragInventory, "Inventory"));

                // ToolTipDisplay
                if (showToolTip)
                {
                    toolTipRect = new Rect(currentEvent.mousePosition.x + 0.01f, currentEvent.mousePosition.y + 0.01f, scr.x * 6f, scr.y * 2f);
                    GUI.Window(7, toolTipRect, DrawToolTip, "");
                }
            }

            // Drop Item
            if (((0 == currentEvent.button) && (EventType.MouseUp == currentEvent.type) && isDragging) || (isDragging && !showInv))
            {
                DropDraggedItem();
            }

            // Draw Item On Mouse
            if (isDragging)
            {
                if (null == draggedItem)
                {
                    // We really shouldn't get here
                    Debug.LogError("Dragging null item");
                    isDragging = false;
                }
                else
                {
                    Rect mouseLocation = new Rect(currentEvent.mousePosition.x + 0.125f, currentEvent.mousePosition.y + 0.125f, scr.x * 0.5f, scr.y * 0.5f);
                    GUI.Window(72, mouseLocation, DrawItem, "");
                }
            }
        }

        #endregion

    }
}