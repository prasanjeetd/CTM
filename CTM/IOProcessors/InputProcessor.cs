using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CTM
{
    public class InputProcessor
    {
        #region " PRIVATE VARIABLES "

        private readonly string _inputFilePath;
        private readonly Conference _conference;
        private Session _longestEvent;

        #endregion

        #region " CONSTRUCTOR "
       
        public InputProcessor(string inputFilePath)
        {
            _inputFilePath = inputFilePath;

            DayFormat trackFormat = new DayFormat();

            trackFormat.Events.Add(new MorningSession(TimeHelper.Parse("09:00 AM"), TimeHelper.Parse("12:00 PM")));
            trackFormat.Events.Add(new Lunch(TimeHelper.Parse("12:00 PM"), TimeHelper.Parse("01:00 PM")));
            trackFormat.Events.Add(_longestEvent = new AfternoonSession(TimeHelper.Parse("01:00 PM"), TimeHelper.Parse("05:00 PM")));
            /*
                Not using 4 PM Start Time for Networking Event because in the Sample Output of Thoughtworks Question
                it always Starts at 5 PM. See Track 2 Networking Event Time
                But from the statement "The networking event can start no earlier than 4:00 and no later than 5:00."
                I understand that the Event can start from 4 PM delayed upto 5 PM
                But Sample output for the Track 2 shows
                "04:00PM Rails for Python Developers lightning
                 05:00PM Networking Event"
                It could have started at 4:05 PM instead of 5 PM
            */
            //trackFormat.Events.Add(new NetworkingEvent(TimeHelper.Parse("04:00 PM"), TimeHelper.Parse("05:00 PM")));
            trackFormat.Events.Add(new NetworkingEvent(TimeHelper.Parse("05:00 PM"), TimeHelper.Parse("05:00 PM")));

            _conference = new Conference(trackFormat);

        }

        public InputProcessor(string inputFilePath, DayFormat trackFormat, Session longestEvent)
        {
            _longestEvent = longestEvent;
            _conference = new Conference(trackFormat);
        }

        #endregion

        #region " PRIVATE METHODS "

        private void ReadFile()
        {
            var lines = File.ReadAllLines(_inputFilePath);

            const string separator = " ";
            for (int i = 0; i < lines.Count(); i++)
            {
                var line = lines[i];
                var lastIndexOfSpace = line.Trim().LastIndexOf(separator);
                var unitValue = line.Substring(lastIndexOfSpace);
                var title = line.Substring(0, lastIndexOfSpace);

                Tuple<TimeUnit, int> unitDuration = GetUnitDuration(unitValue);

                ValidateInput(unitDuration, title);

                var talk = new Talk {
                    Id = i + 1,
                    Title = title,
                    Duration = unitDuration.Item2,
                    Unit = ((unitDuration.Item2 == (int)TimeUnit.Lightning) ? TimeUnit.Lightning : TimeUnit.Min)
                };
                _conference.TalksToSchedule.Add(talk);

            }

        }

        private Tuple<TimeUnit, int> GetUnitDuration(string unitValue)
        {

            if (unitValue.IndexOf(TimeUnit.Min.ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
            {
                String durationValue = Regex.Replace(unitValue, TimeUnit.Min.ToString(), "", RegexOptions.IgnoreCase);

                int duration;
                if (Int32.TryParse(durationValue, out duration))
                    return Tuple.Create(TimeUnit.Min, duration);
                else
                    return null;

            }
            else if (unitValue.IndexOf(TimeUnit.Lightning.ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return Tuple.Create(TimeUnit.Lightning, (int)TimeUnit.Lightning);
            }

            return null;

        }

        private void ValidateInput(Tuple<TimeUnit, int> unitDuration, string title)
        {
            if (unitDuration == null)
                throw new Exception(ExceptionMessage.InvalidTime);

            if (title.Any(char.IsDigit))
                throw new Exception(ExceptionMessage.TitleContainsNumber);

            if (unitDuration.Item2 > _longestEvent.Duration)
                throw new Exception(ExceptionMessage.TalkDurationExceedsMaxEventDuration + _longestEvent.Duration);

        }

        #endregion

        #region " PUBLIC METHOD "

        public Conference GetConference()
        {
            ReadFile();
            return _conference;
        }

        #endregion

    }
}
