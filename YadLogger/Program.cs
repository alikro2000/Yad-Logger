using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YadLogger.Controllers;
using YadLogger.Models;

namespace YadLogger
{
    class Program
    {
        static async Task Main(string[] args)
        {
            YadLogger.Init();
            //YadLogger.Init("D:\\my-projects\\YadLogger\\YadLogger\\Logs\\file.log");
            try
            {
                YadLogger.CreateLogStream("LogStream01", "D:\\my-projects\\YadLogger\\YadLogger\\Logs\\log01.log");
                YadLogger.CreateLogStream("LogStream02", "D:\\my-projects\\YadLogger\\YadLogger\\Logs\\log02.log");
                YadLogger.CreateLogStream("LogStream03", "D:\\my-projects\\YadLogger\\YadLogger\\Logs\\log03.log");
            }
            catch
            {
            }

            YadLogger.SetDefaultStream("LogStream02");
            YadLogger.ResetDefaultStream();
            YadLogger.DeleteStream("LogStream03");
            try
            {
                YadLogger.LogTo("LogStream03", "yo");
            }
            catch { }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            YadLogger.Log("New log! :D");
            YadLogger.Log("New log! :D");
            YadLogger.Log("New log! :D");
            YadLogger.Log("New log! :D");
            YadLogger.Log("New log! :D");

            watch.Stop();

            YadLogger.LogTo("LogStream01", "sync time: " + watch.ElapsedMilliseconds);

            watch.Restart();

            await YadLogger.LogAsync("New log! :D");
            await YadLogger.LogAsync("New log! :D");
            await YadLogger.LogAsync("New log! :D");
            await YadLogger.LogAsync("New log! :D");
            await YadLogger.LogAsync("New log! :D");

            watch.Stop();

            YadLogger.LogTo("LogStream01", "async time: " + watch.ElapsedMilliseconds);

            watch.Restart();

            await YadLogger.LogAsync("New log! :D");
            await YadLogger.LogAsync("New log! :D");
            await YadLogger.LogAsync("New log! :D");
            await YadLogger.LogAsync("New log! :D");
            await YadLogger.LogAsync("New log! :D");

            watch.Stop();

            YadLogger.LogTo("LogStream01", "async time: " + watch.ElapsedMilliseconds);

            watch.Restart();

            await YadLogger.LogAsync("New log! :D");
            await YadLogger.LogAsync("New log! :D");
            await YadLogger.LogAsync("New log! :D");
            await YadLogger.LogAsync("New log! :D");
            await YadLogger.LogAsync("New log! :D");

            watch.Stop();

            YadLogger.LogTo("LogStream01", "async time: " + watch.ElapsedMilliseconds);
        }
    }
}
