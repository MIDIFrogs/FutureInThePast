using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

namespace MIDIFrogs.FutureInThePast.UI
{
    public class ImageFader : MonoBehaviour
    {
        [SerializeField] private Image[] images;
        [SerializeField] private float fadeDuration = 1f;
        [SerializeField] private float endDelay;
        [SerializeField] private UnityEvent onSlideshowEnd;

        [SerializeField] private bool startOnAwake;

        private void Start()
        {
            if (startOnAwake)
            {
                StartCoroutine(FadeInImages());
            }
        }

        private IEnumerator FadeInImages()
        {
            foreach (Image img in images)
            {
                yield return StartCoroutine(FadeIn(img));
            }
            for (float delay = 0; delay < endDelay; delay += Time.unscaledDeltaTime)
            {
                if (InputHandler.IsAnyKeyPressed())
                    break;
                yield return new WaitForEndOfFrame();
            }
            onSlideshowEnd.Invoke();
        }

        private IEnumerator FadeIn(Image img)
        {
            Color color = img.color;
            color.a = 0;
            img.color = color;

            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
                img.color = color;
                if (InputHandler.IsAnyKeyPressed())
                    break;
                yield return null;
            }

            color.a = 1;
            img.color = color;
        }
    }

    /// <summary>
    /// Represents a unified input service.
    /// </summary>
    public static class InputHandler
    {
        /// <summary>
        /// Gets current input point (if available).
        /// </summary>
        /// <returns>Main input position in pixel coordinates.</returns>
        public static Vector2 GetInputPosition()
        {
            if (Input.touchCount > 0)
            {
                return Input.GetTouch(0).position;
            }
            else
            {
                return Input.mousePosition;
            }
        }

        /// <summary>
        /// Checks if any of inputs is currently pressed.
        /// </summary>
        /// <returns><see langword="true"/> if any touches or mouse click detected; otherwise <see langword="false"/></returns>
        public static bool IsInputPressed()
        {
            if (Input.touchCount > 0)
            {
                return Input.GetTouch(0).phase == TouchPhase.Began ||
                       Input.GetTouch(0).phase == TouchPhase.Moved ||
                       Input.GetTouch(0).phase == TouchPhase.Stationary;
            }
            else
            {
                return Input.GetMouseButton(0);
            }
        }

        /// <summary>
        /// Checks if any key is currently pressed or input is pressed at the current frame.
        /// </summary>
        /// <returns><see langword="true"/> if any input key is pressed; otherwise <see langword="false"/>.</returns>
        public static bool IsAnyKeyPressed() => Input.anyKeyDown || IsInputPressed();
    }
}