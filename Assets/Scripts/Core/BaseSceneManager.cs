using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSceneManager : MonoBehaviour
{
    protected List<SheepData> _sheepDataListResource;

    protected DataHandler dataHandler;
    protected ProgressData progressData;
    protected SettingsData settingsData;

    private int _dataCount = 0;
    protected event Action<ProgressData, SettingsData> onProgressLoad;
    
    public virtual void Awake()
    {
        _sheepDataListResource = Resources.Load<SheepDataList>("SheepDataListSO").sheepDataList;
        progressData = new ProgressData();
        settingsData = new SettingsData();
        dataHandler = new DataHandler(this);
        dataHandler.Load(progressData, LoadGame);
        dataHandler.Load(settingsData, LoadGame);
    }
    void LoadGame(IDataSave getData)
    {
        switch(getData)
        {
            case ProgressData progressData:
                progressData = getData as ProgressData;
                break;
            case SettingsData settingsData:
                settingsData = getData as SettingsData;
                break;
        }
        _dataCount++;
        if(_dataCount >= 2) 
        {
            onProgressLoad?.Invoke(progressData, settingsData);
        }
    }
}
