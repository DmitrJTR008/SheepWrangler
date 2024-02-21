using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DataHandler 
{
    private BaseSceneManager _baseSceneManager;
    public DataHandler(BaseSceneManager baseSceneManager)
    {
        _baseSceneManager = baseSceneManager;
    }
    
    public void Save(IDataSave dataSave)
    {
        SaveGameHandler.SaveGame(dataSave);
    }
    public void Load(IDataSave dataLoad, Action<IDataSave> CallBackLoadGameData)
    {
        SaveGameHandler.LoadGame(dataLoad, CallBackLoadGameData);
    }
}
