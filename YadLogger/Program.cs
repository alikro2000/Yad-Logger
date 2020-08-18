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
            YadLogger.Init();
            try
            {

            }
            catch
            {
                YadLogger.CreateLogStream("LogStream01", "D:\\my-projects\\YadLogger\\YadLogger\\Logs\\log01.log");
                YadLogger.CreateLogStream("LogStream02", "D:\\my-projects\\YadLogger\\YadLogger\\Logs\\log02.log");
                YadLogger.CreateLogStream("LogStream03", "D:\\my-projects\\YadLogger\\YadLogger\\Logs\\log03.log");
            }

            YadLogger.Log("New log! :D");
        }
    }
}
