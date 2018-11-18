using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPlayerMovementState : IState
{
    PlayerController _playerController;
    Rigidbody _playerRigidbody;

    int _ticksWithSpeedZero;

    public InPlayerMovementState(PlayerController playerController)
    {
        _playerController = playerController;
        _playerRigidbody = _playerController.GetRigidbody();
    }

    public void Enter()
    {
        _ticksWithSpeedZero = 0;
    }

    public void Execute()
    {
        //Debug.Log("SPEED MAGNITUDE " + _playerRigidbody.velocity.magnitude);
        var isSlowVelocity = _playerRigidbody.velocity.magnitude <= _playerController.MinValidMovementSpeed;
        var isSlowAngularVelocity = _playerRigidbody.angularVelocity.magnitude <= _playerController.MinValidMovementSpeed;
        
        if(isSlowVelocity || (isSlowAngularVelocity && isSlowVelocity))
        {
            ++_ticksWithSpeedZero;

            if (_ticksWithSpeedZero > 3)
            {
                _playerController.PlayerStopMoving();
            }
        }
    }

    public void Exit()
    {
     
    }
}
