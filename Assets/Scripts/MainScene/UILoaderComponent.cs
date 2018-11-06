using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoaderComponent : LogicComponent
{
    public string PrefabPath;
    
    public override void Initialize()
    {
        var uiElement = ServicesManager.Instance.UIStackController.GetView(PrefabPath);
        ServicesManager.Instance.UIStackController.Push(uiElement);
    }
}