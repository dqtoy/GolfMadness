using BlastyEvents;
using UnityEngine;

public class GolfCameraController : MonoBehaviour
{
    public GameObject Target;
    [SerializeField] Vector3 TargetPositionOffset;
    [SerializeField] Vector3 CameraPositionOffset;

    [Range(0,10)]
    [SerializeField] float lerpValue = 1;

    [Range(0, 1)]
    [SerializeField] float MinSpeed = 1;

    Vector3 _initialPosition;

    private Vector3 velocity = Vector3.zero;
    private Rigidbody _targetRigidbody;

    void Start ()
    {
        _initialPosition = transform.localPosition;
        _targetRigidbody = Target.GetComponent<Rigidbody>();

        EventManager.Instance.StartListening(TouchEvent.EventName, OnPanUpdated);
        SetInitialCamera();
    }

    private void OnPanUpdated(BlastyEventData ev)
    {
        var touchEventData = (TouchEventData) ev;

        if (touchEventData.PanType == TouchManager.PanType.Player)
        {
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

    void Update ()
    {
        if (_targetRigidbody.velocity.magnitude > MinSpeed || _targetRigidbody.angularVelocity.magnitude > MinSpeed)
        {
            UpdateCamera();
        }
    }

    public void SetInitialCamera()
    {
        var targetPosition = Target.transform.localPosition + TargetPositionOffset;
        transform.localPosition = Target.transform.position + CameraPositionOffset;
        transform.LookAt(targetPosition);
    }

    void UpdateCamera()
    {
        var targetPosition = Target.transform.localPosition + TargetPositionOffset;
        transform.LookAt(targetPosition);
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, Target.transform.position + CameraPositionOffset, ref velocity, lerpValue);
    }

    void RotateCameraAroundPlayer(Vector2 deltaIncrement)
    {
        transform.RotateAround(Target.transform.position, Vector3.up, deltaIncrement.x);
    }
}
