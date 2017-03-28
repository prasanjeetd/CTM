using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM
{
    public class Lunch : SingleEvent
    {
        public Lunch(TimeSpan startTime, TimeSpan endTime)
            : base(startTime, endTime, "Lunch")
        {
        }
    }
}
