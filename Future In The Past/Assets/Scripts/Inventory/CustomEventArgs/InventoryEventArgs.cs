using System;

namespace SibGameJam.Inventory.CustomEventArgs
{
    public class InventoryEventArgs : EventArgs
    {
        public ItemInfo OldItem { get; private set; }

        public InventoryEventArgs(ItemInfo oldItem)
        {
            OldItem = oldItem;
        }
    }
}