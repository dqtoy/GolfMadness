using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAddForce : Elemental {

    [SerializeField] float Power;
    [SerializeField] Transform DirectionTransform;
    [SerializeField] bool _overrideForce = true;


    protected override void CollisionableTriggerEnter(Collider other)
    {
        var rigidbody = other.gameObject.GetComponent<Rigidbody>();
        if(_overrideForce)
        {
            rigidbody.velocity = Vector3.zero;
        }
        rigidbody.AddForce(DirectionTransform.forward * Power, ForceMode.Impulse);
    }
}
