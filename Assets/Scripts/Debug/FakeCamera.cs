using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCamera : MonoBehaviour
{
    [SerializeField] GameObject Target;

    Vector3 _initialOffset;

    void Start ()
    {
        _initialOffset = transform.position;	
	}
	
	void Update ()
    {
        transform.position = Vector3.Slerp(transform.position, Target.transform.position + _initialOffset, 0.1f);	
	}
}
