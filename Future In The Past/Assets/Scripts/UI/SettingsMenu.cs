using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace MIDIFrogs.FutureInThePast
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown qualityDropdown;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private Slider voiceoverVolumeSlider;
        [SerializeField] private Toggle fullscreenToggle;
        [SerializeField] private Toggle vSyncToggle;
        [SerializeField] private GameObject panel;

        [SerializeField] private AudioMixer mixer;

        private List<Resolution> supportedResolutions;

        private void Start()
        {
            supportedResolutions = new List<Resolution>(Screen.resolutions.OrderBy(x => x.width * x.height));
            RestoreSettings();
            ApplySettings();

            masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
            voiceoverVolumeSlider.onValueChanged.AddListener(OnVoiceoverVolumeChanged);
        }

        public void Open()
        {
            panel.SetActive(true);
            RestoreSettings();
        }

        public void Close()
        {
            panel.SetActive(false);
        }

        public void ReadSettings()
        {
            // Read settings from PlayerPrefs
            qualityDropdown.value = PlayerPrefs.GetInt("QualityLevel", 2); // Default to medium
            fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1; // Default to true
            vSyncToggle.isOn = PlayerPrefs.GetInt("VSync", 1) == 1; // Default to true
            masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f); // Default to 1
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f); // Default to 1
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SfxVolume", 1f); // Default to 1
            voiceoverVolumeSlider.value = PlayerPrefs.GetFloat("VoiceoverVolume", 1f); // Default to 1

            // Populate resolution dropdown
            resolutionDropdown.ClearOptions();
            List<string> options = new();
            foreach (var resolution in supportedResolutions)
            {
                options.Add(resolution.ToString());
            }
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionIndex", 0); // Default to first resolution
        }

        public void RefreshSettings()
        {
            // Update UI elements with the current settings
            qualityDropdown.RefreshShownValue();
            resolutionDropdown.RefreshShownValue();
            fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
            vSyncToggle.isOn = PlayerPrefs.GetInt("VSync", 1) == 1;
            masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SfxVolume", 1f);
            voiceoverVolumeSlider.value = PlayerPrefs.GetFloat("VoiceoverVolume", 1f);
        }

        public void ApplySettings()
        {
            // Apply quality settings
            if (QualitySettings.GetQualityLevel() != qualityDropdown.value)
            {
                QualitySettings.SetQualityLevel(qualityDropdown.value);
            }

            // Apply fullscreen settings
            if (Screen.fullScreen != fullscreenToggle.isOn)
            {
                Screen.fullScreen = fullscreenToggle.isOn;
            }

            // Apply VSync settings
            if (QualitySettings.vSyncCount != (vSyncToggle.isOn ? 1 : 0))
            {
                QualitySettings.vSyncCount = vSyncToggle.isOn ? 1 : 0;
            }

            // Set the resolution only if it has changed
            Resolution selectedResolution = supportedResolutions[resolutionDropdown.value];
            if (Screen.currentResolution.width != selectedResolution.width ||
                Screen.currentResolution.height != selectedResolution.height ||
                Screen.fullScreen != fullscreenToggle.isOn)
            {
                Screen.SetResolution(selectedResolution.width, selectedResolution.height, fullscreenToggle.isOn);
            }

            // Set audio mixer volumes
            SetAudioMixerVolume("MasterVolume", masterVolumeSlider.value);
            SetAudioMixerVolume("MusicVolume", musicVolumeSlider.value);
            SetAudioMixerVolume("SfxVolume", sfxVolumeSlider.value);
            SetAudioMixerVolume("VoiceoverVolume", voiceoverVolumeSlider.value);

            // Save settings to PlayerPrefs
            PlayerPrefs.SetInt("QualityLevel", qualityDropdown.value);
            PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);
            PlayerPrefs.SetInt("VSync", vSyncToggle.isOn ? 1 : 0);
            PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
            PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
            PlayerPrefs.SetFloat("SfxVolume", sfxVolumeSlider.value);
            PlayerPrefs.SetFloat("VoiceoverVolume", voiceoverVolumeSlider.value);
            PlayerPrefs.SetInt("ResolutionIndex", resolutionDropdown.value);
            PlayerPrefs.Save();
        }

        private void OnMasterVolumeChanged(float value)
        {
            SetAudioMixerVolume("MasterVolume", value);
        }

        private void OnMusicVolumeChanged(float value)
        {
            SetAudioMixerVolume("MusicVolume", value);
        }

        private void OnSFXVolumeChanged(float value)
        {
            SetAudioMixerVolume("SfxVolume", value);
        }

        private void OnVoiceoverVolumeChanged(float value)
        {
            SetAudioMixerVolume("VoiceoverVolume", value);
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


        public void RestoreSettings()
        {
            ReadSettings();
            RefreshSettings();
        }
    }
}
