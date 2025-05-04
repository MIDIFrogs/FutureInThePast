using MIDIFrogs.FutureInThePast.UI.DialogSystem;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    public class StartupDialog : MonoBehaviour
    {
        [Header("Dialogs")]
        [SerializeField] private DialogPlayer player;
        [SerializeField] private Dialog dialogToPlay;

        protected virtual void Start()
        {
            player.StartDialog(dialogToPlay);
        }
    }
}