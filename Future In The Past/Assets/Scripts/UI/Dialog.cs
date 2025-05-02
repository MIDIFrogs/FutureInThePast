using System.Collections.Generic;
using UnityEngine;
using System;

namespace MIDIFrogs.FutureInThePast.DialogSystem
{
    [Serializable]
    [CreateAssetMenu(fileName = "New dialog", menuName = "Dialogs/Dialog")]
    public class Dialog : ScriptableObject
    {

        [SerializeField] private List<Replic> dialogContent;

        public IReadOnlyList<Replic> DialogContent => dialogContent;
    }
}
