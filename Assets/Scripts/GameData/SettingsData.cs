using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData : MonoBehaviour, IDataSave
{
    public float MusicVolume = .5f;
    public float SoundVolume = .5f;
    public int LangID = 0;
    public string GetSavePath()
    {
        return "SettingsSaveData";
    }
}
