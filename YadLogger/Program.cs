using System;
using System.Collections.Generic;
using YadLogger.Controllers;
using YadLogger.Models;

namespace YadLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            var logs = new List<LogDataModel>();
            //{
            //new LogDataModel ("Hello world!", "Main thread"),
            //new LogDataModel ("line 2", "Main thread"),
            //new LogDataModel ("line :3", "Main thread"),
            //new LogDataModel ("line for", "Main thread")
            //};
            var logfile = new LogFileModel("D:\\my-projects\\YadLogger\\YadLogger\\Logs\\file.log");
            var logfile2 = new LogFileModel("D:\\my-projects\\YadLogger\\YadLogger\\Logs\\file_copy.log");

            //LogFileController.Write(logfile, logs);
            //LogFileController.Read(logfile, out logs);
            //logs.ForEach(l => Console.WriteLine(l.ToString(true, true)));

            //LogFileController.Copy(logfile, "D:\\my-projects\\YadLogger\\YadLogger\\Logs\\file_copy.log");

            //LogFileController.Delete(logfile);

            logfile = LogFileController.Move(logfile2, logfile.Path);
        }
    }
}
