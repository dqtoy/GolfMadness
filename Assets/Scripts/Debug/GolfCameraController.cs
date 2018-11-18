using BlastyEvents;
using UnityEngine;

public class GolfCameraController : MonoBehaviour
{
    public GameObject Target;
    [SerializeField] Vector3 TargetPositionOffset;
    [SerializeField] Vector3 CameraPositionOffset;

    [SerializeField] private float MinCameraHeight = 3f;
    [SerializeField] private float MaxCameraHeight = 35f;
    
    [SerializeField] Vector3 CameraPositionOffsetMin;
    [SerializeField] Vector3 CameraPositionOffsetMax;
    
    [Range(0,10)]
    [SerializeField] float lerpValue = 1;

    [Range(0, 1)]
    [SerializeField] float MinSpeed = 1;

    [SerializeField] float CameraRotationSpeed = 1;
    Vector3 _initialPosition;

    private Vector3 velocity = Vector3.zero;
    private Rigidbody _targetRigidbody;

    private TrajectoryLine _trajectoryLine;

    [SerializeField] private float _zoom;
    [SerializeField] private float InitialZoom = 0.3f;
    [SerializeField] private float ZoomSpeed = 30f;

    [SerializeField] private Vector2 MinScreenPercentageForRotation;
    
    public float Zoom
    {
        get { return _zoom; }
        set { _zoom = Mathf.Clamp(value, 0f, 1f); }
    }
    
    void Start ()
    {
        _initialPosition = transform.localPosition;
        _targetRigidbody = Target.GetComponent<Rigidbody>();

        EventManager.Instance.StartListening(TouchEvent.EventName, OnPanUpdated);

        _zoom = InitialZoom;
        RotateCameraAroundPlayer(Vector2.zero);
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
    
    void Update ()
    {
        if (_targetRigidbody.velocity.magnitude > MinSpeed || _targetRigidbody.angularVelocity.magnitude > MinSpeed)
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
        var targetPosition = Target.transform.localPosition + TargetPositionOffset;
        transform.LookAt(targetPosition);
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, Target.transform.position + CameraPositionOffset, ref velocity, lerpValue);
    }

    public void RotateCameraAroundPlayer(Vector2 deltaIncrement)
    {

        if ((Mathf.Abs(deltaIncrement.x) / Screen.width) > MinScreenPercentageForRotation.x)
        {
            var increment = deltaIncrement.x * CameraRotationSpeed * Time.deltaTime;
            transform.RotateAround(Target.transform.position, Vector3.up, increment);
        }

        if ((Mathf.Abs(deltaIncrement.y) / Screen.height) > MinScreenPercentageForRotation.y)
        {
            var incrementY = (deltaIncrement.y / Screen.height) * Time.deltaTime * ZoomSpeed;
            Zoom = Zoom + incrementY;
            CameraPositionOffset.y = MinCameraHeight + (MaxCameraHeight - MinCameraHeight) * Zoom;
        }

        var targetPosition = Target.transform.localPosition + TargetPositionOffset;
        transform.LookAt(targetPosition);
        var newPosition = transform.localPosition;
        newPosition.y = Mathf.Lerp(transform.localPosition.y, 
                                             (Target.transform.position + CameraPositionOffset).y, 0.8f);
        transform.localPosition = newPosition;
    }

}
