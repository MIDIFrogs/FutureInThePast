using System;
using System.Collections.Generic;
using FutureInThePast.Quests;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast.UI.DialogSystem
{
    [CreateAssetMenu(fileName = "New dialog clip", menuName = "Dialogs/Dialog clip")]
    public class DialogClip : ScriptableObject
    {
        [SerializeField] private List<Replic> replics;
        [SerializeField] private List<Response> responses;

        public IReadOnlyList<Replic> Replics => replics;

        public IReadOnlyList<Response> Responses => responses;
    }
}
