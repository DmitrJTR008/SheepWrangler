using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UICarrerData : MonoBehaviour
{
    [SerializeField] private List<Button> ButtonSelectLevelList;

    private void OnEnable()
    {
        for (int i = 0; i < ButtonSelectLevelList.Count; i++)
        {
            int index = i + 1;
            ButtonSelectLevelList[i].onClick.AddListener(()=> SceneManager.LoadScene(index));
        }
    }

    private void OnDisable()
    {
        ButtonSelectLevelList.ForEach(x => x.onClick.RemoveAllListeners());
    }

    public void InitLevels(int levelComplete)
    {
        int totalLevels = Mathf.Clamp(levelComplete, 0, ButtonSelectLevelList.Count - 1);
        for (int i = 0; i <= totalLevels; i++)
        {
            ButtonSelectLevelList[i].interactable = true;
        }
    }
}
