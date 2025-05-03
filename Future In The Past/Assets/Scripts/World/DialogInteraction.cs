using MIDIFrogs.FutureInThePast.UI.DialogSystem;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    public class DialogInteraction : InteractiveObject
    {
        [Header("Interactions")]
        [SerializeField] private DialogPlayer player;
        [SerializeField] private Dialog dialogToPlay;

        public override void OnInteract()
        {
            player.StartDialog(dialogToPlay);
        }
    }
}