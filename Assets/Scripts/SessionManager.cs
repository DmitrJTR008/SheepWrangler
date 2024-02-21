using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionManager : BaseSceneManager
{
    [SerializeField] private int sheepCountSession;
    
    [SerializeField] private List<Texture2D> SheepTextures;
    [SerializeField] private GameTimer gameTimerPrefab;

    
    [SerializeField] private Animal animalPrefab;
    [SerializeField] private Transform baseSpawnAnimalPoint;
    [SerializeField] private UISessionView view;
    [SerializeField] private InputHandler inputManager;
    [SerializeField] private float spawnRaidus;
    [SerializeField] private float levelTimer;

    private GameTimer _timerInstance;
    private GameTimeFormat _gameTimeFormat;
    private List<SheepMechanic> _activeAnimal;
    private AnimalSpawner _animalSpawner;
    private int _surviveSheeps;
    private int _deadSheeps;
    
    public override void Awake()
    {
        onProgressLoad += InitSession;
        base.Awake();
        
    }
    
    private void InitSession(ProgressData progressData, SettingsData settingsData)
    {
        _animalSpawner = new AnimalSpawner();
        _gameTimeFormat = new GameTimeFormat();
        Debug.Log("Session");
        StartSession();
    }
    void StartSession()
    {
        _animalSpawner.SpawnAnimal(InitAnimalList, animalPrefab, baseSpawnAnimalPoint, sheepCountSession,spawnRaidus);
        StartTime();
    }

    private void StartTime()
    {
        if (_timerInstance != null)
            Destroy(_timerInstance.gameObject);
        _timerInstance = Instantiate(gameTimerPrefab, transform.position, Quaternion.identity);
        _timerInstance.StartTimer(_gameTimeFormat, levelTimer, TimerENd, TickTimer);
    }

    private void TimerENd()
    {
        view.ShowAdsDialog(GetAnswerDialog);
        inputManager.enabled = false;
    }
    private void GetAnswerDialog(bool isAccept)
    {
        switch(isAccept)
        {
            case true:
                view.CloseAdsDialog();
                StartTime();
                inputManager.enabled = true;
                break;
            case false:
                view.CloseAdsDialog();
                if (_timerInstance != null)
                    _timerInstance.StopTimer();
                view.HandleEndGamePanels(false);
                break;
        }
    }
    private void TickTimer()
    {
        view.UpdateTImer(_gameTimeFormat);
    }

    private void InitAnimalList(List<Animal> animalList)
    {
        _activeAnimal = animalList.OfType<SheepMechanic>().ToList();

        foreach (var animal in _activeAnimal)
        {
            animal.OnAnimalSave += IncrementSheepSave;
            animal.OnAnimalDie += IncrementSheepDead;
            animal.SetNewSprite(SheepTextures[progressData.DefaultSheep]);
        }

    }
    
    public void IncrementSheepSave()
    {
        _surviveSheeps++;
        CheckSheepCount();

    }
    public void IncrementSheepDead()
    {
        _deadSheeps++;
        CheckSheepCount();
    }

    public void CheckSheepCount()
    {
        if (_surviveSheeps + _deadSheeps >= sheepCountSession)
           RegisterCompleteGame();
    }
    private void RegisterCompleteGame()
    {
        if (_timerInstance != null)
            _timerInstance.StopTimer();
        view.CloseAdsDialog();
        view.HandleEndGamePanels(true);
    }
    
    // DEBUG temporarily
    public void GOMENU()
    {
        SceneManager.LoadScene(0);
    }

    public void GORESTART()
    {
        SceneManager.LoadScene(1);
    }
}
