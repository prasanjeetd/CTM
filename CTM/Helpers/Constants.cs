using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM
{
    public enum TimeUnit
    {
        Min = 1,
        Lightning = 5
    }

    public class ExceptionMessage
    {
        public const string InvalidTime = "Invalid Time or Unit";

        public const string TitleContainsNumber = "Title contains number in it";

        public const string TalkDurationExceedsMaxEventDuration = "Talk duration exceeding longest session duration time of Min:";
    }
}
