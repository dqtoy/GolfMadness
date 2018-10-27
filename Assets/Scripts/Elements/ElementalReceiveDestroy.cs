using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Example of elemental receiver
public class ElementalReceiveDestroy : ElementalDebug
{
    protected override void CollisionableCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}