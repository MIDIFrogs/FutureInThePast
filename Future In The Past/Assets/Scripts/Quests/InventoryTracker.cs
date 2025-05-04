using System;
using System.Collections.Generic;
using System.Linq;
using MIDIFrogs.FutureInThePast.Quests;
using UnityEngine;

namespace SibGameJam.Inventory.UI
{
    public class InventoryTracker : MonoBehaviour
    {
        [SerializeField] private List<ItemTriggerPair> items;
        [SerializeField] private PlayerInventory inventory;

        private bool suppressUpdates;
        private bool isInitialized;

        private void Awake()
        {
            if (!isInitialized)
            {
                Initialize();
            }
        }

        public void Initialize()
        {
            inventory.OnPlayerInventoryUpdated += OnInventoryChanged;

            foreach (var pair in items)
            {
                pair.Trigger.Quest.Completed += OnTriggerSet;
                pair.Trigger.Quest.Restored += OnTriggerReset;
            }
            isInitialized = true;
        }

        private void OnInventoryChanged(object sender, ItemInfo e)
        {
            if (suppressUpdates)
                return;
            var pair = items.Find(x => x.Item == e);
            if (pair == null)
                return;
            suppressUpdates = true;
            pair.Trigger.Quest.IsCompleted = inventory.Contains(e);
            suppressUpdates = false;
        }

        private void OnTriggerReset(object sender, EventArgs e)
        {
            if (suppressUpdates)
                return;
            Debug.Log(items.Count);
            Debug.Log(((QuestTrigger)sender).Tag);
            var pair = items.Find(x => x.Trigger.Quest.Tag == ((QuestTrigger)sender).Tag);
            suppressUpdates = true;
            if (pair == null)
                return;
            Debug.Log($"Found {pair.Trigger.Quest.Tag}");
            inventory.TryRemoveItem(pair.Item);
            suppressUpdates = false;
        }

        private void OnTriggerSet(object sender, EventArgs e)
        {
            if (suppressUpdates)
                return;
            Debug.Log(items.Count);
            Debug.Log(((QuestTrigger)sender).Tag);
            var pair = items.Find(x => x.Trigger.Quest.Tag == ((QuestTrigger)sender).Tag);
            suppressUpdates = true;
            if (pair == null)
                return;
            Debug.Log($"Found {pair.Trigger.Quest.Tag}");
            inventory.TryAddItem(pair.Item);
            suppressUpdates = false;
        }
    }

    [Serializable]
    public class ItemTriggerPair
    {
        public ItemInfo Item;
        public TriggerConfig Trigger;
    }
}