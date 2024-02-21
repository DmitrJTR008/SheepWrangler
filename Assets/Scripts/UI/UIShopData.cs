using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class UIShopData : MonoBehaviour
{
    [SerializeField] private Transform rootShopContent;
    [SerializeField] private ShopComponent shopComponentPrefab;
    private List<ShopComponent> _activeShopList;
    private ShopComponent _lastSelectShopComponent;
    public event Action<SheepData> OnTryGetItem;


    private void OnDisable()
    {
        _activeShopList.ForEach(component => component.PurchaseItemButton.onClick.RemoveAllListeners());
    }

    public void InitShop(List<SheepData> sheepDataList)
    {
        
        shopComponentPrefab.gameObject.SetActive(false);
        _activeShopList = new List<ShopComponent>();
        if (sheepDataList.Count < 1) return;
        for (int i = 0; i < sheepDataList.Count; i++)
        {
            ShopComponent clone = Instantiate(shopComponentPrefab, rootShopContent);
            clone.InitShopComponent(sheepDataList[i]);
            clone.gameObject.SetActive(true);
            _activeShopList.Add(clone);
        }

        shopComponentPrefab.gameObject.SetActive(false);
        SubscribePurchase();
    }

    private void SubscribePurchase()
    {
        _activeShopList.ForEach(component =>
            component.PurchaseItemButton.onClick.AddListener(() => GetSelectItem(component.sheepData.ID)));
    }

    public void GetSelectItem(int index)
    {
        Debug.Log(index);
        ShopComponent targetBuy = _activeShopList.FirstOrDefault(item => item.sheepData.ID == index);
        if (targetBuy != null)
        {
            _lastSelectShopComponent = targetBuy;
            OnTryGetItem?.Invoke(_lastSelectShopComponent.sheepData);
        }
    }

    public SheepData BuyItem(SheepData sheepData, int money)
    {
        if (money >= sheepData.Price)
        {
            _lastSelectShopComponent.PurchaseItemButton.onClick.RemoveAllListeners();
            _activeShopList.Remove(_lastSelectShopComponent);
            Destroy(_lastSelectShopComponent.gameObject);
            return sheepData;
        }
        return null;
    }

}
