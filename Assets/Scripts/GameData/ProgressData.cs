using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ProgressData : MonoBehaviour, IDataSave
{
    public List<int> IDPurchaseList = new List<int> { 0 };
    public int Money;
    public int CompleteLVL = 4;
    public int DefaultSheep = 0;
    public string GetSavePath()
    {
        return "GameProgressData";
    }
}
