using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using BlastyEvents;

public class EventManager
{
    static EventManager _instance;
    private Dictionary<string, BlastyEvent> _eventDictionary;
    
    public static EventManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new EventManager();
                _instance._eventDictionary = new Dictionary<string, BlastyEvent>();
            }

            return _instance;
        }
    }

    public void RegisterEvent(BlastyEvent blastyEvent)
    {
        _eventDictionary.Add(blastyEvent.GetEventName(), blastyEvent);
    }

    public void UnRegisterEvent(BlastyEvent blastyEvent)
    {
        if(_eventDictionary.ContainsKey(blastyEvent.GetEventName()))
        {
            _eventDictionary.Remove(blastyEvent.GetEventName());
        }
    }
    
    public void StartListening(string eventName, UnityAction<BlastyEventData> listener)
    {
        BlastyEvent thisEvent = null;
        if (_eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
    }

    public void StopListening(string eventName, UnityAction<BlastyEventData> listener)
    {
        if (Instance == null) return;
        BlastyEvent thisEvent = null;
        if (_eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public void TriggerEvent(string eventName, BlastyEventData arg = null)
    {
        BlastyEvent thisEvent = null;
        if (_eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(arg);
        }
    }

    public void ResetAllEvents()
    {
        _eventDictionary.Clear();
    }
}