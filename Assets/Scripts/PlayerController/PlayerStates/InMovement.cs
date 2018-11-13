using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InPlayerMovementState : IState
{
    PlayerController _playerController;
    Rigidbody _playerRigidbody;

    float _timeWithSpeedZero;

    public InPlayerMovementState(PlayerController playerController)
    {
        _playerController = playerController;
        _playerRigidbody = _playerController.GetRigidbody();
    }

    public void Enter()
    {
        _timeWithSpeedZero = 0f;
    }

    public void Execute()
    {
        //Debug.Log("SPEED MAGNITUDE " + _playerRigidbody.velocity.magnitude);
        if(_playerRigidbody.velocity.magnitude <= _playerController.MinValidMovementSpeed)
        {
            _timeWithSpeedZero += Time.deltaTime;

            if (_timeWithSpeedZero > 1f)
            {
                _playerController.PlayerStopMoving();
            }
        }
    }

    public void Exit()
    {
     
    }
}
