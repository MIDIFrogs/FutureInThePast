using System.Collections;
using System.Threading.Tasks;
using MIDIFrogs.FutureInThePast.Quests;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast.UI.DialogSystem
{
    public class DialogPlayer : MonoBehaviour
    {
        [SerializeField] private PauseManager pauseMenu;

        [Header("Dialog properties")]
        [SerializeField] private float textSpeed;
        [SerializeField] private bool autoplay;
        [SerializeField] private DialogFrame dialogFrame;
        [SerializeField] private ResponseFrame responseFrame;

        [Header("Debug options")]
        [SerializeField] private Dialog startupDialog;

        public float TextSpeed => textSpeed;

        public bool Autoplay => autoplay;

        public Task CurrentDialogTask { get; private set; }

        private void Start()
        {
            QuestManager.Initialize();
            if (startupDialog != null)
            {
                CurrentDialogTask = StartDialogAsync(startupDialog);
            }
        }

        /// <summary>
        /// Starts a dialog without waiting for the completion
        /// </summary>
        /// <param name="dialog">Dialog to read.</param>
        public void StartDialog(Dialog dialog)
        {
            CurrentDialogTask = StartDialogAsync(dialog);
        }

        /// <summary>
        /// Starts a dialog and waits for the completion.
        /// </summary>
        /// <param name="dialog">Dialog to read.</param>
        public async Task StartDialogAsync(Dialog dialog)
        {
            try
            {
                pauseMenu.IsPaused = true;

                foreach (var clip in dialog.Clips)
                {
                    var currentClip = clip;
                    while (currentClip != null)
                    {
                        dialogFrame.gameObject.SetActive(true);
                        foreach (var replic in currentClip.Replics)
                            await dialogFrame.ShowText(replic, TextSpeed, Autoplay);
                        dialogFrame.gameObject.SetActive(false);
                        if (currentClip.Responses.Count > 0)
                        {
                            responseFrame.gameObject.SetActive(true);
                            var response = await responseFrame.WaitForResponse(currentClip);
                            responseFrame.gameObject.SetActive(false);
                            QuestManager.SetTrigger(response.SelectionTrigger);
                            currentClip = response.Continuation;
                        }
                        else
                        {
                            currentClip = null;
                        }
                    }
                }
                
                pauseMenu.IsPaused = false;
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex);
                Debug.LogErrorFormat("An error encountered while running a dialog. Exception details: {0}", ex.Message);
            }
        }
    }
}
