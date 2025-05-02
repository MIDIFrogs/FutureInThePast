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
        }

        public void SetTrigger(string triggerName)
        {
            triggers[triggerName].IsCompleted = true;
        }

        public bool IsTrigger(string triggerName)
        {
            return triggers[triggerName].IsCompleted;
        }
    }
}
