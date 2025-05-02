using System.Collections.Generic;
using UnityEngine;
using System;

namespace MIDIFrogs.FutureInThePast.UI.DialogSystem
{
    [Serializable]
    [CreateAssetMenu(fileName = "New dialog", menuName = "Dialogs/Dialog")]
    public class Dialog : ScriptableObject
    {

        [SerializeField] private DialogClip startupClip;

        public DialogClip StartupClip => startupClip;
    }
}
