using UnityEngine;

namespace MIDIFrogs.FutureInThePast.Quests
{
    [CreateAssetMenu(fileName = "New quest trigger", menuName = "Quests/Trigger")]
    public class TriggerConfig : ScriptableObject
    {
        [SerializeField] private QuestTrigger trigger;
        [SerializeField] private bool initialValue;

        public QuestTrigger Quest => trigger;

        public bool InitialValue => initialValue;

        private void OnValidate()
        {
            if (string.IsNullOrWhiteSpace(trigger.Tag))
            {
                trigger.tag = name;
            }
        }
    }
}
