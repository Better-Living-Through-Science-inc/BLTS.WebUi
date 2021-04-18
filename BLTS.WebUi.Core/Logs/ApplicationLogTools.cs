using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BLTS.WebUi.Logs
{
    /// <summary>
    /// provides easy logging to multiple sources for the system
    /// </summary>
    public class ApplicationLogTools : IApplicationLogTools
    {

        public ApplicationLogTools()
        {
        }

        /// <summary>
        /// logs exceptions
        /// </summary>
        /// <param name="applicationError"></param>
        public void LogError(Exception applicationError, Dictionary<string, dynamic> actionDictionary = null, [CallerMemberName] string callerMemberName = null)
        {
            string logText = applicationError.ToString();
            LogError(logText, actionDictionary, callerMemberName);
        }

        /// <summary>
        /// logs text message as error
        /// </summary>
        /// <param name="logText"></param>
        public void LogError(string logText, Dictionary<string, dynamic> actionDictionary = null, [CallerMemberName] string callerMemberName = null)
        {
            EventLogEntryType currentLogType = EventLogEntryType.Error;
        }

        /// <summary>
        /// logs text message as warning
        /// </summary>
        /// <param name="logText"></param>
        public void LogWarning(string logText, Dictionary<string, dynamic> actionDictionary = null, [CallerMemberName] string callerMemberName = null)
        {
            EventLogEntryType currentLogType = EventLogEntryType.Warning;
        }

        /// <summary>
        /// logs text message as information
        /// </summary>
        /// <param name="logText"></param>
        public void LogInformation(string logText, Dictionary<string, dynamic> actionDictionary = null, [CallerMemberName] string callerMemberName = null)
        {
            EventLogEntryType currentLogType = EventLogEntryType.Information;
        }

    }
}
