using System;
using UnityEngine;
using UnityEngine.Audio;

namespace MIDIFrogs.FutureInThePast.UI
{
	public class SoundModeSwitcher : MonoBehaviour
	{
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private bool isFutureMode = true;

        public void Awake()
        {
            SwitchMusic();
        }

        public void LateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                SwitchMusic();
            }
        }

        private void SwitchMusic()
        {
            if (isFutureMode)
            {
                ResetSwitch("Future", "Past");
                isFutureMode = false;
            }
            else
            {
                ResetSwitch("Past", "Future");
                isFutureMode = true;
            }
        }

        private void ResetSwitch(string from, string to)
        {
            Debug.Log($"Switching from {from} to {to}");
            SetAudioMixerVolume(to, 1);
            SetAudioMixerVolume(from, 0.0001f);
            SetAudioMixerVolume(to + "Sfx", 1);
            SetAudioMixerVolume(from + "Sfx", 0.0001f);
        }

        private void SetAudioMixerVolume(string parameterName, float value)
        {
            float dbValue = Mathf.Log10(value) * 20;
            float currentValue;
            mixer.GetFloat(parameterName, out currentValue);
            if (currentValue != dbValue)
            {
                mixer.SetFloat(parameterName, dbValue);
            }
        }
    }
}