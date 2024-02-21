using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class UIMenuPresenter 
{
    private UIMenuView _view;
    public UIMenuPresenter(UIMenuView view)
    {
        _view = view;
    }
    
    public void ShowReward(int ID)
    {
        YandexGame.RewVideoShow(ID);
    }
}
