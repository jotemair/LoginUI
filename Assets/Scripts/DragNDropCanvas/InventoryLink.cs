using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryLink : MonoBehaviour
{
    private DragNDropInventoryCanvas.Inventory _inventory = null;

    public DragNDropInventoryCanvas.Inventory Inventory
    {
        get
        {
            if (null == _inventory)
            {
                _inventory = GetComponentInParent<DragNDropInventoryCanvas.Inventory>();
            }

            return _inventory;
        }
    }
}
