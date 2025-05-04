using MIDIFrogs.FutureInThePast.Quests;
using SibGameJam.Inventory;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    public class SeedPlaceholder : InteractiveObject
    {
        [Header("Trigger")]
        [SerializeField] private TriggerConfig trigger;
        [SerializeField] private ItemInfo seedItem;
        [SerializeField] private PlayerInventory inventory;

        private bool placed;

        public override void OnInteract()
        {
            if (placed && inventory.TryAddItem(seedItem))
            {
                QuestManager.ResetTrigger(trigger.Quest);
                placed = false;
            }
            else if (!placed && inventory.TryRemoveItem(seedItem))
            {
                QuestManager.SetTrigger(trigger.Quest);
                placed = true;
            }
        }
    }
}