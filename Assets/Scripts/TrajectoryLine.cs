using System.Collections.Generic;
using BlastyEvents;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    public class TrajectoryStepData
    {
        public Vector3 originPoint;
        public Vector3 endPoint;
        public Vector3 direction;
        public float distance;
    }

    public float MaxLineLength = 10f;
    public int MaxReboundTries = 5;
    public float CollisionHeight = 0.25f;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _startDummy;
    [SerializeField] private GameObject _endDummy;
    [SerializeField] private GameObject _arrowModel;
    [SerializeField] float _directionAngleIncrement;
    [SerializeField] float _maxVerticalSizeScreenPercentage;
    [SerializeField] float _maxVerticalScale;

    List<TrajectoryStepData> _trajectorySteps = new List<TrajectoryStepData>();

    private int reboundLayerMask;

    Vector2 _initialDragPosition;
    Vector3 _initialPosition;
    float _curPower;

    Vector2 _onScreenInitialDirection;
    float _initialYRotation;

    public float Power { get { return _curPower; } }

    void Start()
    {
        reboundLayerMask = 1 << 9;
        ResetRotation();
        _initialPosition = transform.localPosition;
        FinishAiming();
        
        EventManager.Instance.StartListening(TouchEvent.EventName, OnPanUpdated);
        
    }



    private void OnPanUpdated(BlastyEventData ev)
    {
        var touchEventData = (TouchEventData) ev;

        if (touchEventData.PanType == TouchManager.PanType.World)
            return;
        
        switch (touchEventData.TouchState)
        {
            case TouchManager.TouchState.InitPan:
                _initialDragPosition = touchEventData.CurPosition;
                StartNewAiming();
                GetInitialDirectionOnScreenSpace();
                break;
            case TouchManager.TouchState.UpdatePan:
                MoveDirectionArrow(touchEventData);
                UpdateArrowSize(touchEventData.CurPosition);
                break;
            case TouchManager.TouchState.FinishPan:
                ResetRotation();
                FinishAiming();
                break;
        }
    }

    public void StartNewAiming()
    {
        gameObject.SetActive(true);
    }

    public void FinishAiming()
    {
        gameObject.SetActive(false);
    }

    void GetInitialDirectionOnScreenSpace()
    {
        var startPoint = Camera.main.WorldToScreenPoint(_playerController.transform.position);
        var endPoint = Camera.main.WorldToScreenPoint(_playerController.transform.position + _playerController.transform.forward * 5f);

        _initialYRotation = _playerController.transform.localRotation.y;
        _onScreenInitialDirection = (endPoint - startPoint).normalized;
    }

    void UpdateArrowSize(Vector2 curPosition)
    {
        float dragLength = Mathf.Abs(Vector2.Distance(curPosition, _initialDragPosition));
        var verticalPercentage = dragLength / Display.main.renderingHeight;

        //Debug.Log("DRAG LENGTH " + dragLength);

        verticalPercentage = Mathf.Abs(verticalPercentage);

        verticalPercentage = Mathf.Clamp(verticalPercentage, 0f, _maxVerticalSizeScreenPercentage);
        _curPower = (verticalPercentage / _maxVerticalSizeScreenPercentage);


        var finalSize = verticalPercentage * _maxVerticalScale;
        _arrowModel.transform.localScale = new Vector3(1f + finalSize, 1f + finalSize, 1 + finalSize);
        //Debug.Log("DISTANCE " + verticalPercentage + "  SIZE " + verticalPercentage * _maxVerticalScale * -1f);
    }

    void MoveDirectionArrow(TouchEventData touchEventData)
    {
        var initialDirection = touchEventData.CurPosition - touchEventData.InitPosition;
        
        var angles = Vector2.SignedAngle(touchEventData.DeltaIncrement, initialDirection);
        var dotProduct = Vector2.Dot(touchEventData.DeltaIncrement, initialDirection);

        angles = touchEventData.DeltaIncrement.x;
        Debug.Log("INCREMENT " + touchEventData.DeltaIncrement.x + "   ROT " + transform.localRotation.y + "   INCR " + angles);
        transform.localRotation = Quaternion.Euler(0f, transform.localRotation.y + angles, 0f);
        Debug.Log("POST INCREMENT " + touchEventData.DeltaIncrement.x + "   ROT " + transform.localRotation.y);
        //transform.localRotation *= Quaternion.Euler(0f, deltaIncrement.x * _directionAngleIncrement, 0f);
    }

    private Vector3 _originDirPos, _endDirPos;
    public Vector3 GetAimingDirection()
    {
        _originDirPos = _startDummy.transform.position;
        _originDirPos.y = CollisionHeight;

        _endDirPos = _endDummy.transform.position;
        _endDirPos.y = CollisionHeight;
        
        return (_originDirPos - _endDirPos).normalized;
    }

    void ResetRotation()
    {
        transform.localPosition = _initialPosition;
        transform.localRotation = Quaternion.identity;
    }
}