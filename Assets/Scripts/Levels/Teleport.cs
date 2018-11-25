using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] TeleportSpot _firstPortal;
    [SerializeField] TeleportSpot _secondPortal;
    [SerializeField] bool _oneWayOnly = false;
    [SerializeField] bool _keepBallVelocity = false;

    PlayerController _playerController;

    void Start ()
    {
        _playerController = FindObjectOfType<PlayerController>();

        _firstPortal.Init(FirstPortalActivated);
        _secondPortal.Init(SecondPortalActivated);
    }
	
    void FirstPortalActivated(TeleportSpot spot)
    {
        TeleportPlayer(_secondPortal.transform.position);
    }

    void SecondPortalActivated(TeleportSpot spot)
    {
        if (!_oneWayOnly)
        {
            TeleportPlayer(_firstPortal.transform.position);
        }
    }

    void TeleportPlayer(Vector3 position)
    {
        _playerController.TeleportPlayer(position);
    }
}
