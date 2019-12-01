using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private Image _iconImage = null;

    public UnityEvent OnClick = default;

    private InventorySlot _connectedSlot = null;

    public InventorySlot ConnectedSlot
    {
        get { return _connectedSlot; }
        set { _connectedSlot = value; }
    }

    public Sprite Icon
    {
        set { _iconImage.sprite = value; }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClick.Invoke();
        _connectedSlot.RemoveItem();
    }
}
