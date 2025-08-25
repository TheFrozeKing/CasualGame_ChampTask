using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSettingsHandler : MonoBehaviour
{ 
    public UserGameSettings Settings;
    private UserGameSettings _oldSettings;
    public bool IsSaved;
    public event Action OnReset;
    public Action<int> LanguageChanged;
    public Action<bool> AudioChanged;

    private void Awake()
    {
        GameSettingsHandler[] gameSettingsHandlers = FindObjectsOfType<GameSettingsHandler>();
        if (gameSettingsHandlers.Length > 1)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(gameObject);

        if (!Directory.Exists("Resources"))
        {
            Directory.CreateDirectory("Resources");
        }

        Settings = new();

        try
        {
            LoadSettings();
        }
        catch
        {
            SaveSettings();
        }
    }

    public void SaveSettings()
    {
        DataHandler.WriteXml(Settings, nameof(UserGameSettings));
        _oldSettings = Settings.Clone();
        IsSaved = true;
    }

    public void LoadSettings()
    {
        Settings = DataHandler.ReadXml<UserGameSettings>(nameof(UserGameSettings));
        _oldSettings = Settings.Clone();
        IsSaved = true;
    }

    public void ResetSettings()
    {
        Settings = _oldSettings.Clone();
        OnReset?.Invoke();
        IsSaved = true;
    }

    public void FactoryReset()
    {
        _oldSettings = new();
        ResetSettings();
    }
}
