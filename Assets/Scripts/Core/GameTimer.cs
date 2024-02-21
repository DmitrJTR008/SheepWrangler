using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeFormat
{
    public int Min, Sec;
    public string GetFormatTime()
    {
        return $"{Min:00}:{Sec:00}";
    }

}
public class GameTimer : MonoBehaviour
{
    private event Action OnEndTimer, OnTimerTick;
    
    private Coroutine _currentTimerCoroutine;
    private GameTimeFormat _gameTimeFormat;
    
    private float _targetTime;
    private bool  _isPause;
    
    public void StartTimer(GameTimeFormat gameTimeData, float time, Action OnTimerEnd, Action OnTick)
    {
        if (_currentTimerCoroutine != null) return;
        OnEndTimer = OnTimerEnd;
        OnTimerTick = OnTick;
        _targetTime = time;
        _gameTimeFormat = gameTimeData;
        _currentTimerCoroutine = StartCoroutine(ReleaseTimer());
    }

    public void HandleTimer(bool toPause)
    {
        if (_currentTimerCoroutine == null) return;
        _isPause = toPause;
    }

    public void StopTimer()
    {
        if (_currentTimerCoroutine == null) return;
        StopCoroutine(_currentTimerCoroutine);
        OnTimerTick = null;
        OnEndTimer = null;
        _currentTimerCoroutine = null;

    }

    IEnumerator ReleaseTimer()
    {
        while(_targetTime >= 0)
        {
            _gameTimeFormat.Min = (int)(_targetTime / 60);
            _gameTimeFormat.Sec = (int)(_targetTime % 60);
            OnTimerTick?.Invoke();
            yield return new WaitForSeconds(1);
            if (!_isPause)
                _targetTime -= 1f;
        }
        
        OnEndTimer?.Invoke();
        _currentTimerCoroutine = null;
    }
}
