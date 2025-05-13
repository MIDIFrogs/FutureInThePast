using MIDIFrogs.FutureInThePast.Quests.Dialogs;
using MIDIFrogs.FutureInThePast.UI.Dialogs;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    public class DialogInteraction : InteractiveObject
    {
        [Header("Interactions")]
        [SerializeField] private DialogPlayer player;
        [SerializeField] private Dialog dialogToPlay;
        [SerializeField] private bool canRepeat = false;
        private bool isPlayed = false;

        public override async void OnInteract()
        {
            if (!isPlayed || canRepeat)
            {
                await player.StartDialogAsync(dialogToPlay);
                isPlayed = true;
            }
        }
    }
}