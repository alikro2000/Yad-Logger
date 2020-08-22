using System;
using System.Collections.Generic;
using System.Text;

namespace YadLogger.Models
{
    /// <summary>
    /// Data Model for Files containing logs.
    /// </summary>
    class LogFileModel
    {
        /// <summary>
        /// Path to the logs file.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Initial date the log file was created on.
        /// </summary>
        public DateTime? CreationDate { get; private set; }

        /// <summary>
        /// Contains information on latest changes of the logfile.
        /// </summary>
        public Dictionary<KeyValuePair<string, string>, DateTime> Updates { get; private set; }

        /// <summary>
        /// Creates a new logfile model. You can save it by calling LogFileController.Write() method.
        /// </summary>
        /// <param name="path">The directory of the logfile.</param>
        /// <param name="creationDate">The date on which the file was created. If passed as null, then the time is set to DateTime.Now</param>
        public LogFileModel(string path, DateTime? creationDate = null)
        {
            Path = path;
            CreationDate = creationDate != null ? creationDate.Value : DateTime.Now;

            Updates = new Dictionary<KeyValuePair<string, string>, DateTime>();
            AddUpdate("Create", $"Created file initially at {path}", CreationDate);
        }

        /// <summary>
        /// Adds an update to the log file. You need to save the changes to the file later.
        /// </summary>
        /// <param name="title">Update title</param>
        /// <param name="info">Update info</param>
        /// <param name="time">Update time. Is set to DateTime.Now if passed as null.</param>
        public void AddUpdate(string title, string info, DateTime? time = null)
        {
            time = time != null ? time.Value : DateTime.Now;
            Updates.Add(new KeyValuePair<string, string>(title, info), time.Value);
        }
    }
}
