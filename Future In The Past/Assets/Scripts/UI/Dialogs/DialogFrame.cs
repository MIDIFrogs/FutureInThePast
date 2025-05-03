using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace MIDIFrogs.FutureInThePast.UI.DialogSystem
{
    public class DialogFrame : MonoBehaviour
    {
        [SerializeField] private TMP_Text replicText;
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                OnNextFrame();
            }
        }

        /// <summary>
        /// Animates the text writing it asynchronously char-by-char each <see langword="40"/> / <paramref name="textSpeed"/> milliseconds.
        /// </summary>
        /// <param name="text">Text to write.</param>
        /// <param name="textSpeed">Text speed to speed up or slow down the reading.</param>
        /// <param name="token">Token to cancel an animation.</param>
        public async Task AnimationReplic(string text, float textSpeed, CancellationToken token)
        {
            for (int i = 0; i <= text.Length; i++)
            {
                token.ThrowIfCancellationRequested();
                replicText.text = text[..i];
                await Task.Delay(TimeSpan.FromMilliseconds(40) / textSpeed);
            }

            // Assuming each 100 characters in text should be read in 1.5 seconds.
            float textReadCoefficient = text.Length / 100f;
            // For autoplay to read after text
            await Task.Delay(TimeSpan.FromSeconds(1.5) * textReadCoefficient);
        }

        public async Task AnimateImage(Image image, float fromAlpha, float toAlpha, float speed, CancellationToken token)
        {
            for (int i = 0; i < 100; i++)
            {
                token.ThrowIfCancellationRequested();
                float alpha = Mathf.Lerp(fromAlpha, toAlpha, i / 100f);
                image.color = new(1, 1, 1, alpha);
                await Task.Delay(TimeSpan.FromMilliseconds(3) / speed);
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
            replicText.fontStyle = replic.FontStyle;
            var animation = AnimationReplic(replic.Message, textSpeed, cancelPreTask.Token);
            authorName.text = replic.Author.Name;
            authorAvatar.sprite = replic.Author.Avatar;
            authorName.color = replic.Author.SignColor;
            Task fadeOut = Task.CompletedTask, fadeIn = Task.CompletedTask;
            // Animate splash screens
            oldSplash.sprite = splash.sprite;
            splash.sprite = replic.FrameSplash;

            if (splash.sprite != null)
            {
                splash.color = new Color(1, 1, 1, 0);
                fadeIn = AnimateImage(splash, 0, 1, textSpeed, cancelPreTask.Token);
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
                    fadeOut = AnimateImage(oldSplash, 1, 0, textSpeed, cancelPreTask.Token);
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