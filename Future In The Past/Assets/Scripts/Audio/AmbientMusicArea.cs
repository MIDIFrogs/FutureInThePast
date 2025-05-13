using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MIDIFrogs.FutureInThePast
{
    [RequireComponent(typeof(AudioSource))]
    public class AmbientMusicArea : MonoBehaviour
    {
        private AudioSource audioSource;

        public event EventHandler PlayerEntered;

        public event EventHandler PlayerLeft;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public async UniTaskVoid FadeIn(float duration)
        {
            float elapsed = 0;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(0, 1, elapsed / duration);
                await UniTask.NextFrame();
            }
        }

        public async UniTaskVoid FadeOut(float duration)
        {
            float elapsed = 0;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(1, 0, elapsed / duration);
                await UniTask.NextFrame();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
                PlayerEntered?.Invoke(this, EventArgs.Empty);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
                PlayerLeft?.Invoke(this, EventArgs.Empty);
        }
    }
}
