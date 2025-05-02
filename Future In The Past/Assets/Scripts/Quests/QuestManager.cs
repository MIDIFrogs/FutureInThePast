using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FutureInThePast.Quests
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private Dictionary<string, QuestTrigger> triggers;

        private void Start()
        {
            triggers = Resources.LoadAll<TriggerConfig>("Triggers").ToDictionary(x => x.Trigger.Tag, y => y.Trigger);
            // I don't know if it's actually needed.
            foreach (var trigger in triggers.Values)
                trigger.IsCompleted = false;
        }

        public void SetTrigger(string triggerName)
        {
            Debug.Log($"Trigger {triggerName} is set.");
            triggers[triggerName].IsCompleted = true;
        }

        public void SetTrigger(QuestTrigger trigger)
        {
            Debug.Log($"Triggers status: {string.Join(',', triggers.Values.Select(x => x.Tag + ':' + x.IsCompleted))}");
            Debug.Log($"Trigger {trigger.Tag} is set.");
            trigger.IsCompleted = true;
            Debug.Log($"Triggers status: {string.Join(',', triggers.Values.Select(x => x.Tag + ':' + x.IsCompleted))}");
        }

        public bool IsTrigger(QuestTrigger trigger)
        {
            return trigger.IsCompleted;
        }
        
        public bool IsTrigger(string triggerName)
        {
            return triggers[triggerName].IsCompleted;
        }
    }
}
