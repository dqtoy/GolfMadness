using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject _initialPosition;
    
    [SerializeField] private BallMovement _ball;
    
    void Start()
    {
    }

    
    void Update()
    {
        UpdateInput();
    }

    void UpdateInput()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            _ball.ResetToPosition(_initialPosition.transform.position);
        }

    }
}