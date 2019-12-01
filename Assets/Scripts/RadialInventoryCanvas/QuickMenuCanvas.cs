using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickMenuCanvas : MonoBehaviour
{
    [SerializeField]
    private RectTransform _root = null;

    [SerializeField]
    private DragNDropInventoryCanvas.Inventory _connectedInv = null;

    private List<(Item, InventorySlot)> _connectedInvItems = null;

    private int _itemCount = 0;

    [SerializeField]
    private GameObject _quickSlotPrefab = null;

    private List<QuickSlot> _slots = new List<QuickSlot>();

    [SerializeField]
    private float _slotPosAdjust = 0f;

    private bool _shouldShowInv = false;

    private void Start()
    {
        _root.gameObject.SetActive(false);
    }

    private void Update()
    {
        bool showingInv = false;

        if (Input.GetKey(KeyCode.Tab))
        {
            _connectedInvItems = new List<(Item, InventorySlot)>();

            foreach (var slot in _connectedInv.ItemSlots)
            {
                if (null != slot.SlotItem)
                {
                    _connectedInvItems.Add((slot.SlotItem, slot));
                }
            }

            GenerateSlots();
            _itemCount = _connectedInvItems.Count;

            for (int i = 0; i < _itemCount; ++i)
            {
                _slots[i].Icon = _connectedInvItems[i].Item1.Icon;
            }

            showingInv = true;
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            showingInv = false;
        }

        if (showingInv != _shouldShowInv)
        {
            if (showingInv)
            {
                _shouldShowInv = true;
                _root.gameObject.SetActive(true);
                InventoryManager.Instance.OpenInventory();
            }
            else
            {
                _shouldShowInv = false;
                _root.gameObject.SetActive(false);
                InventoryManager.Instance.CloseInventory();
            }
        }
    }

    private void GenerateSlots()
    {
        foreach (var slot in _slots)
        {
            Destroy(slot.gameObject);
        }
        _slots.Clear();

        float sectorAngle = (360f / _connectedInvItems.Count) * Mathf.Deg2Rad;

        foreach (var item in _connectedInvItems)
        {
            GameObject newSlot = Instantiate<GameObject>(_quickSlotPrefab, _root.transform);
            RectTransform slotRect = newSlot.GetComponent<RectTransform>();

            float adjustedWidth = (_root.sizeDelta.x / 2f + _slotPosAdjust);
            float adjustedHeight = (_root.sizeDelta.y / 2f + _slotPosAdjust);

            slotRect.anchoredPosition = new Vector2(adjustedWidth * Mathf.Sin(_slots.Count * sectorAngle), adjustedHeight * Mathf.Cos(_slots.Count * sectorAngle));

            QuickSlot newQuickSlot = newSlot.GetComponent<QuickSlot>();
            newQuickSlot.ConnectedSlot = item.Item2;
            _slots.Add(newQuickSlot);
        }
    }
}
