using SibGameJam.Inventory;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    public class ItemProvider : InteractiveObject
    {
        [Header("Options")]
        [SerializeField] private PlayerInventory inventory;
        [SerializeField] private ItemInfo toAdd;

        private bool received = false;

        public override void OnInteract()
        {
            if (!received)
            {
                if (inventory.TryAddItem(toAdd))
                    received = true;
            }
        }
    }
}