using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameLoader : LogicComponent
{
    [SerializeField] private GameObject _playerSpawnPosition;
    public override void Initialize()
    {
        if (_playerSpawnPosition == null)
        {
            Debug.LogError("[ABORT] PLAYER SPAWN POSITION IS NULL");
            return;
        }
        
        //TODO: spawn player at position
    }

}