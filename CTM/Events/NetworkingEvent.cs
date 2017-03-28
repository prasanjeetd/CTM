using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM
{
    public class NetworkingEvent : SingleEvent
    {
        public TimeSpan MaxStartTime { get; set; }

        public NetworkingEvent(TimeSpan startTime, TimeSpan maxStartTime)
            : base(startTime, startTime, "Networking Event")
        {
            this.MaxStartTime = maxStartTime;
        }

        public override Event Schedule()
        {
            //base.Schedule();

            var lastEvent = this.Track.Events.Last();

            if (lastEvent is Session)
            {
                var session = lastEvent as Session;
                if (session.Talks.Count > 0)
                {
                    var lastTalk = session.Talks.Last();

                    if (lastTalk.EndTime > this.StartTime)
                    {
                        this.Info.StartTime = lastTalk.EndTime;
                        this.Info.EndTime = this.Info.StartTime;
                    }
                }
            }

            return this;
        }
    }
}
