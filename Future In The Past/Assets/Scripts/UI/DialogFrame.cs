using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections;

namespace MIDIFrogs.FutureInThePast.DialogSystem
{
    public class DialogFrame : MonoBehaviour
    {
        [SerializeField] private TMP_Text replicaText;
        [SerializeField] private TMP_Text authorName;
        [SerializeField] private Image authorAvatar;
        [SerializeField] private AudioSource voiceOver;
        [SerializeField] private Image splash;
        [SerializeField] private Image oldSplash;

        private TaskCompletionSource<bool> waitButton;

        public void OnNextFrame()
        {
            waitButton?.SetResult(true);
        }

        /// <summary>
        /// Animates the text writing it asynchronously char-by-char each <see langword="40"/> / <paramref name="textSpeed"/> milliseconds.
        /// </summary>
        /// <param name="text">Text to write.</param>
        /// <param name="textSpeed">Text speed to speed up or slow down the reading.</param>
        /// <param name="token">Token to cancel an animation.</param>
        public IEnumerator AnimateReplica(string text, float textSpeed)
        {
            for (int i = 0; i <= text.Length; i++)
            {
                replicaText.text = text[..i];
                yield return new WaitForSeconds(40f / textSpeed / 1000f); // Convert milliseconds to seconds
            }

            // Assuming each 100 characters in text should be read in 1.5 seconds.
            float textReadCoefficient = text.Length / 100f;
            yield return new WaitForSeconds(1.5f * textReadCoefficient);
        }

        public IEnumerator AnimateImage(Image image, float fromAlpha, float toAlpha, float speed)
        {
            for (int i = 0; i < 100; i++)
            {
                float alpha = Mathf.Lerp(fromAlpha, toAlpha, i / 100f);
                image.color = new Color(1, 1, 1, alpha);
                yield return new WaitForSeconds(3f / speed / 1000f); // Convert milliseconds to seconds
            }
        }

        /// <summary>
        /// Shows the replic in the dialog frame.
        /// </summary>
        /// <param name="replic">Replic to show.</param>
        /// <param name="textSpeed">Reading speed coefficient for the text.</param>
        /// <param name="autoplay">Set to <see langword="true"/> if the replic should be autoplayed.</param>
        public async Task ShowText(Replic replic, float textSpeed, bool autoplay)
        {
            // Prepare the token to cancel the animation.
            CancellationTokenSource cancelPreTask = new();

            voiceOver.clip = null;
            // Fill up the frame data
            if (replic.Voice != null)
            {
                voiceOver.clip = replic.Voice;
                voiceOver.Play();
            }
            replicaText.fontStyle = replic.FontStyle;
            Coroutine animation = StartCoroutine(AnimateReplica(replic.Message, textSpeed));
            authorName.text = replic.Author.Name;
            authorAvatar.sprite = replic.Author.Avatar;
            authorName.color = replic.Author.SignColor;
            Coroutine fadeOut = null, fadeIn = null;
            // Animate splash screens
            oldSplash.sprite = splash.sprite;
            splash.sprite = replic.FrameSplash;

            if (splash.sprite != null)
            {
                splash.color = new Color(1, 1, 1, 0);
                fadeIn = StartCoroutine(AnimateImage(splash, 0, 1, textSpeed));
                if (oldSplash.sprite != null)
                {
                    oldSplash.color = Color.white;
                }
            }
            else
            {
                splash.color = default;
                if (oldSplash.sprite != null)
                {
                    oldSplash.color = Color.white;
                    fadeOut = StartCoroutine(AnimateImage(oldSplash, 1, 0, textSpeed));
                }
                else
                {
                    oldSplash.color = default;
                }
            }

            

            // Wait for the button click
            waitButton = new();

            if (autoplay)
            {
                yield return animation;
                yield return fadeIn;
                yield return fadeOut;
                await Task.WhenAny(Task.WhenAll(animation, fadeIn, fadeOut), waitButton.Task);
            }
            else
            {
                await waitButton.Task;
            }

            // Cancel incompleted animations
            cancelPreTask.Cancel();
            if (replic.Voice != null)
            {
                voiceOver.Stop();
            }
        }
    }
}