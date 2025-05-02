using System;
using UnityEngine;

namespace FutureInThePast.Quests
{
    [Serializable]
    public class QuestTrigger
    {
        [SerializeField] private string tag;
        [SerializeField] private string description;

        public string Tag => tag;
        public string Description => description;

        public bool IsCompleted { get; set; }
    }
}
