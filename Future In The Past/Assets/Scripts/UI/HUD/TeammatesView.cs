using UnityEngine;
using UnityEngine.UI;
using MIDIFrogs.FutureInThePast.Quests;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MIDIFrogs.FutureInThePast.UI.HUD
{
    public class TeammatesView : MonoBehaviour
    {
        [SerializeField] private List<TeammateDetails> teammates;
        [SerializeField] private Image imagePrefab;

        private Dictionary<TriggerConfig, Image> loadedImages = new();

        private void Update()
        {
            foreach (var teammate in teammates.Where(x => !loadedImages.ContainsKey(x.Trigger)))
            {
                if (teammate.Trigger.Quest.IsCompleted)
                {
                    var image = Instantiate(imagePrefab, transform);
                    image.sprite = teammate.Avatar;
                    loadedImages.Add(teammate.Trigger, image);
                }
            }
        }
    }

    [Serializable]
    public class TeammateDetails
    {
        public TriggerConfig Trigger;
        public Sprite Avatar;
    }
}
