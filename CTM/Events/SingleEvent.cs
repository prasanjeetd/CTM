using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM
{
    public class SingleEvent : Event
    {
        public Talk Info { get; set; }

        public Track Track { get; set; }

        public SingleEvent(TimeSpan startTime, TimeSpan endTime,String title)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Duration = TimeHelper.GetDuration(this.StartTime, this.EndTime);
            this.Info = new Talk {
                Title = title,
                StartTime = StartTime,
                EndTime = EndTime,
                Unit = ((Duration==(int)TimeUnit.Lightning)?TimeUnit.Lightning : TimeUnit.Min )
                };
        }

        public override Event Schedule()
        {
            //this.Info = new Talk { Title = Title, StartTime = StartTime, EndTime = EndTime };
            return this;
        }


    }
}
