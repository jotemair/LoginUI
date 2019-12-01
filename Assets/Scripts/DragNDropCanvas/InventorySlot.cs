using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private DragNDropInventoryCanvas.Inventory _containingInventory = null;

    private Item _item = null;

    private Image _itemIconRender = null;

    private RectTransform _panel = null;

    private Vector2 _relativeDragStartPosition = Vector2.zero;

    private Vector3 _initialLocalPosition = Vector3.zero;

    private bool _cancelDrag = true;

    [SerializeField]
    private Item.ItemType _slotType = Item.ItemType.Undefined;

    private bool _isSetup = false;

    public bool HasItem
    {
        get { return (null != _item); }
    }

    public Item SlotItem
    {
        get { return _item; }
    }

    public DragNDropInventoryCanvas.Inventory Inventory
    {
        get { return _containingInventory; }
        set { _containingInventory = value; }
    }

    private void Start()
    {
        Setup();
    }

    public void Setup()
    {
        if (!_isSetup)
        {
            _panel = GetComponent<RectTransform>();
            _initialLocalPosition = _panel.localPosition;

            _itemIconRender = GetComponent<Image>();
            _itemIconRender.sprite = null;
            _itemIconRender.color = new Color(0f, 0f, 0f, 0f);

            _isSetup = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_cancelDrag)
        {
            Vector2 newPos = eventData.position + _relativeDragStartPosition;
            _panel.position = new Vector3(newPos.x, newPos.y, 0f);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (null != _item)
        {
            _cancelDrag = false;
            _relativeDragStartPosition = new Vector2(_panel.position.x, _panel.position.y) - eventData.position;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_item == null)
        {
            return;
        }

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        InventorySlot targetSlot = null;
        DragNDropInventoryCanvas.Inventory inventory = null;
        foreach (var result in results)
        {
            if (null == targetSlot)
            {
                targetSlot = result.gameObject.GetComponent<InventorySlot>();
                if (this == targetSlot)
                {
                    targetSlot = null;
                }
            }

            if (null == inventory)
            {
                InventoryLink invLink = result.gameObject.GetComponent<InventoryLink>();
                if (null != invLink)
                {
                    inventory = invLink.Inventory;
                }
            }
        }

        if ((null != targetSlot) && targetSlot.CanAcceptItem(_item))
        {
            if (!targetSlot.HasItem)
            {
                targetSlot.SetItem(_item);
                RemoveItem();
            }
            else
            {
                TrySwap(targetSlot);
            }
        }
        else if (null != inventory)
        {
            if (_containingInventory != inventory)
            {
                if (inventory.TryAddItem(_item))
                {
                    RemoveItem();
                }
            }
        }
        else if ((null != _containingInventory) && (null != _containingInventory.DropPoint))
        {
            ItemData.SpawnItem(_item, _containingInventory.DropPoint.position, _containingInventory.DropPoint.rotation).AddComponent<Rigidbody>().useGravity = true;
            RemoveItem();
        }

        BackToInital();
    }

    public void BackToInital()
    {
        _cancelDrag = true;
        _panel.localPosition = _initialLocalPosition;
    }

    public virtual void SetItem(Item item)
    {
        if (null == _item)
        {
            _item = item;
            _itemIconRender.sprite = _item.Icon;
            _itemIconRender.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            Debug.LogError("Adding item to slot that already has item");
        }
    }

    public virtual void RemoveItem()
    {
        _item = null;
        _itemIconRender.sprite = null;
        _itemIconRender.color = new Color(0f, 0f, 0f, 0f);
    }

    private bool CouldSwap(InventorySlot other)
    {
        bool couldSwap =
            ( 
                (_slotType == other._slotType) ||
                (
                    CanAcceptItem(other._item) && other.CanAcceptItem(_item)
                ) 
            );

        return couldSwap;
    }

    public void TrySwap(InventorySlot other)
    {
        if (CouldSwap(other))
        {
            Item tmp = _item;
            RemoveItem();
            SetItem(other._item);
            other.RemoveItem();
            other.SetItem(tmp);
        }
    }

    public bool CanAcceptItem(Item item)
    {
        return ((Item.ItemType.Undefined == _slotType) || (_slotType == item.Type));
    }
}
