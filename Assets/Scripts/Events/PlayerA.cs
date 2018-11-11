using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleEventClassA : MonoBehaviour
{
    [SerializeField]
    ExampleEvent projectileEvent;

    void Update()
    {
        // Register event
        if (Input.GetKeyUp(KeyCode.A))
        {
            EventManager.RegisterEvent(projectileEvent.GetEventName(), projectileEvent);
        }

        // Trigger event
        if (Input.GetKeyUp(KeyCode.C))
        {
            ExampleEventData data = new ExampleEventData();
            data.x = 999;
            data.y = 555;
            EventManager.TriggerEvent(projectileEvent.GetEventName(), data);
        }

    }
}
