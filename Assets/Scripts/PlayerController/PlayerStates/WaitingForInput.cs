using System;
using UnityEngine;

public class WaitForInput : IState
{
    bool _startedInputMovement = false;
    PlayerController _playerController;
    TrajectoryLine _trajectoryLine;
    Vector2 _initialInputPosition;


    public WaitForInput(PlayerController playerController)
    {
        _playerController = playerController;
        _trajectoryLine = _playerController.GetTrajectoryLine();
    }

    public void Enter()
    {
        _playerController.OnBallMoving(false);
        //_playerController.ResetRotation();
        //_playerController.OnInputChangedEvent += OnInputChanged;
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
       // _playerController.OnInputChangedEvent -= OnInputChanged;
        
    }

    public string Name()
    {
        return "WaitingForInput";
    }
}
