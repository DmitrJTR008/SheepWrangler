using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISessionView : MonoBehaviour
{
    [SerializeField] private Text _timerText;
    [SerializeField] private Transform _winPanel, _losePanel;
    [SerializeField] private GameDialog _dialog;

    public void ShowAdsDialog(Action<bool> DialogAnswer)
    {
        _dialog.gameObject.SetActive(true);
        _dialog.OnDialogAnswer += DialogAnswer;
    }
    public void CloseAdsDialog()
    {
        _dialog.gameObject.SetActive(false);
    }
    public void HandleEndGamePanels(bool isWin)
    {
        switch(isWin)
        {
            case true:
                _winPanel.gameObject.SetActive(true); break;
            case false:
                _losePanel.gameObject.SetActive(true); break;
        }
    }
    public void UpdateTImer(GameTimeFormat gameTimeFormat)
    {
        _timerText.text = gameTimeFormat.GetFormatTime();
    }
}
