using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YadLogger.Models
{
    /// <summary>
    /// Contains information about the LogFileModel and its data.
    /// </summary>
    class LogStream
    {
        /// <summary>
        /// Keeps track of IDs of all streams currently running.
        /// </summary>
        private static List<int> usedIDs = new List<int>();

        /// <summary>
        /// Stream ID.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Stream name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Stream LogFile. Data are saved to it.
        /// </summary>
        public readonly LogFileModel logFile;

        /// <summary>
        /// logs to write to the LogFile.
        /// </summary>
        public readonly List<LogDataModel> logs;

        /// <summary>
        /// Creates a new log.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path">Will be set as the path of the corresponding logfile.</param>
        public LogStream(string name, string path)
        {
            Name = name;
            Id = $"#{GetNewRandomId()}";
            logFile = new LogFileModel(path);
            logs = new List<LogDataModel>();
        }

        /// <summary>
        /// Adds a log to the stream.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="time"></param>
        /// <remarks>Does not save it to the file.</remarks>
        public void AddLog(string text, DateTime? time = null)
        {
            logs.Add(new LogDataModel(text, Name, time));
        }

        /// <summary>
        /// Generated a new random id for stream ID.
        /// </summary>
        /// <returns></returns>
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
