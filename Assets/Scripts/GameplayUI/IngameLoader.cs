using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameLoader : LogicComponent
{
    [Header("Prefabs")] [SerializeField] private GameObject _playerBall;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _touchManager;
    [SerializeField] private GameObject _touchPlane;
    
    [Header("Utils")]
    [SerializeField] private GameObject _playerSpawnPosition;
    
    public override void Initialize()
    {
        if (_playerSpawnPosition == null)
        {
            Debug.LogError("[ABORT] PLAYER SPAWN POSITION IS NULL");
            return;
        }

        var playerObject = Instantiate(_playerBall);
        playerObject.GetComponent<PlayerController>().InitialPosition = _playerSpawnPosition.transform;
        var cameraObject = Instantiate(_camera);
        cameraObject.GetComponent<GolfCameraController>().Target = playerObject;
        Instantiate(_touchManager);
        Instantiate(_touchPlane);
    }

}