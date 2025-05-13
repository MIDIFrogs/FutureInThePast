using UnityEngine;
using UnityEngine.Audio;

namespace MIDIFrogs.FutureInThePast.UI
{
	public class SoundModeSwitcher : TimeChangeObserver
	{
        [SerializeField] private AudioMixer mixer;

        public override void OnEnterFuture() => Enable("Future");

        public override void OnEnterPast() => Enable("Past");

        public override void OnLeaveFuture() => Disable("Future");

        public override void OnLeavePast() => Disable("Past");

        private void Disable(string target)
        {
            SetAudioMixerVolume(target, 0.0001f);
            SetAudioMixerVolume(target + "Sfx", 0.0001f);
        }

        private void Enable(string target)
        {
            SetAudioMixerVolume(target, 1);
            SetAudioMixerVolume(target + "Sfx", 1);
        }

        private void SetAudioMixerVolume(string parameterName, float value)
        {
            float dbValue = Mathf.Log10(value) * 20;
            mixer.GetFloat(parameterName, out float currentValue);
            if (currentValue != dbValue)
            {
                mixer.SetFloat(parameterName, dbValue);
            }
        }
    }
}