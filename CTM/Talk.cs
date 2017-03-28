using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CTM
{
    public class Talk
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Duration { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public TimeUnit Unit {get;set;}

    }
}
