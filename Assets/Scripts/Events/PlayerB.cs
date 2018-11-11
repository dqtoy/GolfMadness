using UnityEngine;

public class ExampleEventClassB : MonoBehaviour
{
 
    // Update is called once per frame
    void Update()
    {
        // Subscribe to event
        if(Input.GetKeyUp(KeyCode.B))
        {
            EventManager.StartListening(ExampleEvent.EventName, OnEventReceived);
        }
    }

    void OnEventReceived(GameEventData gameEventData)
    {
        var aux = (ExampleEventData)gameEventData;
        Debug.Log("OnEventReceived 1    " + aux.x + "  " + aux.y);
    }

    public void OnEventReceived2(GameEventData obj)
    {
        ExampleEventData aux = (ExampleEventData)obj;
        Debug.Log("OnEventReceived2    " + aux.x + "  " + aux.y);
    }
}
