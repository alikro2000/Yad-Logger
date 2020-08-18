using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using YadLogger.Models;

namespace YadLogger.Controllers
{
    /// <summary>
    /// Controls data flow over log files. (Read, Write, Move, Delete, Copy)
    /// </summary>
    class LogFileController
    {
        /// <summary>
        /// Read a logfile.
        /// </summary>
        /// <param name="logFile"></param>
        /// <param name="logs">output the logs.</param>
        public static void Read(LogFileModel logFile, out List<LogDataModel> logs)
        {
            if (!File.Exists(logFile.Path))
            {
                throw new FileNotFoundException("Can't find the log file at " + logFile.Path);
            }

            logs = new List<LogDataModel>();

            try
            {
                using (StreamReader reader = new StreamReader(logFile.Path))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string from = line.Substring(1, line.IndexOf(")") - 1);
                        string time = line.Substring(line.IndexOf("[") + 1, line.IndexOf("]") - line.IndexOf("[") - 1);
                        string text = line.Substring(line.IndexOf(":") + 1);

                        logs.Add(new LogDataModel(text, from, DateTime.Parse(time)));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured: " + e.Message);
            }

        }

        /// <summary>
        /// Write logs to a log file. Creates a new file if it doesn't exists at logFile.Path.
        /// </summary>
        /// <param name="logFile"></param>
        /// <param name="logs"></param>
        /// <param name="append">Appends to the file, if it exists.</param>
        public static void Write(LogFileModel logFile, List<LogDataModel> logs, bool append = true)
        {
            try
            {
                using (var writer = append ? File.AppendText(logFile.Path) : File.CreateText(logFile.Path))
                {
                    foreach (var l in logs)
                    {
                        writer.WriteLine(l.ToString(true, true));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured: " + e.Message);
            }
        }
    }
}
