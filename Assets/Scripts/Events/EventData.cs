using System;
using UnityEngine.Events;

namespace BlastyEvents
{
    [Serializable]
    public abstract class BlastyEvent : UnityEvent<BlastyEventData>
    {
        public void Initialize()
        {
            EventManager.Instance.RegisterEvent(this);
        }

        public abstract string GetEventName();

        public void Raise(BlastyEventData data)
        {
            EventManager.Instance.TriggerEvent(GetEventName(), data);
        }
    }

    public interface BlastyEventData { }
   
}