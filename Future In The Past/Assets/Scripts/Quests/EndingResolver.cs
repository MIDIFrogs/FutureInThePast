using System;
using System.Collections.Generic;
using System.Linq;
using MIDIFrogs.FutureInThePast.UI.DialogSystem;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast.Quests
{
    public class EndingResolver : MonoBehaviour
    {
        [Header("Composition")]
        [SerializeField] private List<EndingLink> endings;

        [Header("Bindings")]
        [SerializeField] private DialogPlayer dialogPlayer;

        private void Start()
        {
            Dialog selectedDialog = null;
            foreach (var ending in endings.OrderByDescending(x => x.Triggers.Count))
            {
                if (ending.Triggers.All(x => x.Trigger.IsCompleted))
                {
                    selectedDialog = ending.EndingDialog;
                }
            }
            dialogPlayer.StartDialog(selectedDialog);
        }
    }

    [Serializable]
    public class EndingLink
    {
        public List<TriggerConfig> Triggers;
        public Dialog EndingDialog;
    }
}