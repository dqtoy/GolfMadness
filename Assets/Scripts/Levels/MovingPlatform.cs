using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum PlayingMode
    {
        FORWARD,
        BACKWARDS,
        LOOP,
        PINGPONG,
    }

    [SerializeField] Vector3 _relativeTargetPosition;
    [SerializeField] float _timeToMove;
    [SerializeField] float _initialElapsedTime;
    [SerializeField] PlayingMode _playingMode;
    [SerializeField] float _startWaitingTime;

    Vector3 _startingPosition;
    Vector3 _endingPosition;
    float _movingTimeElapsed;
    float _startingTimeElapsed;

    private void OnDrawGizmos()
    {
        var endingPos = Application.isPlaying ? _endingPosition : transform.TransformPoint(_relativeTargetPosition);
        var startingPos = Application.isPlaying ? _startingPosition : transform.position;
        Gizmos.DrawWireSphere(endingPos, 0.5f);
        Gizmos.DrawWireSphere(startingPos, 0.5f);
        Gizmos.DrawLine(startingPos, endingPos);
    }

    // Use this for initialization
    void Start ()
    {
        _startingPosition = transform.position;
        _endingPosition = transform.TransformPoint(_relativeTargetPosition);

        if(_playingMode == PlayingMode.BACKWARDS)
        {
            SwapStartingAndEndingPositions();
        }

        _movingTimeElapsed = _initialElapsedTime;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(_startingTimeElapsed < _startWaitingTime)
        {
            _startingTimeElapsed += Time.deltaTime;
            return;
        }

        _movingTimeElapsed += Time.deltaTime;
        var t = _movingTimeElapsed / _timeToMove;
        transform.position = Vector3.Lerp(_startingPosition, _endingPosition, t);

        if(_playingMode == PlayingMode.LOOP && t >= 1f)
        {
            _startingTimeElapsed = 0f;
            _movingTimeElapsed = 0f;
        }
        else if(_playingMode == PlayingMode.PINGPONG && t >= 1f)
        {
            _startingTimeElapsed = 0f;
            _movingTimeElapsed = 0f;
            SwapStartingAndEndingPositions();
        }
	}

    void SwapStartingAndEndingPositions()
    {
        var tempTargetPosition = _startingPosition;
        _startingPosition = _endingPosition;
        _endingPosition = tempTargetPosition;
    }
}
