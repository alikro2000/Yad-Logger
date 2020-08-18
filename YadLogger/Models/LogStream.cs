using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YadLogger.Models
{
    class LogStream
    {
        private static List<int> usedIDs = new List<int>();

        public string Id { get; private set; }
        public string Name { get; private set; }

        public readonly LogFileModel logFile;

        public readonly List<LogDataModel> logs;

        public LogStream(string name, string path)
        {
            Name = name;
            Id = $"#{GetNewRandomId()}";
            logFile = new LogFileModel(path);
            logs = new List<LogDataModel>();
        }

        public void AddLog(string text, DateTime? time = null)
        {
            logs.Add(new LogDataModel(text, Name, time));
        }

        private static int GetNewRandomId()
        {
            int result;
            do
            {
                result = new Random().Next(100001, 999999);
            } while (usedIDs.Contains(result));
            usedIDs.Add(result);
            return result;
        }
    }
}
