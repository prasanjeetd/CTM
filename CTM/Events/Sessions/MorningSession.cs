using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM
{
    public class MorningSession : Session
    {

        public MorningSession(List<Talk> talks, Session session, Track track)
            :base(talks,session,track)
        {
            SetTitle();
        }

        public MorningSession(TimeSpan startTime, TimeSpan endTime)
            :base(startTime,endTime)
        {
            SetTitle();
        }

        private void SetTitle()
        {
            this.Title = "Morning Session";
        }
       
    }
}
