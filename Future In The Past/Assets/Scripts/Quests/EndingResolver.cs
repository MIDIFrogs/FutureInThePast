using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using MIDIFrogs.FutureInThePast.Navigation;
using MIDIFrogs.FutureInThePast.Quests.Dialogs;
using MIDIFrogs.FutureInThePast.UI.Dialogs;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast.Quests
{
    public class EndingResolver : MonoBehaviour
    {
        [Header("Composition")]
        [SerializeField] private List<EndingLink> endings;

        [Header("Bindings")]
        [SerializeField] private DialogPlayer dialogPlayer;
        [SerializeField] private SimpleNavigator backNavigator;

        private async void Start()
        {
            Dialog selectedDialog = null;
            foreach (var ending in endings.OrderByDescending(x => x.Triggers.Count))
            {
                if (ending.Triggers.All(x => x.Quest.IsCompleted))
                {
                    selectedDialog = ending.EndingDialog;
                }
            }
            Debug.Log($"Selected dialog {selectedDialog.name}");
            await dialogPlayer.StartDialogAsync(selectedDialog);
        }
    }

    [Serializable]
    public class EndingLink
    {
        public List<TriggerConfig> Triggers;
        public Dialog EndingDialog;
    }
}