using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Example of elemental receiver
public class ElementalMoving : ElementalDebug
{
    [SerializeField] private Rigidbody _rigidbody;
    
    protected override void CollisionableCollisionEnter(Collision other)
    {
        var playerController = other.gameObject.GetComponent<PlayerController>();

        if (!playerController)
        {
            _rigidbody.isKinematic = false;
            return;
        }
        
        var otherElemental = other.gameObject.GetComponent<Elemental>();
        
        if (otherElemental != null && otherElemental.CurrentElement != CurrentElement)
        {
            _rigidbody.isKinematic = true;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.velocity = Vector3.zero;
        }
        else
        {
            _rigidbody.isKinematic = false;
        }
    }
}