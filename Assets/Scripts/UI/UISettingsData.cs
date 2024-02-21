using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettingsData : MonoBehaviour
{
    [SerializeField] Slider MusicSlider, SoundSlider;
    [SerializeField] List<Button> LanguageButtons;
    public event Action<float> OnMusicValueChange, OnSoundVolumeChange;
    public event Action<int> OnLanguageChange;

    private void OnEnable()
    {
        MusicSlider.onValueChanged.AddListener((x) => MusicVolumeChange(MusicSlider.value));
        SoundSlider.onValueChanged.AddListener((x) => SoundVolumeChange(SoundSlider.value));

        for(int i = 0; i < LanguageButtons.Count; i++)
        {
            int index = i;
            LanguageButtons[i].onClick.AddListener(() => LanguageChange(index));
        }
        
    }
    private void OnDisable()
    {
        MusicSlider.onValueChanged.RemoveAllListeners();
        SoundSlider.onValueChanged.RemoveAllListeners();
        LanguageButtons.ForEach(x => { x.onClick.RemoveAllListeners(); });
    }


    public void InitSliderView(float musicVolume, float soundVolume)
    {
        MusicSlider.value = musicVolume;
        SoundSlider.value = soundVolume;
    }

    private void MusicVolumeChange(float value)
    {
        Debug.Log(value);
        OnMusicValueChange?.Invoke(value);
    }

    private void SoundVolumeChange(float value)
    {
        Debug.Log(value);
        OnSoundVolumeChange?.Invoke(value);
    }

    private void LanguageChange(int ID)
    {
        Debug.Log(ID);
        OnLanguageChange?.Invoke(ID);
    }
}
