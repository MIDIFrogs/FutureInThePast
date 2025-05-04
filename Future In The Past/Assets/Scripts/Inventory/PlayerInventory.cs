using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SibGameJam.Inventory
{
    public sealed class PlayerInventory : MonoBehaviour, IEnumerable<ItemInfo>
    {
        [SerializeField] private InventoryGrid inventoryGrid;
        
        [Header("Only for test")]
        [SerializeField] private List<ItemInfo> testItem;
        
        private InventorySlot[] _slots;
        private bool isInitialize = false;
        public InventoryGrid InventoryGrid => inventoryGrid;
        public int MaxCapacity => inventoryGrid.Width * inventoryGrid.Height;
        public IReadOnlyList<InventorySlot> Slots => _slots;
        public event EventHandler<ItemInfo> OnPlayerInventoryUpdated = delegate { };

        public void Initialize()
        {
            _slots = new InventorySlot[MaxCapacity];

            for (int cellIndex = 0; cellIndex < MaxCapacity; cellIndex++)
            {
                var slot = new InventorySlot();
                _slots[cellIndex] = slot;
                slot.OnSlotStatusUpdate += Slot_OnSlotStatusUpdate;
            }

            for (int i = 0; i < testItem.Count; i++)
            {
                foreach (ItemInfo item in testItem)
                {
                    TryAddItem(item);
                }
            }
            isInitialize = true;
        }

        private void Slot_OnSlotStatusUpdate(object sender, CustomEventArgs.InventoryEventArgs e)
        {
            OnPlayerInventoryUpdated(this, e.OldItem);
        }
        
        public bool TryAddItem<TItem>(TItem item) where TItem : ItemInfo
        {
            foreach (InventorySlot slot in _slots)
            {
                if (slot.IsEmpty)
                {
                    slot.AddInSlot(item);
                    return true;
                }
            }

            return false;
        }

        public bool TryRemoveItem<TItem>(TItem item) where TItem : ItemInfo
        {
            foreach (InventorySlot slot in _slots)
            {
                if (slot.ItemInfo == item)
                {
                    slot.RemoveFromSlot();
                    return true;
                }
            }

            return false;
        }

        public bool TryRemoveItem<TItem>(TItem item, int count) where TItem : ItemInfo
        {
            int rightItemCount = _slots.Count(x => x.ItemInfo == item);
            if (rightItemCount >= count)
            {
                for (int i = 0; i < count; i++)
                {
                    TryRemoveItem(item);
                }
                return true;
            }

            return false;
        }
         
        public void MoveItemToCell(int draggedSlotIndex, int droppedSlotIndex)
        {
            InventorySlot draggedSlot = _slots[draggedSlotIndex];
            InventorySlot droppedSlot = _slots[droppedSlotIndex];

            InventorySlot tempSlot = new();
            tempSlot.AddInSlot(draggedSlot.ItemInfo);
            draggedSlot.RemoveFromSlot();
            draggedSlot.AddInSlot(droppedSlot.ItemInfo);
            droppedSlot.RemoveFromSlot();
            droppedSlot.AddInSlot(tempSlot.ItemInfo);
        }

        private void Awake()
        {
            if (!isInitialize)
            {
                Initialize();
            }
        }

        public IEnumerator<ItemInfo> GetEnumerator()
        {
            foreach (var slot in Slots)
            {
                if (slot.ItemInfo != null)
                {
                    yield return slot.ItemInfo;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}