using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioObject : MonoBehaviour
{
    private AudioSource _source;
    private GameSettingsHandler _gameSettingsHandler;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }    
    private void Start()
    {
        _gameSettingsHandler = FindObjectOfType<GameSettingsHandler>();
        _gameSettingsHandler.AudioChanged += ChangeState;
        ChangeState(_gameSettingsHandler.Settings.IsAudioOn);
    }

    public void Play(AudioClip clip)
    {
        _source.PlayOneShot(clip);
    }
    public void ChangeState(bool isPlaying)
    {
        _source.mute = !isPlaying;
    }


}
