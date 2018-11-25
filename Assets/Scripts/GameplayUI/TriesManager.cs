using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriesManager
{
    private int _remainingTries;
    public int MaxTries;
    
    public Action<int> OnTriesUpdated;
    
    public void Init(LevelData levelData)
    {
        if (levelData.DeactivateTries)
        {
            return;
        }

        PlayerController.Instance.OnShoot -= OnShoot;
        PlayerController.Instance.OnShoot += OnShoot;
        _remainingTries = (levelData.AmountOfTries > 0) ? levelData.AmountOfTries : 7;
        MaxTries = _remainingTries;
        UpdateElements();
    }

    void OnShoot()
    {
        _remainingTries--;
        if (_remainingTries <= 0)
        {
            SoundManager.Instance.PlayDefat();
            ServicesManager.Instance.UIStackController.Push("UIPrefabs/LosePopup");

        }
        UpdateElements();
    }

    void UpdateElements()
    {
        if (OnTriesUpdated != null)
        {
            OnTriesUpdated(_remainingTries);
        }
    }
}