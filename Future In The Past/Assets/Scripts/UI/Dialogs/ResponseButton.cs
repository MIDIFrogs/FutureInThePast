using System;
using System.Collections;
using System.Linq;
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

        private bool isClicked;

        public IEnumerator WaitForClick(Response response, QuestManager quests)
        {
            responseText.text = response.Text;
            button.enabled = response.Requirements.All(x => quests.IsTrigger(x.Tag));
            if (!button.enabled)
            {
                RequirementsText.text = requirementsLabel + string.Join(Environment.NewLine, response.Requirements.Where(x => !x.IsCompleted).Select(x => x.Description));
            }
            isClicked = false;
            yield return new WaitUntil(() => isClicked);
        }

        public void Click()
        {
            isClicked = true;
        }
    }
}
