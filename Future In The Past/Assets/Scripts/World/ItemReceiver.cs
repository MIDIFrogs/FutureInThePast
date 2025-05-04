using MIDIFrogs.FutureInThePast.Quests;
using SibGameJam.Inventory;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    public class ItemReceiver : InteractiveObject
    {
        [Header("Options")]
        [SerializeField] private TriggerConfig givingTrigger;
        [SerializeField] private PlayerInventory inventory;
        [SerializeField] private ItemInfo toAdd;

        private bool received = false;

        public override void OnInteract()
        {
            if (!givingTrigger.Quest.IsCompleted)
                return;
            if (!received)
            {
                if (inventory.TryAddItem(toAdd))
                    received = true;
            }
        }
    }
}