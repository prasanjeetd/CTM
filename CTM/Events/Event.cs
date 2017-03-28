using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM
{
    public abstract class Event
    {
        public string Title { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public int Duration { get; set; }

        public abstract Event Schedule();

    }
}
