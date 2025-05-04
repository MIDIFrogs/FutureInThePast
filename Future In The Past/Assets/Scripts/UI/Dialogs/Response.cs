using System;
using System.Collections.Generic;
using System.Linq;
using MIDIFrogs.FutureInThePast.Quests;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast.UI.DialogSystem
{
    [Serializable]
    public class Response
    {
        [SerializeField] private string text;
        [SerializeField] private List<TriggerConfig> requirements;
        [SerializeField] private TriggerConfig selectionTrigger;
        [SerializeField] private bool shouldSetTrigger = true;
        [SerializeField] private DialogClip continuation;
        [SerializeField] private bool endsGame;

        public string Text => text;

        public IEnumerable<QuestTrigger> Requirements => requirements.Select(x => x.Quest);

        public QuestTrigger SelectionTrigger => selectionTrigger?.Quest;

        public bool ShouldSetTrigger => shouldSetTrigger;

        public DialogClip Continuation => continuation;

        public bool EndsGame => endsGame;
    }
}
