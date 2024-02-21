using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryComponent : MonoBehaviour
{
    [SerializeField] private Image RootSprite;

    public SheepData sheepData { get; private set; }
    [field: SerializeField] public Button SelectSheepButton { get; private set; }

    public void InitInventoryComponent(SheepData sheepDataSO)
    {
        sheepData = sheepDataSO;
        RootSprite.sprite = sheepData.TargetSheepTexture;
    }
}
