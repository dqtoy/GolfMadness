using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjective : Objective
{
    public ObjectiveCollisionableContainer Target;

    public void OnCollisionableContainerTriggers(bool inside)
    {
        OnObjectiveUpdated(inside, this);
    }
}