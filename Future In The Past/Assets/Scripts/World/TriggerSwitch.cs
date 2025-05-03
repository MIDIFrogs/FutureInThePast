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
            if (!trigger.Quest.IsCompleted)
            {
                QuestManager.SetTrigger(trigger.Quest);
            }
            else if (twoWay)
            {
                QuestManager.ResetTrigger(trigger.Quest);
            }
        }
    }
}