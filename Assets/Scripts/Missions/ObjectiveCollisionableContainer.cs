using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveCollisionableContainer : Elemental
{
    protected override void CollisionableTriggerEnter(Collider other)
    {
        var moveObjective = other.gameObject.GetComponent<MoveObjective>();
        if (moveObjective != null && moveObjective.Target == this)
        {
            moveObjective.OnCollisionableContainerTriggers(true);
        }
    }

    protected override void CollisionableTriggerExit(Collider other)
    {
        var moveObjective = other.gameObject.GetComponent<MoveObjective>();
        if (moveObjective != null && moveObjective.Target == this)
        {
            moveObjective.OnCollisionableContainerTriggers(false);
        }
    }
}