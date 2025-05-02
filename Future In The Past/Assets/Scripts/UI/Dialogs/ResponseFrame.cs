using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FutureInThePast.Quests;
using GluonGui.Dialog;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MIDIFrogs.FutureInThePast.UI.DialogSystem
{
    public class ResponseFrame : MonoBehaviour
    {
        [Header("Dialog properties")]
        [SerializeField] private DialogHistoryEntry historyEntryPrefab;
        [SerializeField] private GameObject historyPanel;
        [SerializeField] private RectTransform historyViewport;
        [SerializeField] private ResponseButton responsePrefab;
        [SerializeField] private GameObject responsePanel;
        [SerializeField] private TMP_Text questionText;

        private int selectedButton = -1;

        public Response Response { get; private set; }

        public IEnumerator WaitForResponse(DialogClip clip, QuestManager quests)
        {
            questionText.text = clip.EndQuestion;
            foreach (Transform child in historyViewport)
            {
                Destroy(child.gameObject);
            }
            foreach (var replic in clip.Replics)
            {
                var replicEntry = Instantiate(historyEntryPrefab, historyViewport);
                replicEntry.PlaceReplic(replic);
            }
            foreach (Transform child in responsePanel.transform)
            {
                Destroy(child.gameObject);
            }
            List<Coroutine> buttonCoroutines = new();
            foreach (var response in clip.Responses)
            {
                var button = Instantiate(responsePrefab, responsePanel.transform);
                buttonCoroutines.Add(StartCoroutine(button.WaitForClick(response, quests)));
            }
            yield return WaitForCoroutines(buttonCoroutines);
            Response = clip.Responses.ElementAtOrDefault(selectedButton);
        }

        private IEnumerator WaitForCoroutines(List<Coroutine> coroutines)
        {
            while (coroutines.Count > 0)
            {
                // Check if any coroutine is still running
                for (int i = coroutines.Count - 1; i >= 0; i--)
                {
                    // If the coroutine is done, remove it from the list
                    if (coroutines[i] == null)
                    {
                        selectedButton = i;
                        break;
                    }
                }
                yield return null; // Wait for the next frame
            }
        }

        public void OnHistoryOpen()
        {
            historyPanel.SetActive(true);
        }

        public void OnHistoryClose()
        {
            historyPanel.SetActive(false);
        }
    }
}
