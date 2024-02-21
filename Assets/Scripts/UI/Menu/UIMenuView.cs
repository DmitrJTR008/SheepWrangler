using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UIMenuView : MonoBehaviour
{
 
    [SerializeField] private UISettingsData settingsMenu;
    [SerializeField] private UIShopData shopMenu;
    [SerializeField] private UIInventoryData inventoryMenu;
    [SerializeField] private UICarrerData carrerMenu;
    [SerializeField] private Button rewardBtn;
    [SerializeField] private Text _moneyText;
    
    private UIMenuPresenter _presenter;
    private ProgressData progressData;
    private SettingsData settingsData;
    private List<SheepData> sheepDataListSO;

    private event Action<SettingsData> onSettingsChanged;
    private event Action<int> onGetReward;
    private event Action<SheepData> OnPurchaseSucess;
    private event Action<SheepData> OnSelectSheepChange;

    private void Awake()
    {
        _presenter = new UIMenuPresenter(this);
    }

    private void OnEnable()
    {
        rewardBtn.onClick.AddListener(() => _presenter.ShowReward(0));
        settingsMenu.OnMusicValueChange += OnMusicVolumeChange;
        settingsMenu.OnSoundVolumeChange += OnSoundVolumeChange;
        settingsMenu.OnLanguageChange += OnLanguageChange;
        shopMenu.OnTryGetItem += TryBuyItemFromShop;
        inventoryMenu.OnSheepSelect += ChangeSheep;
    }
    private void OnDisable()
    {
        settingsMenu.OnMusicValueChange -= OnMusicVolumeChange;
        settingsMenu.OnSoundVolumeChange -= OnSoundVolumeChange;
        settingsMenu.OnLanguageChange -= OnLanguageChange;
        shopMenu.OnTryGetItem -= TryBuyItemFromShop;
        onSettingsChanged = null;
        OnPurchaseSucess = null;
        OnSelectSheepChange = null;
        YandexGame.RewardVideoEvent -= onGetReward;

        onGetReward = null;
        rewardBtn.onClick.RemoveAllListeners();
    }

    public void SubscribeView(Action<SettingsData> OnSettingsDataChange, Action<int> OnGetReward, Action<SheepData> OnSuccessBuySheep, Action<SheepData> OnChoiseSheepChange)
    {
       onSettingsChanged = OnSettingsDataChange;
       OnPurchaseSucess = OnSuccessBuySheep;
       OnSelectSheepChange = OnChoiseSheepChange;
       YandexGame.RewardVideoEvent += OnGetReward;
    }
    
    public void InitView(IDataSave gameData)
    {
        switch(gameData)
        {
            case ProgressData progr:
                progressData = gameData as ProgressData;
                UpdateView(progressData);
                break;
            case SettingsData setting:
                settingsData = gameData as SettingsData;
                settingsMenu.InitSliderView(settingsData.MusicVolume, setting.SoundVolume);
                break;
        }
    }

    public void InitView(List<SheepData> sheepListSO)
    {
        sheepDataListSO = sheepListSO;
        List<SheepData> inventoryList = sheepDataListSO.Where(x => progressData.IDPurchaseList.Contains(x.ID)).ToList();
        inventoryMenu.InitInventory(inventoryList);
        sheepDataListSO.RemoveAll(item=>inventoryList.Contains(item));
        shopMenu.InitShop(sheepDataListSO);
        
    }

    public void UpdateView(ProgressData progressData)
    {
        this.progressData = progressData;
        _moneyText.text = this.progressData.Money.ToString("00000");
        carrerMenu.InitLevels(this.progressData.CompleteLVL);
        Debug.Log(JsonUtility.ToJson(progressData));
    }

    private void TryBuyItemFromShop(SheepData sheepData)
    {
       SheepData buySheep = shopMenu.BuyItem(sheepData, progressData.Money);
       if (buySheep != null)
       {
           OnPurchaseSucess?.Invoke(buySheep);
           inventoryMenu.AddNewitem(buySheep);
       }
    }

    private void ChangeSheep(SheepData sheep)
    {
        OnSelectSheepChange?.Invoke(sheep);
    }

    // Sound CallBacks
    private void OnMusicVolumeChange(float value)
    {
        settingsData.MusicVolume = value;
        onSettingsChanged?.Invoke(settingsData);
    }
    private void OnSoundVolumeChange(float value) 
    {
        settingsData.SoundVolume = value;
        onSettingsChanged?.Invoke(settingsData);
    }
    private void OnLanguageChange(int ID)
    {
        settingsData.LangID = ID;
        onSettingsChanged?.Invoke(settingsData);
    }
    // End Sound CallBack
}
