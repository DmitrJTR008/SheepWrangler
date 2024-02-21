using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
public static class SaveGameHandler 
{
    public static void SaveGame(IDataSave data)
    {
        string dataStr = JsonUtility.ToJson(data);

        if (YandexGame.auth && data is ProgressData gameData)
        {
            YandexGame.savesData.GameData = JsonUtility.ToJson(gameData);
            YandexGame.SaveProgress();
        }
        else
        {
            PlayerPrefs.SetString(data.GetSavePath(), dataStr);
            PlayerPrefs.Save();
        }
    }

    public static void LoadGame(IDataSave data, Action<IDataSave> CallBackLoad)
    {
        //���� ����������� � ��� ���� �����
        if(YandexGame.auth && data is ProgressData gameData)
        {
            Debug.Log("������������ ����������� ");
            // ��������� ���������� � ������
            if(!string.IsNullOrWhiteSpace(YandexGame.savesData.GameData))
            {
                // ���� ���� �� ����������
                Debug.Log("� ������ ���� ���������� ");
                JsonUtility.FromJsonOverwrite(YandexGame.savesData.GameData, data);
                CallBackLoad?.Invoke(data);
            }

            // ���� ��� �� ��������� ���������
            else
            {
                Debug.Log("� ������ ��� ����������");
                // ���� ��� ��������� 
                if (!PlayerPrefs.HasKey(gameData.GetSavePath()))
                {
                    Debug.Log("������� ����� � ���������� � ������");
                    // ������� ����� �� �������
                    SaveGame(data);
                    JsonUtility.FromJsonOverwrite(YandexGame.savesData.GameData, data);
                    CallBackLoad?.Invoke(data);
                }
                // ���� ����
                else
                {
                    Debug.Log("������� ��������� ����������");
                    string dataStr = PlayerPrefs.GetString(data.GetSavePath());
                    JsonUtility.FromJsonOverwrite(dataStr, data);
                    
                    SaveGame(data);
                    CallBackLoad?.Invoke(data);
                }
            }
        }
        // ���� ������������ �� �����������
        else
        {
            Debug.Log("�������� ��������� ���������� ��� �����������");
            if(!PlayerPrefs.HasKey(data.GetSavePath())) // ��������� ���� ��� ���������� �� ������� �����
            {
                Debug.Log("��� ���������� � ������� �����");
                SaveGame(data);
            }

            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(data.GetSavePath()),data);
            CallBackLoad?.Invoke(data);
        }
    }
}
