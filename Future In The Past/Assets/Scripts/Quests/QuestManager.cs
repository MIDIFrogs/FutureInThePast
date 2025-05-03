using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FutureInThePast.Quests
{
    public static class QuestManager
    {
        private static Dictionary<string, QuestTrigger> triggers;

        public static void Initialize()
        {
            triggers = Resources.LoadAll<TriggerConfig>("Triggers").ToDictionary(x => x.Trigger.Tag, y => y.Trigger);
            // I don't know if it's actually needed.
            foreach (var trigger in triggers.Values)
                trigger.IsCompleted = false;
        }

        public static QuestTrigger GetTrigger(string name) => triggers[name];

        public static void ResetTrigger(string triggerName)
        {
            Debug.Log($"Trigger {triggerName} is unset.");
            triggers[triggerName].IsCompleted = false;
        }

        public static void ResetTrigger(QuestTrigger trigger)
        {
            Debug.Log($"Trigger {trigger.Tag} is set.");
            trigger.IsCompleted = false;
        }

        public static void SetTrigger(string triggerName)
        {
            Debug.Log($"Trigger {triggerName} is set.");
            triggers[triggerName].IsCompleted = true;
        }

        public static void SetTrigger(QuestTrigger trigger)
        {
            Debug.Log($"Trigger {trigger.Tag} is set.");
            trigger.IsCompleted = true;
        }

        public static bool IsTrigger(QuestTrigger trigger)
        {
            return trigger.IsCompleted;
        }
        
        public static bool IsTrigger(string triggerName)
        {
            return triggers[triggerName].IsCompleted;
        }
    }
}
