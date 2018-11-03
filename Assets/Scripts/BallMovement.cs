using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private TrajectoryLine _trajectoryLine;
    [SerializeField] float _shootPower;
    

    private Rigidbody _rigidbody;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        UpdateInput();
    }


    void UpdateInput()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Shoot();
        }

    }

    public void ResetToPosition(Vector3 pos)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.position = pos;
        
    }

    void Shoot()
    {
        _rigidbody.AddForce(_trajectoryLine.GetAimingDirection() * _shootPower, ForceMode.Impulse);
    }
}