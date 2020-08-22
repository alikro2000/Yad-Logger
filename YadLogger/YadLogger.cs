﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YadLogger.Controllers;
using YadLogger.Models;

namespace YadLogger
{
    /// <summary>
    /// Contains all you need to interact with YadLogger.
    /// </summary>
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
        public static void Init(string defaultStremPath = "C:\\YadLogger\\default.log")
        {
            allStreams = new List<LogStream>();
            runningUpdates = new List<Task>();
            defaultStream = new LogStream("DefaultStream", defaultStremPath);
            allStreams.Add(defaultStream);
        }

        /// <summary>
        /// Creates a new logstream.
        /// </summary>
        /// <param name="name">Stream's name, this will show up as the origin.</param>
        /// <param name="path">The location which the logs will be stored at.</param>
        /// <remarks>LogStreams with the same path are not allowed.</remarks>
        /// <exception>Throws an exception if a LogStream with the same path already exists.</exception>
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
            defaultStream.AddLog(text);
            Task task = new Task(() =>
                           {
                               LogFileController.Write(defaultStream.logFile, defaultStream.logs);
                           });
            runningUpdates.Add(task);
            task.RunSynchronously();
            runningUpdates.Remove(task);
        }

        /// <summary>
        /// Logs to a stream of choice.
        /// </summary>
        /// <param name="streamName"></param>
        /// <param name="text"></param>
        /// <exception>Throws an exception if a LogStream with the specified name is not running.</exception>
        public static void LogTo(string streamName, string text)
        {
            var stream = allStreams.SingleOrDefault(s => s.Name.Equals(streamName));
            if (stream == null)
            {
                throw new Exception($"Cannot find the LogStream with the specified name \'{streamName}\'.");
            }

            stream?.AddLog(text);
            runningUpdates.Add(new Task(() =>
            {
                LogFileController.Write(stream.logFile, stream.logs);
            }));
            runningUpdates[runningUpdates.Count - 1].RunSynchronously();
            runningUpdates.RemoveAt(runningUpdates.Count - 1);
        }

        /// <summary>
        /// Logs to a stream of choice asynchronously.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <remarks>(Still under test)</remarks>
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

        /// <summary>
        /// Sets default stream to another.
        /// </summary>
        /// <param name="streamName"></param>
        /// <remarks>You need to have your stream of choice previously created.</remarks>
        public static void SetDefaultStream(string streamName)
        {
            var stream = allStreams.SingleOrDefault(s => s.Name.Equals(streamName));
            if (stream != null)
            {
                defaultStream = stream;
            }
        }

        /// <summary>
        /// The default stream will be reset to a file located at C:\\YadLogger\\default.log
        /// </summary>
        public static void ResetDefaultStream()
        {
            defaultStream = allStreams.Single(s => s.Name.Equals("DefaultStream"));
        }

        /// <summary>
        /// Deletes and stops tracking a stream.
        /// </summary>
        /// <param name="streamName"></param>
        /// <remarks>The 'DefaultStream' stream cannot be deleted.</remarks>
        public static void DeleteStream(string streamName)
        {
            var stream = allStreams.SingleOrDefault(s => s.Name.Equals(streamName) && !s.Name.Equals("DefaultStream"));
            allStreams.Remove(stream);
        }
    }
}
