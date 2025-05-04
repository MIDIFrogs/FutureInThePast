using System.Collections.Generic;
using System.Linq;
using MIDIFrogs.FutureInThePast.Quests;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    public class TriggerSwitch : InteractiveObject
    {
        [Header("Trigger")]
        [SerializeField] private TriggerConfig trigger;
        [SerializeField] private List<TriggerConfig> requirements;
        [SerializeField] private bool twoWay;

        public override void OnInteract()
        {
            if (requirements.All(x => x.Quest.IsCompleted))
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
}