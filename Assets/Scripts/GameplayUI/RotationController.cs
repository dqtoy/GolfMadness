using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationAmount;

    void Update()
    {
        var rotation = transform.rotation.eulerAngles + _rotationAmount * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rotation);
    }
}