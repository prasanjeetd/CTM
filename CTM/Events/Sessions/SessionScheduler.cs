using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM
{
    public class SessionScheduler
    {
        private List<Talk> _talks;
        private Session _session;
        private Track _track;

        public SessionScheduler(List<Talk> talks, Session session, Track track)
        {
            _talks = talks;
            _session = session;
            _track = track;
        }

        private Session GetSessionFactory()
        {

            if (_session is MorningSession)
                return new MorningSession(_talks, _session, _track);
            else if (_session is AfternoonSession)
                return new AfternoonSession(_talks, _session, _track);
            else
                return new Session(_talks, _session, _track);

        }
                        
        public Event Schedule()
        {
            return GetSessionFactory().Schedule();

        }
    }
}
