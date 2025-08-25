using System;
using System.Collections.Generic;

[Serializable]
public class NewLevelData
{
    public List<ObjectData> Objects;
    public CarData CarData;
    public int CoinReward = 50;
    public static NewLevelData ConvertFromOld(LevelData convertFrom)
    {
        NewLevelData newLevelData = new();
        newLevelData.Objects = convertFrom.Objects;
        newLevelData.CarData = convertFrom.CarData;
        newLevelData.CoinReward = 50;
        return newLevelData;
    }    
}
