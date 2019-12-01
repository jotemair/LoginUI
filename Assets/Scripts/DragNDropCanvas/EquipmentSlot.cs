using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : InventorySlot
{
    [SerializeField]
    private Transform _equipLocation = null;

    private GameObject _equipObj = null;

    public override void SetItem(Item item)
    {
        base.SetItem(item);

        if (null != _equipLocation)
        {
            _equipObj = ItemData.SpawnItem(SlotItem, _equipLocation);
        }
    }

    public override void RemoveItem()
    {
        base.RemoveItem();

        Destroy(_equipObj);
    }
}
