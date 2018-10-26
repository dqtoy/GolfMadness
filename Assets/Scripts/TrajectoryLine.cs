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
    }

    public int TrajectoryRebounds = 3;
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
        initialPoint.y = CollisionHeight;
        
        var initialDirection = direction;
        
        for (int i = 0; i < TrajectoryRebounds; ++i)
        {
            RaycastHit hit = new RaycastHit();
            if (!GetReboundPosition(initialPoint, initialDirection, ref hit))
            {
                break;
            }

            var trajectoryStep = new TrajectoryStepData();
            trajectoryStep.originPoint = initialPoint;
            trajectoryStep.endPoint = hit.point;
            trajectoryStep.direction = initialDirection; 
            _trajectorySteps.Add(trajectoryStep);

            initialDirection = Vector3.Reflect(initialDirection, hit.normal);
            initialPoint = hit.point;
            initialPoint.y = CollisionHeight;
        }

        DrawTrajectories();
    }

    bool GetReboundPosition(Vector3 originPoint, Vector3 direction, ref RaycastHit hit)
    {
        Debug.DrawRay(originPoint, direction * 20, Color.magenta);
        if (!Physics.Raycast(originPoint, direction, out hit, Mathf.Infinity, reboundLayerMask))
        {
            return false;
        }

        Debug.DrawLine(originPoint, originPoint + Vector3.up * 10f, Color.green);
        return true;
    }

    void DrawTrajectories()
    {
        int index = 0;
        _lineRenderer.numPositions = _trajectorySteps.Count * 2;
        
        foreach (var trajectoryStep in _trajectorySteps)
        {
            _lineRenderer.SetPosition(index, trajectoryStep.originPoint);
            _lineRenderer.SetPosition(index + 1, trajectoryStep.endPoint);
            index += 2;
        }
    }
}