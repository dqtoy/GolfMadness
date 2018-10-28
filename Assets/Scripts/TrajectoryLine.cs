using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private BallMovement _ball;
    [SerializeField] private GameObject _startDummy;
    [SerializeField] private GameObject _endDummy;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] float _directionAngleIncrement;
    
    List<TrajectoryStepData> _trajectorySteps = new List<TrajectoryStepData>();

    private int reboundLayerMask;
    
    
    void Start()
    {
        reboundLayerMask = 1 << 9;
    }

    
    void Update()
    {
        UpdateInput();
    }

    void UpdateInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveDirectionArrow(1f);
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveDirectionArrow(-1f);
        }

        ProjectNewTrajectory(transform.position, GetAimingDirection());

    }
    
    
    void MoveDirectionArrow(float increment)
    {
        increment *= _directionAngleIncrement;
        
        transform.RotateAround(_ball.transform.position, Vector3.up, increment);
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
    
    public void ProjectNewTrajectory(Vector3 originPoint, Vector3 direction)
    {
        _trajectorySteps.Clear();
        
        var initialPoint = originPoint;
        var initialDirection = direction;

        float accumulatedDistance = 0f;

        int numTries = 0;

        while(accumulatedDistance < MaxLineLength && numTries < MaxReboundTries)
        {
            ++numTries;
            initialPoint.y = CollisionHeight;
            initialDirection.y = 0f;

            RaycastHit hit = new RaycastHit();
            if (!GetReboundPosition(initialPoint, initialDirection, ref hit))
            {
                numTries = 0;
                break;
            }

            //Debug.DrawLine(hit.point, (hit.point + Vector3.up*5f), Color.yellow);

            var trajectoryStep = new TrajectoryStepData();
            trajectoryStep.originPoint = initialPoint;
            trajectoryStep.direction = initialDirection;

            float reboundLength = Vector3.Distance(hit.point, initialPoint);

            if((accumulatedDistance + reboundLength) > MaxLineLength)
            {
                reboundLength = MaxLineLength - accumulatedDistance;
                accumulatedDistance = MaxLineLength;
                trajectoryStep.endPoint = initialPoint + (initialDirection * reboundLength);
                numTries = 0;
            }
            else
            {
                trajectoryStep.endPoint = hit.point;
                accumulatedDistance += reboundLength;
            }

            _trajectorySteps.Add(trajectoryStep);

            initialPoint = hit.point;
            initialDirection = Vector3.Reflect(initialDirection, hit.normal);
        }

        DrawTrajectories();
    }

    bool GetReboundPosition(Vector3 originPoint, Vector3 direction, ref RaycastHit hit)
    {
        //Debug.DrawRay(originPoint, direction * 20, Color.magenta);
        if (!Physics.Raycast(originPoint, direction, out hit, Mathf.Infinity, reboundLayerMask))
        {
            return false;
        }

        //Debug.DrawLine(originPoint, originPoint + Vector3.up * 10f, Color.green);
        return true;
    }

    void DrawTrajectories()
    {
        int index = 0;
        _lineRenderer.positionCount = _trajectorySteps.Count * 2;
        
        foreach (var trajectoryStep in _trajectorySteps)
        {
            _lineRenderer.SetPosition(index, trajectoryStep.originPoint);
            _lineRenderer.SetPosition(index + 1, trajectoryStep.endPoint);
            index += 2;
        }
    }
}