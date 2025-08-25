using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataInitializer : MonoBehaviour
{
    [SerializeField] private LanguageDataContainer _languageDataContainer;
    [SerializeField] private NewLevelDataContainer _newLevelDataContainer;
    [SerializeField] private LevelDataContainer _oldLevelDataContainer;
    private UserGameSettings _baseSettings;
    private List<StoreItemData> _storeItemDatas;
    private List<CarItemData> _carItemDatas;
    private void Awake()
    {
        _baseSettings = new();
        _storeItemDatas = new();
        _carItemDatas = new();

        if (!Directory.Exists("Resources"))
        {
            Directory.CreateDirectory("Resources");
        }
        try
        {
            LoadData();
        }
        catch
        {
            CreateData();
        }
    }
    public void CreateData()
    {
        DataHandler.WriteXml(new UserGameSettings(), "BaseGameSettings");
        _storeItemDatas = new()
        {
            new StoreItemData() {Reward = 150 },
            new StoreItemData() {Reward = 450 },
            new StoreItemData() {Reward = 1000 },
            new StoreItemData() {Reward = 100 },
            new StoreItemData() {Reward = 100 },
            new StoreItemData() {Reward = 150 },
        };
        DataHandler.WriteXml(_storeItemDatas, "StoreConfig");
        _carItemDatas = new()
        {
            new CarItemData() {Speed = 5, Name = "Propeller" },
            new CarItemData() {Speed = 5, Name = "Wheel" },
            new CarItemData() {Speed = 5, Name = "Spiked Wheel" },
            new CarItemData() {Speed = 5, Name = "Wings" },
            new CarItemData() {Speed = 5, Name = "Rocket" },
        };
        DataHandler.WriteXml(_carItemDatas, "ItemSettings");

        List<string> englishData = new() { "Level", "Play" };
        List<string> russianData = new() { "Уровень", "Играть" };
        LanguageData languageData = new() { English = englishData, Russian = russianData };
        DataHandler.WriteJson(languageData, "LanguageData");
    }

    public void LoadData()
    {
        _baseSettings = DataHandler.ReadXml<UserGameSettings>("BaseGameSettings");
        _storeItemDatas = DataHandler.ReadXml<List<StoreItemData>>("StoreConfig");
        _carItemDatas = DataHandler.ReadXml<List<CarItemData>>("ItemSettings");
        _languageDataContainer.Data = DataHandler.ReadJson<LanguageData>("LanguageData");
        CreateNewLevelDatas();
    }

    private void OnApplicationQuit()
    {
        CreateNewLevelDatas();
    }
    public void CreateNewLevelDatas()
    {
        List<NewLevelData> newDatas = new();
        foreach(var oldData in _oldLevelDataContainer.Datas)
        {
            newDatas.Add(NewLevelData.ConvertFromOld(oldData));
        }

        _newLevelDataContainer.Datas = newDatas;
        DataHandler.WriteXml(newDatas, "LevelDatas");
    }

}
