using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Dropdown _resolutionDropdown;
    [SerializeField] private Toggle _windowedToggle;
    [SerializeField] private Toggle _audioToggle;
    [SerializeField] private Dropdown _languageDropdown;
    [SerializeField] private GameObject _saveWarningPanel;
    private GameSettingsHandler _settingsHandler;

    private UserGameSettings _settings => _settingsHandler.Settings;

    private List<Resolution> _resolutions = new()
    {
        new Resolution() {width = 1280, height = 720 },
        new Resolution() {width = 1366, height = 766 },
        new Resolution() {width = 1600, height = 900 },
        new Resolution() {width = 1920, height = 1080 },
    };

    private void Start()
    {
        _settingsHandler = FindObjectOfType<GameSettingsHandler>();
        _settingsHandler.OnReset += OnReset;
        OnReset();
    }
    public void OnReset()
    {
        _windowedToggle.isOn = _settings.IsWindowed;
        _audioToggle.isOn = _settings.IsAudioOn;
        _resolutionDropdown.value = _settings.ResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
        _languageDropdown.value = _settings.LanguageIndex;
        _languageDropdown.RefreshShownValue();

        SetResolution(_resolutions[_settings.ResolutionIndex], !_settings.IsWindowed);

        _settingsHandler.IsSaved = true;
    }

    public void SetResolution(Resolution resolution, bool isFullscreen)
    {
        Screen.SetResolution(resolution.width, resolution.height, isFullscreen);
    }

    public void ChangeResolution(int index)
    {
        _settings.ResolutionIndex = index;
        //SetResolution(_resolutions[index], !_settings.IsWindowed);
        _settingsHandler.IsSaved = false;
    }

    public void ChangeWindowed(bool isOn)
    {
        _settings.IsWindowed = isOn;
        //SetResolution(_resolutions[_settings.ResolutionIndex], isOn);
        _settingsHandler.IsSaved = false;
    }

    public void ChangeAudio(bool isOn)
    {
        _settings.IsAudioOn = isOn;
        _settingsHandler.IsSaved = false;
        //_settingsHandler.AudioChanged?.Invoke(isOn);
    }

    public void ChangeLanguage(int index)
    {
        _settings.LanguageIndex = index;
        _settingsHandler.IsSaved = false;
        //_settingsHandler.LanguageChanged?.Invoke(index);
    }

    public void Apply()
    {
        SetResolution(_resolutions[_settings.ResolutionIndex], !_settings.IsWindowed);
        _settingsHandler.LanguageChanged?.Invoke(_settings.LanguageIndex);
        _settingsHandler.AudioChanged?.Invoke(_settings.IsAudioOn);
        _settingsHandler.IsSaved = true;
    }

    public void TryClose()
    {
        if (_settingsHandler.IsSaved)
        {
            gameObject.SetActive(false);
            return;
        }

        _saveWarningPanel.SetActive(true);
    }
}
