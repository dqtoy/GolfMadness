using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAddForce : Elemental
{
    [SerializeField] float Power;

    protected override void CollisionableCollisionEnter(Collision collision)
    {
        if (collision.contacts.Length == 0)
        {
            return;
        }


        Debug.DrawRay(collision.contacts[0].point, collision.contacts[0].normal * -10f, Color.magenta, 2f);
        var rigidbody = collision.other.gameObject.GetComponent<Rigidbody>();
        rigidbody.AddForce(collision.contacts[0].normal * -1f * Power, ForceMode.Impulse);
    }
}