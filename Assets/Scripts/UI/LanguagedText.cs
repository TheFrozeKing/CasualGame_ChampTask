using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguagedText : MonoBehaviour
{
    [SerializeField] private string _englishVariant;
    [SerializeField] private string _russianVariant;
    private int _currentLanguageIndex;
    private Text _text;
    private GameSettingsHandler _gameSettingsHandler;
    private void Awake()
    {
        _text = GetComponent<Text>();
    }
    private void Start()
    {
        _gameSettingsHandler = FindObjectOfType<GameSettingsHandler>();
        _gameSettingsHandler.LanguageChanged += ChangeLanguage;
        _currentLanguageIndex = _gameSettingsHandler.Settings.LanguageIndex;
        ChangeLanguage(_gameSettingsHandler.Settings.LanguageIndex);
    }

    public void ChangeText(string text)
    {
        _text.text = text;
        ChangeLanguage(_currentLanguageIndex);
    }

    public void ChangeLanguage(int languageIndex)
    {
        string oldVariant = languageIndex == 0 ? _russianVariant : _englishVariant;
        string newVariant = languageIndex == 0 ? _englishVariant : _russianVariant;
        _text.text = _text.text.Replace(oldVariant,newVariant);
        _currentLanguageIndex = languageIndex;
    }
}
