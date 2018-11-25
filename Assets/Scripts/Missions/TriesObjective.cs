using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriesObjective : Objective
{
    public int MaxAmountTries;
    private TriesManager _triesManager;

    void Start()
    {
        _triesManager = ServicesManager.Instance.TriesManager;
        _triesManager.OnTriesUpdated += OnTriesUpdated;
    }

    void OnTriesUpdated(int amountTries)
    {
        bool completed = MaxAmountTries >= _triesManager.MaxTries - amountTries;

        OnObjectiveUpdated(completed, this);
    }

    private void OnDestroy()
    {
        _triesManager.OnTriesUpdated -= OnTriesUpdated;
    }
}