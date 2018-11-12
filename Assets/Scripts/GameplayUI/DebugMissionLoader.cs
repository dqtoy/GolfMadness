using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMissionLoader : LogicComponent
{
    [SerializeField] private LevelData _levelData;

    public override void Initialize()
    {
        if (ServicesManager.Instance.PlayerModel.LoadedLevelIndex < 0)
        {
            ServicesManager.Instance.PlayerModel.LoadedLevelIndex = ServicesManager.Instance.LevelDataCollection.GetIndex(_levelData);
        }
    }
}