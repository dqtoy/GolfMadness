using System;
using BlastyEvents;
using UnityEngine;

public class GolfCameraController : MonoBehaviour
{
    public GameObject Target;
    [SerializeField] Vector3 TargetPositionOffset;
    [SerializeField] Vector3 CameraPositionOffset;

    [SerializeField] float CameraRotationSpeed = 1;

    private TrajectoryLine _trajectoryLine;

    [SerializeField] private float _zoom;
    [SerializeField] private float InitialZoom = 0.3f;
    [SerializeField] private float ZoomSpeed = 30f;

    [SerializeField] private Vector2 MinScreenPercentageForRotation;

    [SerializeField] float _cameraPositionDistanceFromTarget = 4f;
    [SerializeField] float _maxThrowOffset = 1f;

    private Transform _cameraTarget;

    private float _currentThrowOffset = 0f;

    Vector3 _lookAtPosition;

    public float Zoom
    {
        get { return _zoom; }
        set { _zoom = Mathf.Clamp(value, 0f, 2f); }
    }

    public Vector3 TargetLookAtPosition
    {
        get
        {
            var cameraDirection = transform.forward;
            cameraDirection.y = 0f;
            cameraDirection.Normalize();
            _cameraTarget.position = (Target.transform.position + _cameraTarget.rotation * TargetPositionOffset);
            _cameraTarget.LookAt(Target.transform);
            return _cameraTarget.position;
        }
    }

    public Vector3 CameraTargetPosition
    {
        get
        {
            var camDirection = _cameraTarget.forward;
            camDirection.y = 0f;
            camDirection.Normalize();
            return TargetLookAtPosition + _zoom * camDirection * (_cameraPositionDistanceFromTarget + _currentThrowOffset) + Vector3.up * CameraPositionOffset.y;
        }
    }

    void Start()
    {
        _cameraTarget = new GameObject("CameraTarget").transform;

        EventManager.Instance.StartListening(TouchEvent.EventName, OnPanUpdated);

        _zoom = InitialZoom;
        //RotateCameraAroundPlayer(Vector2.zero);
        _cameraTarget.rotation = Target.transform.rotation;
        _lookAtPosition = TargetLookAtPosition;

        _trajectoryLine.SubscribeToOnPowerChanged(UpdatePower);
    }

    void UpdatePower(float amount)
    {
        _currentThrowOffset = amount * _maxThrowOffset;
    }

    public void Init(GameObject initPosition, TrajectoryLine trajectoryLine)
    {
        Target = initPosition;
        _trajectoryLine = trajectoryLine;
    }

    private void OnPanUpdated(BlastyEventData ev)
    {
        var touchEventData = (TouchEventData) ev;

        if (touchEventData.PanType == TouchManager.PanType.Player)
        {
            TryUpdateRotationBecauseOfAimingLimit(touchEventData.CurDirection);
            return;
        }

        switch (touchEventData.TouchState)
        {
            case TouchManager.TouchState.InitPan:
                //_initialDragPosition = sender.ScreenPosition;
                //GetInitialDirectionOnScreenSpace();
                break;
            case TouchManager.TouchState.UpdatePan:
                RotateCameraAroundPlayer(touchEventData.DeltaIncrement);
                break;
            case TouchManager.TouchState.FinishPan:
                //ResetRotation();
                break;
        }
    }

    void TryUpdateRotationBecauseOfAimingLimit(Vector2 curDirection)
    {
        if (curDirection.y > -0.2f)
        {
            RotateCameraAroundPlayer(new Vector2(-curDirection.x, curDirection.y));
            //_trajectoryLine.UpdateRotation(curDirection.x * CameraRotationSpeed * Time.deltaTime);
        }
    }

    void Update()
    {
        //if (_targetRigidbody.velocity.magnitude > MinSpeed || _targetRigidbody.angularVelocity.magnitude > MinSpeed)
        {
            UpdateMovementCamera();
        }
    }

    public void SetInitialCamera()
    {
        transform.rotation = Target.transform.rotation;
        var targetPosition = Target.transform.localPosition + TargetPositionOffset;
        transform.localPosition = Target.transform.position + CameraPositionOffset;
        transform.LookAt(targetPosition);
    }

    void UpdateMovementCamera()
    {
        //   _targetObject.transform.rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.up) * Quaternion.LookRotation(_trajectoryLine.GetAimingDirection());

        _lookAtPosition = Vector3.Lerp(_lookAtPosition, TargetLookAtPosition, 0.3f);
        transform.position = Vector3.Lerp(transform.position, CameraTargetPosition, 0.3f);

        transform.LookAt(_lookAtPosition);
    }

    public void RotateCameraAroundPlayer(Vector2 deltaIncrement)
    {
        if ((Mathf.Abs(deltaIncrement.x) / Screen.width) > MinScreenPercentageForRotation.x)
        {
            var increment = (deltaIncrement.x / Screen.width) * CameraRotationSpeed * Time.deltaTime;
            _cameraTarget.RotateAround(Target.transform.position, Vector3.up, increment);
            transform.RotateAround(Target.transform.position, Vector3.up, increment);
        }

        if ((Mathf.Abs(deltaIncrement.y) / Screen.height) > MinScreenPercentageForRotation.y)
        {
            var incrementY = (deltaIncrement.y / Screen.height) * Time.deltaTime * ZoomSpeed;
            Zoom = Math.Max(0.5f, Zoom + incrementY);
        }

        transform.LookAt(TargetLookAtPosition);
        transform.position = CameraTargetPosition;
    }
}