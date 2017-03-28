using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM
{
    public class EventFactory
    {
        public static SingleEvent GetInstance(SingleEvent singleEvent, Track track)
        {
            SingleEvent @event;
            if (singleEvent is Lunch)
                @event = new Lunch(singleEvent.StartTime, singleEvent.EndTime);
            else if (singleEvent is NetworkingEvent)
                @event = new NetworkingEvent(singleEvent.StartTime, singleEvent.EndTime);
            else
                @event = new SingleEvent(singleEvent.StartTime, singleEvent.EndTime,"Single Event");

            @event.Track = track;

            return @event;
        }
    }
}
