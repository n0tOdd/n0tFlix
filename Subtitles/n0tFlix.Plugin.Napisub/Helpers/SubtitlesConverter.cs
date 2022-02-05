using n0tFlix.Plugin.NapiSub.SubtitlesParser.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using n0tFlix.Plugin.NapiSub.SubtitlesParser;

namespace n0tFlix.Plugin.NapiSub.Helpers
{
    public class SubtitlesConverter
    {
        public static Stream ConvertToSubRipStream(Stream stream)
        {
            if (stream == null) return null;

            var subtitlesLines = new SubParser().ParseStream(stream, Encoding.UTF8);

            return GetSubRipStream(subtitlesLines);
        }

        private static Stream GetSubRipStream(IReadOnlyCollection<SubtitleItem> items)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new StreamWriter(memoryStream, Encoding.UTF8))
                {
                    if (items.Any())
                    {
                        var i = 1;
                        foreach (var item in items)
                        {
                            writer.WriteLine(i++.ToString());   //add line counter
                            writer.WriteLine(GetSubRipTimeLine(item.StartTime, item.EndTime)); //add time line 

                            foreach (var line in item.Lines)
                            {
                                writer.WriteLine(line); //subtitles line
                            }

                            writer.WriteLine(string.Empty); //add empty line
                        }
                    }

                    writer.Flush();
                }

                return new MemoryStream(memoryStream.ToArray(), false);
            }
        }

        private static string GetSubRipTimeLine(int startTime, int endTime)
        {
            var startTs = new TimeSpan(0, 0, 0, 0, startTime);
            var endTs = new TimeSpan(0, 0, 0, 0, endTime);

            var start = $"{startTs:hh\\:mm\\:ss\\,fff}";
            var end = $"{endTs:hh\\:mm\\:ss\\,fff}";

            return $"{start} --> {end}";
        }
    }
}