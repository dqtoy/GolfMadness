using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameLoader : LogicComponent
{
    [Header("Prefabs")] [SerializeField] private GameObject _playerBall;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _touchManager;

    [Header("Utils")] [SerializeField] private GameObject _playerSpawnPosition;

    public override void Initialize()
    {
        if (_playerSpawnPosition == null)
        {
            Debug.LogError("[ABORT] PLAYER SPAWN POSITION IS NULL");
            return;
        }

        Instantiate(_touchManager);
        
        var playerObject = Instantiate(_playerBall);
        var playerController = playerObject.GetComponent<PlayerController>();
        playerController.InitialPosition = _playerSpawnPosition.transform;
        
        var cameraObject = Instantiate(_camera);
        var cameraController = cameraObject.GetComponent<GolfCameraController>();

        var debugMissionLoader = FindObjectOfType<DebugMissionLoader>();
        
        cameraController.Init(playerObject, playerController.GetTrajectoryLine());
        playerController.Init(debugMissionLoader._levelData.InitialElement);


            

        var elementalObjects = FindObjectsOfType<Elemental>();

        for (int i = 0; i < elementalObjects.Length; i++)
        {
            elementalObjects[i].Init();
        }
    }
}