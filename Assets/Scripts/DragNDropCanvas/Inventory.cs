using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DragNDropInventoryCanvas
{
    public class Inventory : MonoBehaviour
    {
        private List<InventorySlot> _slots = null;

        private bool _showInv = false;

        [SerializeField]
        private GameObject _invParent = null;

        [SerializeField]
        private KeyCode _invKey = KeyCode.Tab;

        [SerializeField]
        private bool _debugStuff = false;

        [SerializeField]
        private Transform _dropPoint = null;

        [SerializeField]
        private bool _canDropInItems = true;

        public Transform DropPoint
        {
            get { return _dropPoint; }
        }

        public List<InventorySlot> ItemSlots
        {
            get { return _slots; }
        }

        private void Start()
        {
            _slots = new List<InventorySlot>(gameObject.GetComponentsInChildren<InventorySlot>());

            foreach (var slot in _slots)
            {
                slot.Setup();
                slot.Inventory = this;
            }

            _invParent.SetActive(_showInv);
        }

        private void Update()
        {
            if (_debugStuff && (Input.GetKeyDown(KeyCode.Q)))
            {
                TryAddItem(ItemData.CreateItem(Random.Range(0, 11) * 100 + Random.Range(0, 3)));
            }

            if (Input.GetKeyDown(_invKey))
            {
                _showInv = !_showInv;

                _invParent.SetActive(_showInv);

                foreach (var slot in _slots)
                {
                    slot.BackToInital();
                }

                if (_showInv)
                {
                    InventoryManager.Instance.OpenInventory();
                }
                else
                {
                    InventoryManager.Instance.CloseInventory();
                }
            }
        }

        public bool TryAddItem(Item item)
        {
            bool canAddItem = false;

            if (_canDropInItems)
            {
                foreach (var slot in _slots)
                {
                    if (!slot.HasItem && slot.CanAcceptItem(item))
                    {
                        canAddItem = true;
                        slot.SetItem(item);
                        break;
                    }
                }
            }

            return canAddItem;
        }
    }

}