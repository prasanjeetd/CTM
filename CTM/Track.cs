using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM
{
    public class Track
    {
        public List<Event> Events { get; set; }

        public Track()
        {
            this.Events = new List<Event>();
        }

    }
}
