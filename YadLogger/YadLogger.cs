using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YadLogger.Controllers;
using YadLogger.Models;

namespace YadLogger
{
    public class YadLogger
    {
        /// <summary>
        /// All working streams.
        /// </summary>
        private static List<LogStream> allStreams;

        /// <summary>
        /// All the streams that are currently running (operating on their file).
        /// </summary>
        private static List<Task> runningUpdates;

        /// <summary>
        /// Default stream.
        /// </summary>
        private static LogStream defaultStream;

        /// <summary>
        /// Initializes the logger with a default stream "DefaultStream" at C:\YadLogger\default.log
        /// </summary>
        public static void Init()
        {
            allStreams = new List<LogStream>();
            runningUpdates = new List<Task>();
            defaultStream = new LogStream("DefaultStream", "C:\\YadLogger\\default.log");
            allStreams.Add(defaultStream);
        }

        /// <summary>
        /// Creates a new logstream.
        /// </summary>
        /// <param name="name">Stream's name, this will show up as the origin.</param>
        /// <param name="path">The location which the logs will be stored at.</param>
        public static void CreateLogStream(string name, string path)
        {
            if (allStreams.SingleOrDefault(s => s.Name.Equals(name)) != null)
            {
                throw new Exception($"A stream with the name {name} already exists!");
            }

            if (allStreams.SingleOrDefault(s => s.logFile.Path.Equals(path)) != null)
            {
                throw new Exception($"An active stream to {path} is already running");
            }

            allStreams.Add(new LogStream(name, path));
        }

        /// <summary>
        /// Adds a new log to default logstream.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="time"></param>
        public static void Log(string text)
        {
            //defaultStream.AddLog(text);
            //runningUpdates.Add(new Task(() =>
            //{
            //LogFileController.Write(defaultStream.logFile, defaultStream.logs);
            //}));
            //runningUpdates[runningUpdates.Count - 1].RunSynchronously();
            //runningUpdates.RemoveAt(runningUpdates.Count - 1);

            Action newLog = () =>
            {
                LogFileController.Write(defaultStream.logFile, defaultStream.logs);
            };
            var task = new Task(newLog);
            runningUpdates.Add(task);
            newLog.Invoke();
            runningUpdates.Remove(task);
        }

        /// <summary>
        /// Logs to a stream of choice.
        /// </summary>
        /// <param name="streamName"></param>
        /// <param name="text"></param>
        public static void LogTo(string streamName, string text)
        {
            var stream = allStreams.SingleOrDefault(s => s.Name.Equals(streamName));
            stream?.AddLog(text);
            runningUpdates.Add(new Task(() =>
            {
                LogFileController.Write(stream.logFile, stream.logs);
            }));
            runningUpdates[runningUpdates.Count - 1].RunSynchronously();
            runningUpdates.RemoveAt(runningUpdates.Count - 1);
        }

        public static async Task LogAsync(string text)
        {
            Action newLog = () =>
            {
                LogFileController.Write(defaultStream.logFile, defaultStream.logs);
            };
            var task = new Task(newLog);
            runningUpdates.Add(task);
            await Task.Run(newLog);
            runningUpdates.Remove(task);
        }
    }
}
