using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Collisionable : MonoBehaviour
{
    private Collider _collider;

    private Collider Collider
    {
        get
        {
            if (_collider == null)
            {
                _collider = gameObject.GetComponent<Collider>();
            }

            return _collider;
        }
    }

    protected virtual void CollisionableTriggerEnter(Collider other)
    {
    }

    protected virtual void CollisionableTriggerExit(Collider other)
    {
    }

    protected virtual void CollisionableTriggerStay(Collider other)
    {
    }

    protected virtual void CollisionableCollisionEnter(Collision other)
    {
    }

    protected virtual void CollisionableCollisionExit(Collision other)
    {
    }

    protected virtual void CollisionableCollisionStay(Collision other)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        CollisionableTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        CollisionableTriggerExit(other);
    }

    private void OnTriggerStay(Collider other)
    {
        CollisionableTriggerStay(other);
    }

    private void OnCollisionEnter(Collision other)
    {
        CollisionableCollisionEnter(other);
    }

    private void OnCollisionExit(Collision other)
    {
        CollisionableCollisionExit(other);
    }

    private void OnCollisionStay(Collision other)
    {
        CollisionableCollisionStay(other);
    }
}