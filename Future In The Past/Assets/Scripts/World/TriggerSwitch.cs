using MIDIFrogs.FutureInThePast.Quests;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    public class TriggerSwitch : InteractiveObject
    {
        [Header("Trigger")]
        [SerializeField] private TriggerConfig trigger;
        [SerializeField] private bool twoWay;

        public override void OnInteract()
        {
            if (!trigger.Trigger.IsCompleted)
            {
                QuestManager.SetTrigger(trigger.Trigger);
            }
            else if (twoWay)
            {
                QuestManager.ResetTrigger(trigger.Trigger);
            }
        }
    }
}