using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM
{
    public class OutputProcessor
    {
        private Conference _conference;

        public OutputProcessor(Conference conference)
        {
            this._conference = conference;
        }

        public string Process()
        {
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < _conference.ScheduledTracks.Count; i++)
            {
                var track = _conference.ScheduledTracks[i];

                output.AppendLine("Track " + (i + 1));

                foreach (var @event in track.Events)
                {
                    if (@event is Session)
                    {
                        foreach (var talk in (@event as Session).Talks)
                        {
                            output.Append(TimeHelper.FormatTime(talk.StartTime));
                            output.Append(" ").Append(talk.Title);
                            if (talk.Duration == (int)TimeUnit.Lightning)
                            {
                                output.Append(" ").AppendLine(TimeUnit.Lightning.ToString());
                            }
                            else
                            {
                                output.Append(" ").Append(talk.Duration);
                                output.AppendLine(TimeUnit.Min.ToString());
                            }

                        }
                    }
                    else
                    {
                        var singleEvent = @event as SingleEvent;
                        output.Append(TimeHelper.FormatTime(singleEvent.Info.StartTime));
                        output.Append(" ").AppendLine(singleEvent.Info.Title);
                    }
                }

                output.AppendLine();

            }

            return output.ToString();
        }

        public void SaveInFile(String content, String filePath)
        {
            File.WriteAllText(filePath, content);
        }
    }
}
