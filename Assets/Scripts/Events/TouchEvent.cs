using UnityEngine;

namespace BlastyEvents
{
    public class TouchEvent : BlastyEvent
    {
        public static string EventName = "TouchEvent";

        public override string GetEventName()
        {
            return EventName;
        }
    }

    public class TouchEventData : BlastyEventData
    {
        public TouchManager.TouchState TouchState;
        public Vector2 DeltaIncrement;
        public Vector2 InitPosition;
        public Vector2 CurPosition;
        public TouchManager.PanType PanType;
        public Vector2 CurDirection;
        public float TotalPanScreenPercentageSize;
    }
}