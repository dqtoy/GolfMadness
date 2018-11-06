using System;
using UnityEngine;
using TouchScript.Gestures;

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
        _playerController.OnInputChangedEvent += OnInputChanged;
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        _playerController.OnInputChangedEvent -= OnInputChanged;
        
    }

    void OnInputChanged(Gesture.GestureState gesture, Vector2 inputPosition)
    {
        switch (gesture)
        {
            case Gesture.GestureState.Began:
                _initialInputPosition = inputPosition;
                _trajectoryLine.StartNewAiming();
                break;
            case Gesture.GestureState.Changed:
                _trajectoryLine.StartNewAiming();
                break;
            case Gesture.GestureState.Ended:
                _trajectoryLine.FinishAiming();
                _playerController.OnInputFinished();
                break;
        }

    }
}
