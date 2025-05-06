using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using MIDIFrogs.FutureInThePast.Quests;
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

        public async UniTask<Response> WaitForResponse(DialogClip clip)
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
            List<UniTask> buttonTasks = new();
            foreach (var response in clip.Responses)
            {
                var button = Instantiate(responsePrefab, responsePanel.transform);
                buttonTasks.Add(button.WaitForClick(response));
            }
            int selectedButton = await UniTask.WhenAny(buttonTasks);
            Debug.Log($"Selected button index: {selectedButton}");
            return clip.Responses.ElementAt(selectedButton);
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
