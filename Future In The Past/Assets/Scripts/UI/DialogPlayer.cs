using MIDIFrogs.FutureInThePast.UI;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MIDIFrogs.FutureInThePast.DialogSystem
{
    public class DialogPlayer : MonoBehaviour
    {
        [SerializeField] private PauseManager pauseMenu;

        [Header("Dialog properties")]
        [SerializeField] private float textSpeed;
        [SerializeField] private bool autoplay;
        [SerializeField] private DialogFrame dialogFrame;

        public float TextSpeed => textSpeed;

        public bool Autoplay => autoplay;

        /// <summary>
        /// Starts a dialog and waits for the completion.
        /// </summary>
        /// <param name="dialog">Dialog to read.</param>
        public async Task StartDialogAsync(Dialog dialog)
        {
            pauseMenu.IsPaused = true;
            dialogFrame.gameObject.SetActive(true);

            foreach(var replic in dialog.DialogContent)
                await dialogFrame.ShowText(replic, TextSpeed, Autoplay);

            dialogFrame.gameObject.SetActive(false);
            pauseMenu.IsPaused = false;
        }
    }
}
