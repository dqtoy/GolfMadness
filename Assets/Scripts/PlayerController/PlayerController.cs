using BlastyEvents;
﻿using System.Collections.Generic;
﻿using System;
using BlastyEvents;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController _instance;
    public static PlayerController Instance
    {
        get
        {
            return _instance;
        }
    }
    
    [SerializeField] private TrajectoryLine _trajectoryLine;
    [SerializeField] float _minShootPower;
    [SerializeField] float _maxShootPower;
    [SerializeField] float _minValidSpeed;
    [SerializeField] float _minAngularValidSpeed;
    [SerializeField] float _angularSpeedMultiplier = 1f;
    [SerializeField] private bool _spaceMode = false;
    
    SimpleStateMachine _stateMachine;
    private Rigidbody _rigidbody;

    //public delegate void OnInputChangedEventHandler(Gesture.GestureState gestureState, Vector2 deltaPosition);
    // public event OnInputChangedEventHandler OnInputChangedEvent;

    WaitForInput _waitForInputState;
    InPlayerMovementState _inMovementState;

    public Transform InitialPosition;

    public GolfCameraController CameraController;

    public delegate void OnShootDelegate();
    public event OnShootDelegate OnShoot;

    GolfCameraController _cameraController;

    public float MinValidMovementSpeed { get { return _minValidSpeed; } }
    public float MinValidMovementAngularSpeed { get { return _minAngularValidSpeed; } }

    private Vector3 _prevPositionToShoot;
    private Quaternion _prevPositionToRotation;
    
    private float _colliderHeight;
    public float ColliderHeight { get { return _colliderHeight; } }

    [SerializeField] private ElementalAbsorber _elementalAbsorber;
    public Action<bool> OnBallMoves;
    public GameObject TouchParticle;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        var sphereCollider = GetComponent<SphereCollider>();

        _colliderHeight = sphereCollider.radius + 0.05f;

        SetupStateMachine();

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

    public void PlayerStopMoving()
    {
        _stateMachine.ChangeState(_waitForInputState);
        _rigidbody.isKinematic = true;
    }

    #endregion

    private void PanFinished(BlastyEventData ev)
    {
        var shootEv = (ShootEventData) ev;
        
        if (shootEv.ValidShot)
        {
            Shoot();
            _stateMachine.ChangeState(_inMovementState);
        }
    }

    public void Init(Elemental.Element element = Elemental.Element.NONE)
    {
        _instance = this;
        
        _rigidbody = GetComponent<Rigidbody>();
        
        SetupStateMachine();
        _cameraController = FindObjectOfType<GolfCameraController>();
        StopAllForces();
        ResetToPosition(InitialPosition);
        _stateMachine.ChangeState(_waitForInputState);
        _cameraController.SetInitialCamera();

        EventManager.Instance.StartListening(ShootEvent.EventName, PanFinished);

        _elementalAbsorber = transform.GetComponent<ElementalAbsorber>();
        if (element == Elemental.Element.NONE)
        {
            return;
        }

        if (element != Elemental.Element.ANY)
        {
            _elementalAbsorber.CurrentElement = element;
            return;
        }

        List<Elemental.Element> availableColors = new List<Elemental.Element>();
        availableColors.Add(Elemental.Element.EARTH);
        availableColors.Add(Elemental.Element.FIRE);
        availableColors.Add(Elemental.Element.WATER);

        _elementalAbsorber.CurrentElement = (Elemental.Element)UnityEngine.Random.Range(0, availableColors.Count);
    }

	
	void Update ()
    {
        _stateMachine.Update();

        UpdateFakeInput();

        if (transform.position.y < -6f)
        {
            Reset();
        }
	}

    public void Reset()
    {
        transform.position = _prevPositionToShoot;
        transform.rotation = _prevPositionToRotation;
        StopAllForces();
        _cameraController.RotateCameraAroundPlayer(Vector2.zero);
    }

    void UpdateFakeInput()
    {
        if(Input.GetKeyUp(KeyCode.R))
        {
            Reset();
        }
    }

    public void ResetToPosition(Transform initialPosition)
    {
        StopAllForces();
        transform.position = initialPosition.position;
        transform.localRotation = initialPosition.rotation;
        //ResetRotation();
    }

    public void ResetRotation()
    {
        transform.localRotation = Quaternion.identity;
    }

    void StopAllForces()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        ResetRotation();
    }

    void Shoot()
    {
        if (PlayerIsOnFloor())
        {
            _prevPositionToShoot = transform.localPosition;
            _prevPositionToRotation = transform.localRotation;
        }

        SoundManager.Instance.PlayShoot();
        _rigidbody.isKinematic = false;
        var shootPower = _minShootPower + ((_maxShootPower - _minShootPower) * _trajectoryLine.Power);
        //Debug.Log("SHOOT POWER " + _trajectoryLine.Power + "   FINAL " + shootPower);
        _rigidbody.AddForce(_trajectoryLine.GetAimingDirection() * shootPower, ForceMode.Impulse);

        _rigidbody.maxAngularVelocity = _maxShootPower * _angularSpeedMultiplier * 20f;
        var rotatingAxis = Vector3.Cross(Vector3.up, _trajectoryLine.GetAimingDirection());
        _rigidbody.angularVelocity = rotatingAxis * shootPower * _angularSpeedMultiplier;

        if (OnShoot != null)
        {
            OnShoot();
        }
    }

    public bool PlayerIsOnFloor()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.distance > ColliderHeight + 2)
                return false;
            return true;
        }

        return false;
    }

    public TrajectoryLine GetTrajectoryLine()
    {
        return _trajectoryLine;
    }

    public Rigidbody GetRigidbody()
    {
        return _rigidbody;
    }

    public void OnBallMoving(bool moving)
    {
        TouchParticle.SetActive(!moving);
        if (OnBallMoves != null)
        {
            OnBallMoves(moving);
        }
    }

    public void TeleportPlayer(Vector3 position)
    {
        transform.position = position;
        StopAllForces();
        //_cameraController.RotateCameraAroundPlayer(Vector2.zero);
    }

    private void OnDestroy()
    {
                
        // TODO move it somewhere else
        EventManager.Instance.ResetAllEvents();
    }

    private void OnCollisionEnter(Collision other)
    {
        SoundManager.Instance.PlayRebound();
    }
}
