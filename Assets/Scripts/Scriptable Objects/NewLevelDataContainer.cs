using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable Objects/New Level Data Container")]
public class NewLevelDataContainer : ScriptableObject
{
    public List<NewLevelData> Datas = new();
}
