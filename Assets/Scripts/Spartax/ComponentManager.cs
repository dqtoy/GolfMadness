using System.Collections.Generic;
using UnityEngine;

public class ComponentManager : MonoBehaviour
{
    [HideInInspector]
    public List<LogicComponent> LogicComponents;
	
    void Awake()
    {
        LogicComponents = new List<LogicComponent>(GetComponents<LogicComponent>());

        for (int i = 0; i < LogicComponents.Count; i++)
        {
            LogicComponents[i].Initialize();
        }
    }
}