using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SheepDataSO", menuName = "SO/SheepDataSO")]
public class SheepData : ScriptableObject
{
    public Sprite TargetSheepTexture;
    public int Price;
    public int ID;
}
