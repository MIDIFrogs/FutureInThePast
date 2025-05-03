using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MIDIFrogs.FutureInThePast.Navigation;
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
        [SerializeField] private SimpleNavigator backNavigator;

        private void Start()
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
            dialogPlayer.StartDialog(selectedDialog);
            StartCoroutine(WaitForDialogs());
        }

        private IEnumerator WaitForDialogs()
        {
            while (!dialogPlayer.CurrentDialogTask.IsCompleted)
            {
                yield return new WaitForEndOfFrame();
            }
            backNavigator.Navigate();
        }
    }

    [Serializable]
    public class EndingLink
    {
        public List<TriggerConfig> Triggers;
        public Dialog EndingDialog;
    }
}