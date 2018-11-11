using UnityEngine.Events;
using System;

public interface IEventData { }

[Serializable]
public abstract class GameEvent : UnityEvent<GameEventData>
{
    public void Initialize()
    {
        EventManager.RegisterEvent(GetEventName(), this);
    }

    public abstract string GetEventName();
}

[Serializable]
public class ExampleEvent : GameEvent
{
    public static string EventName = "exampleevent";
    public override string GetEventName() { return EventName; }
}

public interface GameEventData { }

public class ExampleEventData : GameEventData
{
    public int x;
    public int y;
}