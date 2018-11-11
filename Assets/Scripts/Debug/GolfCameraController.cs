using UnityEngine;
using TouchScript.Gestures;

public class GolfCameraController : MonoBehaviour
{
    [SerializeField] GameObject Target;
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

        SetInitialCamera();
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

    public void OnGestureStateChanged(Gesture sender)
    {
        switch (sender.State)
        {
            case Gesture.GestureState.Began:
                //_initialDragPosition = sender.ScreenPosition;
                //GetInitialDirectionOnScreenSpace();
                break;
            case Gesture.GestureState.Changed:
                RotateCameraAroundPlayer(sender);
                //MoveDirectionArrow((_initialDragPosition - sender.ScreenPosition).normalized);
                //UpdateArrowSize(sender.ScreenPosition);
                break;
            case Gesture.GestureState.Ended:
            case Gesture.GestureState.Failed:
            case Gesture.GestureState.Cancelled:
                //ResetRotation();
                break;

        }
    }

    void RotateCameraAroundPlayer(Gesture sender)
    {
        var increment = sender.ScreenPosition.x - sender.PreviousScreenPosition.x;
        transform.RotateAround(Target.transform.position, Vector3.up, increment);
    }
}
