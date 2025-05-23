using System;
using System.Collections.Generic;
using MIDIFrogs.FutureInThePast.Quests;
using UnityEngine;
using UnityEngine.Events;

namespace MIDIFrogs.FutureInThePast
{
    public class ReactiveObject : MonoBehaviour
    {
        [SerializeField] private List<TriggerCallbackInfo> triggerCallbacks;

        protected virtual void Start()
        {
            foreach (var info in triggerCallbacks)
            {
                info.TriggerConfig.Quest.Completed += (s, e) => info.OnTriggerCompleted.Invoke();
                info.TriggerConfig.Quest.Restored += (s, e) => info.OnTriggerRestored.Invoke();
            }
        }
    }

    [Serializable]
    public class TriggerCallbackInfo
    {
        public TriggerConfig TriggerConfig;
        public UnityEvent OnTriggerCompleted;
        public UnityEvent OnTriggerRestored;
    }
}