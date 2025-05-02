using UnityEngine;
using System;

namespace MIDIFrogs.FutureInThePast.UI.DialogSystem
{
    [CreateAssetMenu(fileName = "New author", menuName = "Dialogs/Replic Author")]
    [Serializable]
    public class ReplicAuthor : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private string authorName;
        [SerializeField] private Color signColor;
        [SerializeField] private Sprite avatar;

        public string Id => id;

        public string Name => authorName;

        public Color SignColor => signColor;

        public Sprite Avatar => avatar;
    }
}