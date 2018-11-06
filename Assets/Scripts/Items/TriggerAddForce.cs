using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAddForce : MonoBehaviour {

    [SerializeField] float Power;

    [SerializeField] Transform DirectionTransform;

    private void OnTriggerEnter(Collider other)
    {
        var rigidbody = other.gameObject.GetComponent<Rigidbody>();
        rigidbody.AddForce(DirectionTransform.up * Power, ForceMode.Impulse);
    }
}
