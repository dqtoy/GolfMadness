using UnityEngine;

namespace BlastyEvents
{
    public class ShootEvent : BlastyEvent
    {
        public static string EventName = "ShootEvent";

        public override string GetEventName()
        {
            return EventName;
        }
    }

    public class ShootEventData : BlastyEventData
    {
        public bool ValidShot;
        public float Power;
    }
}