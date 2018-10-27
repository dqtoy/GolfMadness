using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalGiverByCollision : ElementalDebug 
{	
	protected override void CollisionableCollisionEnter(Collision other)
	{
		var absorber = other.gameObject.GetComponent<ElementalAbsorber>();
		if (absorber != null)
		{
			absorber.CurrentElement = CurrentElement;
		}

		base.CollisionableCollisionEnter(other);
	}
}
