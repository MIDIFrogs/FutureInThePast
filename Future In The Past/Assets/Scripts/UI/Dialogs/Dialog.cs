using System.Collections.Generic;
using UnityEngine;
using System;

namespace MIDIFrogs.FutureInThePast.UI.DialogSystem
{
    [Serializable]
    [CreateAssetMenu(fileName = "New dialog", menuName = "Dialogs/Dialog")]
    public class Dialog : ScriptableObject
    {

        [SerializeField] private List<DialogClip> clips;

        public List<DialogClip> Clips => clips;
    }
}
