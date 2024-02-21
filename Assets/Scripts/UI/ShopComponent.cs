using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopComponent : MonoBehaviour
{
    [SerializeField] private Image RootSprite;
    [SerializeField] private Text priceText;

    public SheepData sheepData { get; private set; }
    [field:SerializeField]public Button PurchaseItemButton { get; private set; }
    public int price { get; private set; }

    public void InitShopComponent(SheepData sheepDataSO)
    {
        sheepData = sheepDataSO;
        RootSprite.sprite = sheepData.TargetSheepTexture;
        price = sheepData.Price;
        priceText.text = price.ToString();

    }
}
