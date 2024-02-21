using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class GameDialog : MonoBehaviour
{
    [SerializeField] private Button YesAnswer, NoAnswer;
    public event Action<bool> OnDialogAnswer;
    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        YesAnswer.onClick.AddListener(() => GetAnswer(true));
        NoAnswer.onClick.AddListener(() => GetAnswer(false));
    }

    private void GetAnswer(bool answer)
    {
        OnDialogAnswer?.Invoke(answer);
    }
}
