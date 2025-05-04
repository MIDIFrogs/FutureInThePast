using System;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast.Quests
{
    [Serializable]
    public class QuestTrigger
    {
        public string tag;
        [SerializeField] private string description;

        private bool isCompleted;
        
        public event EventHandler Completed;

        public event EventHandler Restored;

        public string Tag => tag;
        public string Description => description;

        public bool IsCompleted 
        {
            get => isCompleted;
            set
            {
                if (isCompleted != value)
                {
                    isCompleted = value;
                    if (isCompleted)
                    {
                        Completed?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        Restored?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }
    }
}
