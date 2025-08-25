using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanel : MonoBehaviour
{
    [SerializeField] private LanguagedText _nameText;
    public LevelData Data;
    
    public void ChangeName(string name)
    {
        _nameText.ChangeText(name);
    }
}
