using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM
{
    public class AfternoonSession : Session
    {
        public AfternoonSession(List<Talk> talks, Session session, Track track)
            :base(talks,session,track)
        {
            SetTitle();
        }

        public AfternoonSession(TimeSpan startTime, TimeSpan endTime)
            :base(startTime,endTime)
        {
            SetTitle();
        }

        private void SetTitle()
        {
            this.Title = "Afternoon Session";
        }

    }
}
