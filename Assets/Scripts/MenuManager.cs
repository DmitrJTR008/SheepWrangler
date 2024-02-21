using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : BaseSceneManager
{
    [SerializeField] private UIMenuView menuView;

    public override void Awake()
    {
        onProgressLoad += InitData;
        base.Awake();
    }

    private void OnDisable()
    {
        onProgressLoad -= InitData;
    }

    void InitData(ProgressData progress, SettingsData settings)
    {
        menuView.InitView(progress);
        menuView.InitView(settings);
        menuView.InitView(_sheepDataListResource);
        menuView.SubscribeView(SettingChange, GetReward, PurchasSheepData, ChangeNewSheep);
        MusicHandler.Singleton.SetVolume(settings.MusicVolume);
    }

    private void SettingChange(SettingsData settingData)
    {
        MusicHandler.Singleton.SetVolume(settingData.MusicVolume);
        dataHandler.Save(settingData);
    }

    private void PurchasSheepData(SheepData sheep)
    {
        progressData.Money -= sheep.Price;
        progressData.IDPurchaseList.Add(sheep.ID);
        dataHandler.Save(progressData);
        
        menuView.UpdateView(progressData);
        
    }

    private void ChangeNewSheep(SheepData sheep)
    {
        progressData.DefaultSheep = sheep.ID;
        dataHandler.Save(progressData);
    }
    private void GetReward(int ID)
    {
        progressData.Money += 200;
        dataHandler.Save(progressData);
        menuView.UpdateView(progressData);
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }
}
