using System;
using TMPro;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast.DialogSystem
{
    [Serializable]
    public class Replic
    {
        [SerializeField] private ReplicAuthor author;
        [SerializeField][Multiline] private string message;
        [SerializeField] private AudioClip voice;
        [SerializeField] private Sprite frameSplash;
        [SerializeField] private FontStyles fontStyle;

        public ReplicAuthor Author => author;

        public AudioClip Voice => voice;

        public string Message => message;

        public Sprite FrameSplash => frameSplash;
        
        public FontStyles FontStyle => fontStyle;
    }
}
