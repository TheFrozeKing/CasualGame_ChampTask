using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class GameLevelManager : MonoBehaviour
{
    [SerializeField] private LevelDataContainer _container;
    [SerializeField] private NewLevelDataContainer _newContainer;
    [SerializeField] private ObjectBuilder _builder;
    [SerializeField] private LanguagedText _levelText;
    [SerializeField] private Text _levelCoinRewardText;
    [SerializeField] private CarHandler _carHandler;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _abilityPanel;
    [SerializeField] private GameObject _levelFailedPanel;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private UserDataHandler _userDataHandler;
    private Coroutine _loseCoroutine;
    private int _currentLevel = 0;

    public void ShowStartButton() => _startButton.SetActive(true);

    public void StartLevel()
    {
        _abilityPanel.SetActive(true);
        _startButton.SetActive(false);
        Car car = FindObjectOfType<Car>();
        car.StartMovement();
        _cameraController.FollowObject(car.transform);
        FindObjectOfType<AbilityFrameHandler>().SpawnFrames(new());
        _loseCoroutine = StartCoroutine(LoseTimer());
        _levelCoinRewardText.text = _newContainer.Datas[_currentLevel % _newContainer.Datas.Count].CoinReward.ToString();
       
    }
    private void Start()
    {
        LoadLevelsExternal();
        _currentLevel = _userDataHandler.Data.LastCompletedLevel;
    }
    public void ReloadLevel()
    {
        LoadLevel(_currentLevel);
    }

    public void LoadNextLevel()
    {
        _currentLevel++;
        LoadLevel(_currentLevel);
    }

    public void LoadLevel(int index)
    {
        if (_loseCoroutine != null)
        {
            StopCoroutine(_loseCoroutine);
        }
        _cameraController.ResetPosition();
        _abilityPanel.SetActive(false);
        _builder.Place(_container.Datas[index % _container.Datas.Count].Objects);
        _levelText.ChangeText("LEVEL " + ((index % _container.Datas.Count) + 1));
        CarData carData = _container.Datas[index % _container.Datas.Count].CarData;
        bool areWheelsDefault = !carData.HasSpikedWheels;
        _carHandler.SpawnCar(carData.CarModelIndex ,areWheelsDefault,areWheelsDefault, carData.HasSpikedWheels, carData.HasSpikedWheels, carData.HasWings, carData.HasRocket, carData.HasPropeller);
    }

    public void CompleteLevel()
    {
        _userDataHandler.Data.LastCompletedLevel = _currentLevel;
    }

    public void UnloadLevel()
    {
        if (_loseCoroutine != null)
        {
            StopCoroutine(_loseCoroutine);
        }
        _cameraController.ResetPosition();
        _abilityPanel.SetActive(false);
        _startButton.SetActive(false);
        _builder.Place(new());
        _carHandler.DespawnCar();
    }
    public void ResetLevels()
    {
        _currentLevel = 0;
        LoadLevel(_currentLevel);
    }

    public void LoadLevelsExternal()
    {
        _container.Datas = DataHandler.ReadXml<List<LevelData>>("OldLevelDatas");
    }

    public IEnumerator LoseTimer()
    {
        yield return new WaitForSeconds(60);
        _levelFailedPanel.SetActive(true);
        _abilityPanel.SetActive(false);
    }
}
