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

    SimpleStateMachine _stateMachine;
    private Rigidbody _rigidbody;

    //public delegate void OnInputChangedEventHandler(Gesture.GestureState gestureState, Vector2 deltaPosition);
   // public event OnInputChangedEventHandler OnInputChangedEvent;

    WaitForInput _waitForInputState;
    InPlayerMovementState _inMovementState;

    public Transform InitialPosition;

    public delegate void OnShootDelegate();
    public event OnShootDelegate OnShoot;

    GolfCameraController _cameraController;

    public float MinValidMovementSpeed { get { return _minValidSpeed; } }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

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
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
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

    public void Init()
    {
        _instance = this;
        
        _rigidbody = GetComponent<Rigidbody>();
        
        SetupStateMachine();
        _cameraController = FindObjectOfType<GolfCameraController>();
        ResetToPosition(InitialPosition.position);
        _stateMachine.ChangeState(_waitForInputState);
        _cameraController.SetInitialCamera();

        StopAllForces();

        EventManager.Instance.StartListening(ShootEvent.EventName, PanFinished);
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
            StopAllForces();
        }
    }

    public void ResetToPosition(Vector3 pos)
    {
        StopAllForces();
        transform.position = pos;
        ResetRotation();
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
        _rigidbody.isKinematic = false;
        var shootPower = _minShootPower + ((_maxShootPower - _minShootPower) * _trajectoryLine.Power);
        //Debug.Log("SHOOT POWER " + _trajectoryLine.Power + "   FINAL " + shootPower);
        _rigidbody.AddForce(_trajectoryLine.GetAimingDirection() * shootPower, ForceMode.Impulse);

        if (OnShoot != null)
        {
            OnShoot();
        }
    }

    public TrajectoryLine GetTrajectoryLine()
    {
        return _trajectoryLine;
    }

    public Rigidbody GetRigidbody()
    {
        return _rigidbody;
    }


    private void OnDestroy()
    {
                
        // TODO move it somewhere else
        EventManager.Instance.ResetAllEvents();
    }
}
