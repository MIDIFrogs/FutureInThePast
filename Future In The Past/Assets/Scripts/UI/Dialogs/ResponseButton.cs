using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using FutureInThePast.Quests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MIDIFrogs.FutureInThePast.UI.DialogSystem
{
    public class ResponseButton : MonoBehaviour
    {
        [SerializeField] private string requirementsLabel;

        [Header("Dialog properties")]
        [SerializeField] private TMP_Text responseText;
        [SerializeField] private TMP_Text RequirementsText;
        [SerializeField] private Button button;

        private TaskCompletionSource<bool> clickTcs = new();


        public async Task WaitForClick(Response response)
        {
            responseText.text = response.Text;
            Debug.Log($"Found requirements: {string.Join(',', response.Requirements.Select(x => x.Tag + ":" + x.IsCompleted))}");
            button.interactable = response.Requirements.All(x => x.IsCompleted);
            if (!button.interactable)
            {
                RequirementsText.text = requirementsLabel + string.Join(Environment.NewLine, response.Requirements.Where(x => !x.IsCompleted).Select(x => x.Description));
            }
            else
            {
                RequirementsText.text = string.Empty;
            }
            await clickTcs.Task;
        }

        public void Click()
        {
            clickTcs.SetResult(true);
        }
    }
}
