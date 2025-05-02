using System.Collections;
using FutureInThePast.Quests;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast.UI.DialogSystem
{
    public class DialogPlayer : MonoBehaviour
    {
        [SerializeField] private PauseManager pauseMenu;
        [SerializeField] private QuestManager quests;

        [Header("Dialog properties")]
        [SerializeField] private float textSpeed;
        [SerializeField] private bool autoplay;
        [SerializeField] private DialogFrame dialogFrame;
        [SerializeField] private ResponseFrame responseFrame;

        public float TextSpeed => textSpeed;

        public bool Autoplay => autoplay;

        /// <summary>
        /// Starts a dialog and waits for the completion.
        /// </summary>
        /// <param name="dialog">Dialog to read.</param>
        public IEnumerator StartDialog(Dialog dialog)
        {
            pauseMenu.IsPaused = true;

            var clip = dialog.StartupClip;
            while (clip != null)
            {
                dialogFrame.gameObject.SetActive(true);
                foreach (var replic in clip.Replics)
                    yield return dialogFrame.ShowText(replic, TextSpeed, Autoplay);
                dialogFrame.gameObject.SetActive(false);
                if (clip.Responses.Count > 0)
                {
                    responseFrame.gameObject.SetActive(true);
                    yield return responseFrame.WaitForResponse(clip, quests);
                    var response = responseFrame.Response;
                    quests.SetTrigger(response.SelectionTrigger.Tag);
                    clip = response.Continuation;
                    responseFrame.gameObject.SetActive(false);
                }
            }
            pauseMenu.IsPaused = false;
        }
    }
}
