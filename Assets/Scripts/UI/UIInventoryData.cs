using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryData : MonoBehaviour
{
    [SerializeField] private Transform rootShopContent;
    [SerializeField] private InventoryComponent inventoryComponentPrefab;

    private List<InventoryComponent> _activeListInventoryComponents;
    public event Action<SheepData> OnSheepSelect;
    private void OnDisable()
    {
        if(_activeListInventoryComponents.Count < 1) return;
        _activeListInventoryComponents.ForEach(component => component.SelectSheepButton.onClick.RemoveAllListeners());
    }

    public void InitInventory(List<SheepData> sheepListData)
    {
        
        _activeListInventoryComponents = new List<InventoryComponent>();
        if (sheepListData.Count < 1) return;
        inventoryComponentPrefab.gameObject.SetActive(false);

        for (int i = 0; i < sheepListData.Count; i++)
        {
            AddNewitem(sheepListData[i]);
        }
        
       // SubscribeInputInventory();
    }

    /*private void SubscribeInputInventory()
    {
        _activeListInventoryComponents.ForEach(component => component.SelectSheepButton.onClick.AddListener
            (()=>SelectSheep(component.sheepData.ID))
        );
    }*/

    public void AddNewitem(SheepData sheep)
    {
        InventoryComponent clone = Instantiate(inventoryComponentPrefab, rootShopContent);
        clone.SelectSheepButton.onClick.AddListener(()=> SelectSheep(clone.sheepData));
        clone.InitInventoryComponent(sheep);
        clone.gameObject.SetActive(true);
        _activeListInventoryComponents.Add(clone);
        
    }
    
    public void SelectSheep(SheepData sheep)
    {
        OnSheepSelect?.Invoke(sheep);
    }
}
