using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjective : Objective
{
    public ObjectiveCollisionableContainer Target;

    public bool DestroyOnComplete = true;
    
    public void OnCollisionableContainerTriggers(bool inside)
    {
        OnObjectiveUpdated(inside, this);

        if (DestroyOnComplete)
        {
            Destroy(gameObject);
        }
    }
}