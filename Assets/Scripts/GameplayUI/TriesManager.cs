using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriesManager
{
    private int _remainingTries;

    public Action<int> OnTriesUpdated;
    
    public void Init(int tries)
    {
        PlayerController.Instance.OnShoot -= OnShoot;
        PlayerController.Instance.OnShoot += OnShoot;
        _remainingTries = (tries > 0) ? tries : 7;
        UpdateElements();
    }

    void OnShoot()
    {
        _remainingTries--;
        if (_remainingTries <= 0)
        {
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