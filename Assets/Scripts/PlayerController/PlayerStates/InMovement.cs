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
        _playerController.OnBallMoving(true);
    }

    public void Execute()
    {
        //Debug.Log("SPEED MAGNITUDE " + _playerRigidbody.velocity.magnitude);
        var isSlowVelocity = _playerRigidbody.velocity.magnitude <= _playerController.MinValidMovementSpeed;
        var isSlowAngularVelocity = _playerRigidbody.angularVelocity.magnitude <= _playerController.MinValidMovementAngularSpeed;
        
        if(/*isSlowVelocity ||*/ (isSlowAngularVelocity && isSlowVelocity))
        {
            _timeWithSpeedZero += Time.deltaTime;

            if (_timeWithSpeedZero > 0.1f)
            {
                if (_playerController.PlayerIsOnFloor())
                {
                    _playerController.PlayerStopMoving();
                }

            }
        }
    }

    public void Exit()
    {
     
    }

    public string Name()
    {
        return "InMovement";
    }
}
