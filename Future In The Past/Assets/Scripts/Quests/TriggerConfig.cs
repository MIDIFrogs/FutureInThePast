using UnityEngine;

namespace MIDIFrogs.FutureInThePast.Quests
{
    [CreateAssetMenu(fileName = "New quest trigger", menuName = "Quests/Trigger")]
    public class TriggerConfig : ScriptableObject
    {
        [SerializeField] private QuestTrigger trigger;

        public QuestTrigger Quest => trigger;
    }
}
