using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM
{
    public class Conference
    {
        #region " CONSTRUCTORS "

        public Conference()
        {
            this.TalksToSchedule = new List<Talk>();
            this.ScheduledTracks = new List<Track>();
        }

        public Conference(DayFormat trackFormat) : this()
        {
            this.DayFormat = trackFormat;
        }

        #endregion

        #region " PROPERTIES "

        public List<Talk> TalksToSchedule { get; set; }

        public DayFormat DayFormat { get; set; }

        public List<Track> ScheduledTracks { get; set; }

        #endregion

        #region " PRIVATE METHODS "

        private Event HandleSession(Session session, Track track)
        {
            var scheduler = new SessionScheduler(this.TalksToSchedule, session, track);
            var @event = scheduler.Schedule();
            return @event;
        }

        private Event HandleSingleEvent(SingleEvent singleEvent, Track track)
        {
            Event @event=  EventFactory.GetInstance(singleEvent, track);
            @event.Schedule();

            return @event;
        }

        #endregion

        #region " PUBLIC METHOD "

        public List<Track> Schedule()
        {
            TalksToSchedule = TalksToSchedule.OrderByDescending(x => x.Duration).ToList();

            while (TalksToSchedule.Count != 0)
            {
                var track = new Track();
                foreach (var @event in DayFormat.Events)
                {
                    if (@event is Session)
                    {
                        if (TalksToSchedule.Count == 0)
                            break;

                        Event session = HandleSession(@event as Session, track);
                        track.Events.Add(session);
                    }
                    else
                    {
                        Event singleEvent = HandleSingleEvent(@event as SingleEvent, track);
                        track.Events.Add(singleEvent);
                    }
                }

                this.ScheduledTracks.Add(track);
            }

            return this.ScheduledTracks;
        }

        #endregion
    }
}
