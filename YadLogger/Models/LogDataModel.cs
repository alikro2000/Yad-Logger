using System;
using System.Collections.Generic;
using System.Text;

namespace YadLogger.Models
{
    /// <summary>
    /// Data Model for each log.
    /// </summary>
    class LogDataModel
    {
        /// <summary>
        /// Log's text data.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Time of logging.
        /// </summary>
        public DateTime? Time { get; private set; }

        /// <summary>
        /// where the log came from.
        /// </summary>
        public string From { get; private set; }

        /// <summary>
        /// Create new LogData.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="from">Where the log comes from.</param>
        /// <param name="time">Log time. If passed as null, then the time is set to DateTime.Now</param>
        public LogDataModel(string text, string from = "", DateTime? time = null)
        {
            Text = text;
            Time = time != null ? time.Value : DateTime.Now;
            From = from;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getFrom">outputs as (From)</param>
        /// <param name="getTime">outputs as [Time]: </param>
        /// <returns></returns>
        public string ToString(bool getFrom = false, bool getTime = false)
        {
            return $"{(getFrom ? $"({From})" : string.Empty)}{(getTime ? $"[{Time}]: " : string.Empty)}{Text}";
        }
    }
}
