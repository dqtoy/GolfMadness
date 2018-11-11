using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private Dictionary<string, GameEvent> _eventDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _eventDictionary = new Dictionary<string, GameEvent>();
        }
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public static void RegisterEvent(string eventName, GameEvent gameEvent)
    {
        Instance._eventDictionary.Add(eventName, gameEvent);
    }

    public static void StartListening(string eventName, UnityAction<GameEventData> listener)
    {
        GameEvent thisEvent = null;
        if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }

    }

    public static void StopListening(string eventName, UnityAction<GameEventData> listener)
    {
        if (Instance == null) return;
        GameEvent thisEvent = null;
        if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, GameEventData arg = null)
    {
        GameEvent thisEvent = null;
        if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(arg);
        }
    }
}
