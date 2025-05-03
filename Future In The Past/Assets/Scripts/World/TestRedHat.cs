using MIDIFrogs.FutureInThePast.UI.DialogSystem;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    public class TestRedHat : InteractableObject
    {
        [SerializeField] private DialogPlayer player;
        [SerializeField] private Dialog dialogToPlay;

        protected override void OnInteract()
        {
            player.StartDialog(dialogToPlay);
        }
    }
}