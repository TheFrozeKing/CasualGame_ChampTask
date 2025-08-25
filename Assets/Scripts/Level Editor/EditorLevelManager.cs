using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class EditorLevelManager : MonoBehaviour
{
    [SerializeField] private LevelDataContainer _container;
    [SerializeField] private LevelPanel _levelPanelPrefab;
    [SerializeField] private ObjectBuilder _builder;
    private List<LevelPanel> _panels = new();
    private LevelPanel _chosenLevel;

    [Space]
    [SerializeField] private List<Toggle> _modelToggles;
    [SerializeField] private Toggle _spikedWheelsToggle;
    [SerializeField] private Toggle _rocketToggle;
    [SerializeField] private Toggle _propellerToggle;
    [SerializeField] private Toggle _wingsToggle;

    private List<int> _decorationIds = new()
    {
        1,9,5,7,11,14
    };

    private void Awake()
    {
        LoadLevelsExternal();
        for(int i = 0; i < transform.childCount; i++) 
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        
        for(int i = 0; i < _container.Datas.Count; i++)
        {
            LevelPanel newPanel = Instantiate(_levelPanelPrefab, transform);
            newPanel.transform.GetComponent<Button>().onClick.AddListener(() => ChooseLevel(newPanel));
            newPanel.Data = _container.Datas[i];
            newPanel.ChangeName("Level " + (i + 1));
            _panels.Add(newPanel);
        }
    }

    public void ChooseLevel(LevelPanel panel)
    {
        Debug.Log(panel);
        _chosenLevel = panel;
    }

    public void AddLevel()
    {
        LevelPanel newPanel = Instantiate(_levelPanelPrefab, transform);
        newPanel.transform.GetComponent<Button>().onClick.AddListener(() => ChooseLevel(newPanel));
        _panels.Add(newPanel);
        newPanel.Data = new();
        newPanel.ChangeName("Level " + _panels.Count);
        _container.Datas.Add(newPanel.Data);
    }

    public void RemoveLevel()
    {
        _container.Datas.Remove(_chosenLevel.Data);
        _panels.Remove(_chosenLevel);
        Destroy(_chosenLevel.gameObject);
        _chosenLevel = null;
        ReevaluateNames();
    }

    public void SaveLevel()
    {
        List<GameObject> objects = _builder.GetAllObjects();
        List<ObjectData> objectDatas = new();
        foreach (GameObject placedObject in objects)
        {
            ObjectData data = new();
            data.Position = SerializableVector3.ConvertFrom(placedObject.transform.position);
            data.Rotation = SerializableVector3.ConvertFrom(placedObject.transform.rotation.eulerAngles);
            data.Scale = SerializableVector3.ConvertFrom(placedObject.transform.localScale);
            data.ObjectType = (int)ObjectDeterminator.DetermineType(placedObject);
            objectDatas.Add(data);
        }

        int decorationCount = 0;
        foreach(var objectData in objectDatas)
        {
            if (_decorationIds.Contains(objectData.ObjectType))
            {
                decorationCount++;
            }
        }

        if(decorationCount < 4)
        {
            Debug.Log("Saving failed, less than 4 decorations");
            return;
        }

        CarData carData = new CarData();
        int i = 0;
        while(i < _modelToggles.Count)
        {
            if (_modelToggles[i].isOn)
            {
                break;
            }
            i++;
        }
        carData.CarModelIndex = i;
        carData.HasSpikedWheels = _spikedWheelsToggle.isOn;
        carData.HasWings = _wingsToggle.isOn;
        carData.HasPropeller = _propellerToggle.isOn;
        carData.HasRocket = _rocketToggle.isOn;

        _chosenLevel.Data.Objects = objectDatas;
        _chosenLevel.Data.CarData = carData;
     }

    public void LoadLevel()
    {
        _builder.Place(_chosenLevel.Data.Objects);
        _spikedWheelsToggle.isOn = _chosenLevel.Data.CarData.HasSpikedWheels;
        _wingsToggle.isOn = _chosenLevel.Data.CarData.HasWings;
        _propellerToggle.isOn = _chosenLevel.Data.CarData.HasPropeller;
        _rocketToggle.isOn = _chosenLevel.Data.CarData.HasRocket;

        for(int i = 0; i < _modelToggles.Count; i++)
        {
            _modelToggles[i].isOn = false;
            if(i == _chosenLevel.Data.CarData.CarModelIndex)
            {
                _modelToggles[i].isOn = true;
            }
        }
    }

    public void ReevaluateNames()
    {
        for(int i = 0; i < _panels.Count; i++)
        {
            _panels[i].ChangeName("Level " + (i + 1));
        }
    }


    public void SaveLevelsExternal()
    {
        DataHandler.WriteXml(_container.Datas, "OldLevelDatas");
    }

    public void LoadLevelsExternal()
    {
        _container.Datas = DataHandler.ReadXml<List<LevelData>>("OldLevelDatas");
    }
    private void OnApplicationQuit()
    {
        SaveLevelsExternal();
    }
}
