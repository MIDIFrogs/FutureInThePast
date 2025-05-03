using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace MIDIFrogs.FutureInThePast.UI.HUD
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private PlayerLife trackedPlayerLife;
        [SerializeField] private TMP_Text healthLabel;

        [Header("Animation")]
        [SerializeField] private Image healthIcon;
        [SerializeField] private float normalHeartbeatDuration = 0.5f;
        [SerializeField] private float lowHealthHeartbeatDuration = 0.2f;
        [SerializeField] private float shakeDuration = 0.2f;
        [SerializeField] private float shakeMagnitude = 0.1f;

        private Coroutine heartbeatCoroutine;

        private void Update()
        {
            healthLabel.text = trackedPlayerLife.Health.ToString();

            // Start the heartbeat animation if it's not already running
            heartbeatCoroutine ??= StartCoroutine(HeartbeatAnimation());
        }

        private IEnumerator HeartbeatAnimation()
        {
            while (true)
            {
                // Determine the heartbeat speed based on health
                float heartbeatDuration = trackedPlayerLife.Health == 1 ? lowHealthHeartbeatDuration : normalHeartbeatDuration;

                // Scale the icon to simulate a heartbeat
                Vector3 originalScale = healthIcon.transform.localScale;
                Vector3 targetScale = originalScale * 1.1f;

                // Animate the heartbeat
                float elapsedTime = 0f;
                while (elapsedTime < heartbeatDuration)
                {
                    healthIcon.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / heartbeatDuration);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // Return to original scale
                elapsedTime = 0f;
                while (elapsedTime < heartbeatDuration)
                {
                    healthIcon.transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / heartbeatDuration);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // If health is 1, add a shake effect
                if (trackedPlayerLife.Health == 1)
                {
                    StartCoroutine(ShakeIcon());
                    healthLabel.color = Color.red;
                }

                // Wait a bit before the next heartbeat
                yield return new WaitForSeconds(heartbeatDuration);
            }
        }

        private IEnumerator ShakeIcon()
        {
            Vector3 originalPosition = healthIcon.transform.localPosition;
            float elapsedTime = 0f;

            while (elapsedTime < shakeDuration)
            {
                float xOffset = Random.Range(-shakeMagnitude, shakeMagnitude);
                float yOffset = Random.Range(-shakeMagnitude, shakeMagnitude);
                healthIcon.transform.localPosition = new Vector3(originalPosition.x + xOffset, originalPosition.y + yOffset, originalPosition.z);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Reset position
            healthIcon.transform.localPosition = originalPosition;
        }
    }
}
