using MIDIFrogs.FutureInThePast.UI.DialogSystem;
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

        public override void OnInteract()
        {
            if (!isPlayed || canRepeat)
            {
                player.StartDialog(dialogToPlay);
                isPlayed = true;
            }
        }
    }
}