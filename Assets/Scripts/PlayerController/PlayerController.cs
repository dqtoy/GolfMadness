using System;
using UnityEngine;
using TouchScript.Gestures;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TrajectoryLine _trajectoryLine;
    [SerializeField] float _minShootPower;
    [SerializeField] float _maxShootPower;
    [SerializeField] float _minValidSpeed;

    SimpleStateMachine _stateMachine;
    private Rigidbody _rigidbody;
    MetaGesture _metaGesture;

    public delegate void OnInputChangedEventHandler(Gesture.GestureState gestureState, Vector2 deltaPosition);
    public event OnInputChangedEventHandler OnInputChangedEvent;

    WaitForInput _waitForInputState;
    InPlayerMovementState _inMovementState;

    Vector3 _initialPosition;

    public float MinValidMovementSpeed { get { return _minValidSpeed; } }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _initialPosition = transform.position;

        SetupStateMachine();
        SetupGestures();

        // TODO Esto habra que llamarlo desde otro lado con alguna config probablemente
        Init();
    }

    #region StateMachine
    void SetupStateMachine()
    {
        _stateMachine = new SimpleStateMachine();
        _waitForInputState = new WaitForInput(this);
        _inMovementState = new InPlayerMovementState(this);

    }

    public void OnInputFinished()
    {
        Shoot();
        _stateMachine.ChangeState(_inMovementState);
    }

    public void PlayerStopMoving()
    {
        _stateMachine.ChangeState(_waitForInputState);
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    #endregion

    void SetupGestures()
    {
        _metaGesture = GetComponent<MetaGesture>();
    }

    public void OnGestureStateChanged(Gesture sender)
    {
        //Debug.Log(sender.State);
        if (OnInputChangedEvent != null)
        {
            OnInputChangedEvent(sender.State, sender.ScreenPosition);
        }
    }

    public void Init()
    {
        ResetToPosition(_initialPosition);
        _stateMachine.ChangeState(_waitForInputState);
    }

	
	void Update ()
    {
        _stateMachine.Update();

        UpdateFakeInput();
	}

    void UpdateFakeInput()
    {
        if(Input.GetKeyUp(KeyCode.R))
        {
            Init();
        }
    }

    public void ResetToPosition(Vector3 pos)
    {
        StopAllForces();
        transform.position = pos;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    void StopAllForces()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    void Shoot()
    {
        var shootPower = _minShootPower + ((_maxShootPower - _minShootPower) * _trajectoryLine.Power);
        Debug.Log("SHOOT POWER " + _trajectoryLine.Power + "   FINAL " + shootPower);
        _rigidbody.AddForce(_trajectoryLine.GetAimingDirection() * shootPower, ForceMode.Impulse);
    }

    public TrajectoryLine GetTrajectoryLine()
    {
        return _trajectoryLine;
    }

    public Rigidbody GetRigidbody()
    {
        return _rigidbody;
    }


}
