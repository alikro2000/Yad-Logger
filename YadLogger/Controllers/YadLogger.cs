using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YadLogger.Controllers;
using YadLogger.Models;

namespace YadLogger
{
    public class YadLogger
    {
        private static List<LogStream> allStreams;

        public static void Init()
        {
            allStreams = new List<LogStream>();
            allStreams.Add(new LogStream("DefaultStream", "C:\\YadLogger\\default.log"));
        }

        public static void CreateLog(string name, string path)
        {
            allStreams.Add(new LogStream(name, path));
        }

        public static void Log(string text, DateTime? time)
        {
            var defStream = allStreams.Single(s => s.Name.Equals("DefaultStream"));
            defStream?.AddLog(text);
        }

        public static void LogTo(string streamName, string text)
        {
            var stream = allStreams.Single(s => s.Name.Equals(streamName));
            stream?.AddLog(text);
        }
    }
}
