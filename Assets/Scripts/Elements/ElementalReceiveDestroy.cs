using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Example of elemental receiver
public class ElementalReceiveDestroy : ElementalDebug
{
    protected override void CollisionableCollisionEnter(Collision other)
    {
        var otherElemental = other.gameObject.GetComponent<Elemental>();
        if (CurrentElement == Element.ANY || otherElemental.CurrentElement == CurrentElement)
        {
            Destroy(gameObject);
        }
    }
}