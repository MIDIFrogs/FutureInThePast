using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<Response> WaitForResponse(DialogClip clip)
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
            List<Task> buttonTasks = new();
            foreach (var response in clip.Responses)
            {
                var button = Instantiate(responsePrefab, responsePanel.transform);
                buttonTasks.Add(button.WaitForClick(response));
            }
            var clicked = await Task.WhenAny(buttonTasks);
            int selectedButton = buttonTasks.IndexOf(clicked);
            Debug.Log($"Selected button index: {selectedButton}");
            return clip.Responses.ElementAtOrDefault(selectedButton);
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
