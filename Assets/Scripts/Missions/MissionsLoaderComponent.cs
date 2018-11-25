using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsLoaderComponent : LogicComponent
{
    public override void Initialize()
    {
        ServicesManager.Instance.MissionsManager.InitLevelObjectives();
    }
}