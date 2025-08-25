using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Level Data Container")]
public class LevelDataContainer : ScriptableObject
{
    public List<LevelData> Datas;
}
