using Cysharp.Threading.Tasks;
using MIDIFrogs.FutureInThePast.UI.Dialogs;
using MIDIFrogs.FutureInThePast.Quests.Dialogs;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    public class StartupDialog : MonoBehaviour
    {
        [Header("Dialogs")]
        [SerializeField] private DialogPlayer player;
        [SerializeField] private Dialog dialogToPlay;

        protected virtual async void Start()
        {
            await player.StartDialogAsync(dialogToPlay);
        }
    }
}