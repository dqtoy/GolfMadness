using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAddForce : Elemental {

    [SerializeField] float Power;

    [SerializeField] Transform DirectionTransform;

    protected override void CollisionableTriggerEnter(Collider other)
    {
        var rigidbody = other.gameObject.GetComponent<Rigidbody>();
        rigidbody.AddForce(DirectionTransform.up * Power, ForceMode.Impulse);
    }
}
