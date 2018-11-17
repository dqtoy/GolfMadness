using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using BlastyEvents;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public enum PanType
    {
        None,
        Player,
        World
    }

    public enum TouchState
    {
        None,
        InitPan,
        UpdatePan,
        FinishPan
    }
    
    private bool _inPanGesture = false;
    private Vector2 _initPanPosition;
    private Vector2 _prevPanPosition;
    private Vector2 _curPanPosition;
    private PanType _panType;
    private TouchState _touchState;
    private int _panTouchId;

    private TouchEvent _touchEvent;
    private ShootEvent _shootEvent;
    
    private int _touchLayer;
    
    void Start ()
    {
        _panType = PanType.None;
        _touchState = TouchState.None;

        _touchLayer = 1 << 29;
        
        _touchEvent = new TouchEvent();
        _touchEvent.Initialize();
        
        _shootEvent = new ShootEvent();
        _shootEvent.Initialize();
    }

    void Update ()
    {
        //UpdateMobileInput();
        UpdateDesktopInput();
        //var ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        //Debug.DrawRay(ray.origin, ray.direction * 20f, Color.magenta, 1f);
    }

    void UpdateDesktopInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _touchState = TouchState.InitPan;
            _initPanPosition = Input.mousePosition;
            _prevPanPosition = Vector2.zero;
            _curPanPosition = Input.mousePosition;

            var ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            Debug.DrawRay(ray.origin, ray.direction * 20f, Color.magenta, 1f);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 9999f, _touchLayer))
            {
                _panType = PanType.Player;
            }
            else
            {
                _panType = PanType.World;
            }

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
            }
            
            //Debug.Log("START PAN WITH " + _panType);
            TriggerTouchEvent(Vector2.zero);
            return;
        }

        if (Input.GetButton("Fire1"))
        {
            _touchState = TouchState.UpdatePan;
            _prevPanPosition = _curPanPosition;
            _curPanPosition = Input.mousePosition;
            
            //Debug.Log("PREV " + _prevPanPosition + "   CUR: " + _curPanPosition + "   DELTA  "  + (_curPanPosition - _prevPanPosition));
            TriggerTouchEvent(_curPanPosition - _prevPanPosition);
        }
        
        if (Input.GetButtonUp("Fire1"))
        {
            _touchState = TouchState.FinishPan;
            TriggerTouchEvent(_curPanPosition - _prevPanPosition);
        }
    }
    
    void TriggerTouchEvent(Vector2 deltaIncrement)
    {
        var touchEventData = new TouchEventData();
        touchEventData.TouchState = _touchState;
        touchEventData.CurPosition = _curPanPosition;
        touchEventData.DeltaIncrement = deltaIncrement;
        touchEventData.InitPosition = _initPanPosition;
        touchEventData.PanType = _panType;
        touchEventData.CurDirection = (_curPanPosition - _initPanPosition).normalized;
         
        EventManager.Instance.TriggerEvent(TouchEvent.EventName, touchEventData);
    }
}
