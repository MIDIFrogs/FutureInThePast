using SibGameJam.Inventory;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    public class ItemTransferrer : InteractiveObject
    {
        [Header("Options")]
        [SerializeField] private PlayerInventory pastKolobok;
        [SerializeField] private PlayerInventory futureKolobok;
        [SerializeField] private ItemInfo toTransfer;

        public override void OnInteract()
        {
            if (pastKolobok.TryRemoveItem(toTransfer))
            {
                if (!futureKolobok.TryAddItem(toTransfer))
                    pastKolobok.TryAddItem(toTransfer);
            }
        }
    }
}