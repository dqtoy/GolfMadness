using System.Collections.Generic;
using UnityEngine;

public class SpartaxComponentManager : MonoBehaviour
{
    [HideInInspector]
    public List<LogicComponent> LogicComponents;
	
    void Awake()
    {
        LogicComponents = new List<LogicComponent>(FindObjectsOfType<LogicComponent>());

        for (int i = 0; i < LogicComponents.Count; i++)
        {
            LogicComponents[i].Initialize();
        }
    }
}