using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM
{
    public class Session : Event
    {
        #region  " PROTECTED VARIABLES "

        protected List<Talk> _talks;
        protected Session _session;
        protected Track _track;

        #endregion

        #region " CONSTRUCTORS "

        public Session()
        {
            this.Title = "Session";
            this.Talks = new List<Talk>();
        }

        public Session(Session session, List<Talk> talks, Track track)
        {
            this.StartTime = session.StartTime;
            this.EndTime = session.EndTime;
            this.Title = session.Title;
            this.Duration = session.Duration;
            this.SpareTime = session.SpareTime;
            this._talks = talks;
            this._track = track;
            this._session = session;
            this.Talks = new List<Talk>();
        }

        public Session(TimeSpan startTime, TimeSpan endTime) : this()
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Duration = TimeHelper.GetDuration(this.StartTime, this.EndTime);
            this.SpareTime = this.Duration;

        }

        public Session(List<Talk> talks, Session session, Track track)
            : this(session.StartTime, session.EndTime)
        {
            _talks = talks;
            _session = session;
            _track = track;
        }

        #endregion

        #region " PROPERTIES "

        public List<Talk> Talks { get; set; }

        public int SpareTime { get; set; }

        #endregion

        #region " PRIVATE METHODS "

        private TimeSpan AddTalk(Session session, Talk talk, TimeSpan initialStartTime)
        {
            talk.StartTime = initialStartTime;
            talk.EndTime = initialStartTime.Add(TimeSpan.FromMinutes(talk.Duration));
            initialStartTime = talk.EndTime;
            session.Talks.Add(talk);
            session.SpareTime -= talk.Duration;

            return initialStartTime;
        }

        private void SetTalks(List<Session> sessionList)
        {
            var sessionHavingMinSpareTime = sessionList.OrderBy(x => x.SpareTime).FirstOrDefault();

            List<int> talkIds = sessionHavingMinSpareTime.Talks.Select(x => x.Id).ToList();
            _talks.RemoveAll(x => talkIds.Contains(x.Id));

            this.Talks = sessionHavingMinSpareTime.Talks;
            this.SpareTime = sessionHavingMinSpareTime.SpareTime;
        }

        private bool doesTalkNeedsGap(Talk previousTalk,Talk currentTalk)
        {
            var result = !(previousTalk.Duration == (int)TimeUnit.Lightning
                        && currentTalk.Duration == (int)TimeUnit.Lightning);

            return result;
        }

        private void ProcessTalks(Session session, List<Talk> talks, int startCnt, TimeSpan initialStartTime)
        {
            int talkGap = 10;

            for (int i = startCnt + 1; i < talks.Count; i++)
            {
                var talk = talks[i];

                ///*
                var spareTime = talk.Duration;

                var doesTalksNeedGap =doesTalkNeedsGap(talks[i-1],talk);
                if (doesTalksNeedGap)
                    {
                        spareTime =+ talkGap;
                    }

                if (spareTime > session.SpareTime)
                    continue;

                if (doesTalksNeedGap)
                 initialStartTime =initialStartTime.Add(TimeSpan.FromMinutes(talkGap));

               // */

                //if (talk.Duration > session.SpareTime)
                //    continue;

                initialStartTime = AddTalk(session, talk, initialStartTime);

                if (session.SpareTime == 0)
                    break;

            }
        }

        #endregion

        #region " PUBLIC METHOD "

        public override Event Schedule()
        {
            List<Session> sessionList = new List<Session>();

            for (int i = 0; i < _talks.Count; i++)
            {
                var talks = ObjectCopier.Clone(_talks);
                Session session = new Session(this, talks, _track);
                var talk = talks[i];

                TimeSpan initialStartTime = this.StartTime;
                if (talk.Duration <= session.SpareTime)
                    initialStartTime = AddTalk(session, talk, initialStartTime);

                if (session.SpareTime != 0)
                    ProcessTalks(session, talks, i, initialStartTime);

                sessionList.Add(session);
                if (session.SpareTime == 0 || session.Talks.Count == talks.Count)
                    break;

            }

            if (sessionList.Count > 0)
            {
                SetTalks(sessionList);
            }

            return this;

        }

        #endregion

    }
}
