using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalStealer : ElementalDebug 
{
	protected override void CollisionableTriggerEnter(Collider other)
	{
		var absorber = other.gameObject.GetComponent<ElementalAbsorber>();
		if (absorber != null)
		{
			var oldElement = absorber.CurrentElement;
			absorber.CurrentElement = CurrentElement;
			CurrentElement = oldElement;
		}

		base.CollisionableTriggerEnter(other);
	}

}
