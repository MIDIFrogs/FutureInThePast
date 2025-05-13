using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using MIDIFrogs.FutureInThePast.Quests.Dialogs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MIDIFrogs.FutureInThePast.UI.Dialogs
{
    public class ResponseButton : MonoBehaviour
    {
        [SerializeField] private string requirementsLabel;

        [Header("Dialog properties")]
        [SerializeField] private TMP_Text responseText;
        [SerializeField] private TMP_Text RequirementsText;
        [SerializeField] private Button button;

        private UniTaskCompletionSource<bool> clickTcs = new();


        public async UniTask WaitForClick(Response response)
        {
            responseText.text = response.Text;
            Debug.Log($"Found requirements: {string.Join(',', response.Requirements.Select(x => x.Quest.Tag + ":" + x.Quest.IsCompleted))}");
            button.interactable = response.Requirements.All(x => x.Quest.IsCompleted);
            if (!button.interactable)
            {
                RequirementsText.text = requirementsLabel + string.Join(Environment.NewLine, response.Requirements.Where(x => !x.Quest.IsCompleted).Select(x => x.Quest.Description));
            }
            else
            {
                RequirementsText.text = string.Empty;
            }
            await clickTcs.Task;
        }

        public void Click()
        {
            clickTcs.TrySetResult(true);
        }
    }
}
