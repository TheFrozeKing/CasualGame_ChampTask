using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserGameSettings
{
    public int ResolutionIndex = 2;
    public bool IsWindowed = false;
    public bool IsAudioOn = true;
    public int LanguageIndex = 0;

    public UserGameSettings Clone()
    {
        return (UserGameSettings)MemberwiseClone();
    }
}
