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
    
    void Start ()
    {
        _panType = PanType.None;
        _touchState = TouchState.None;
        
        _touchEvent = new TouchEvent();
        _touchEvent.Initialize();
    }

    void Update ()
    {
        //UpdateMobileInput();
        UpdateDesktopInput();

    }

    void UpdateDesktopInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _touchState = TouchState.InitPan;
            _initPanPosition = Input.mousePosition;
            _prevPanPosition = Vector2.zero;
            _curPanPosition = Input.mousePosition;;

            var ray = Camera.main.ScreenPointToRay(new Vector3(_curPanPosition.x, _curPanPosition.y, 100));
            if (Physics.Raycast(ray, 9999f, 29))
            {
                _panType = PanType.Player;
            }
            else
            {
                _panType = PanType.World;
            }

            TriggerTouchEvent(_curPanPosition, Vector2.zero);
            return;
        }

        if (Input.GetButton("Fire1"))
        {
            _touchState = TouchState.UpdatePan;
            _prevPanPosition = _curPanPosition;
            _curPanPosition = Input.mousePosition;
            
            TriggerTouchEvent(Input.mousePosition, _curPanPosition - _prevPanPosition);
            Debug.Log("b");
        }
        
        if (Input.GetButtonUp("Fire1"))
        {
            Debug.Log("c");
            
        }
        
        /*

            case TouchState.InitPan:
                if (_panTouchId != touch.fingerId)
                {
                    return;
                }

                _prevPanPosition = _curPanPosition;
                _curPanPosition = touch.position;
                _touchState = TouchState.UpdatePan;
                TriggerTouchEvent(touch);


                break;

            case TouchState.UpdatePan:
                if (_panTouchId != touch.fingerId)
                {
                    return;
                }

                _prevPanPosition = _curPanPosition;
                _curPanPosition = touch.position;
                TriggerTouchEvent(touch);
                break;

            case TouchState.FinishPan:
                _touchState = TouchState.FinishPan;
                TriggerTouchEvent(touch);
                break;
                }
                */
    }
    
    /*
    void UpdateMobileInput()
    {
         Debug.Log("TOUCHES " + Input.touches.Length);
        foreach(var touch in Input.touches) 
        {
            switch (_touchState)
            {
                case TouchState.None:
                    if (touch.phase == TouchPhase.Began)
                    {
                        _panTouchId = touch.fingerId;
                        _touchState = TouchState.InitPan;
                        _initPanPosition = touch.position;
                        _prevPanPosition = Vector2.zero;
                        _curPanPosition = touch.position;
                        
                        var touchEventData = new TouchEventData();
                        touchEventData.TouchState = _touchState;
                        touchEventData.CurPosition = _curPanPosition;
                        touchEventData.DeltaIncrement = touch.deltaPosition;
                        touchEventData.InitPosition = _initPanPosition;

                        var ray = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 100));
                        if (Physics.Raycast(ray, 9999f, 28))
                        {
                            _panType = PanType.Player;
                        }
                        else
                        {
                            _panType = PanType.World;
                        }

                        TriggerTouchEvent(touch);
                        return;
                    }

                    break;
                case TouchState.InitPan:
                    if (_panTouchId != touch.fingerId)
                    {
                        return;
                    }

                    _prevPanPosition = _curPanPosition;
                    _curPanPosition = touch.position;
                    _touchState = TouchState.UpdatePan;
                    TriggerTouchEvent(touch);

                    
                    break;

                case TouchState.UpdatePan:
                    if (_panTouchId != touch.fingerId)
                    {
                        return;
                    }

                    _prevPanPosition = _curPanPosition;
                    _curPanPosition = touch.position;
                    TriggerTouchEvent(touch);
                    break;

                case TouchState.FinishPan:
                    _touchState = TouchState.FinishPan;
                    TriggerTouchEvent(touch);
                    break;
            }

        }
    }
    */
    
    void TriggerTouchEvent(Vector2 curPosition, Vector2 deltaIncrement)
    {
        var touchEventData = new TouchEventData();
        touchEventData.TouchState = _touchState;
        touchEventData.CurPosition = _curPanPosition;
        touchEventData.DeltaIncrement = deltaIncrement;
        touchEventData.InitPosition = _initPanPosition;

        var ray = Camera.main.ScreenPointToRay(new Vector3(curPosition.x, curPosition.y, 100));
        if (Physics.Raycast(ray, 9999f, 28))
        {
            touchEventData.PanType = PanType.Player;
        }
        else
        {
            touchEventData.PanType = PanType.World;
        }
                        
        EventManager.Instance.TriggerEvent(TouchEvent.EventName, touchEventData);
    }
}
