using BLTS.WebUi.Configuration;
using BLTS.WebUi.DataAccessLayer;
using BLTS.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BLTS.WebUi.Logs
{
    /// <summary>
    /// provides easy logging to multiple sources for the system
    /// </summary>
    public class ApplicationLogTools
    {
        private IRepository<ApplicationLog, long> _repositoryAuditLog;
        private AutoConfiguration _autoConfiguration;

        public ApplicationLogTools(IRepository<ApplicationLog, long> repositoryAuditLog
                                 , AutoConfiguration autoConfiguration)
        {
            _repositoryAuditLog = repositoryAuditLog;
            _autoConfiguration = autoConfiguration;
        }

        /// <summary>
        /// logs exceptions
        /// </summary>
        /// <param name="applicationError"></param>
        public void LogError(Exception applicationError, Dictionary<string, dynamic> actionDictionary = null)
        {
            string logText = applicationError.ToString();
            LogError(logText, actionDictionary);
        }

        /// <summary>
        /// logs text message as error
        /// </summary>
        /// <param name="logText"></param>
        public void LogError(string logText, Dictionary<string, dynamic> actionDictionary = null)
        {
            EventLogEntryType currentLogType = EventLogEntryType.Error;
            LogToDatabaseApplicationLog(logText, currentLogType, actionDictionary);
        }

        /// <summary>
        /// logs text message as warning
        /// </summary>
        /// <param name="logText"></param>
        public void LogWarning(string logText, Dictionary<string, dynamic> actionDictionary = null)
        {
            EventLogEntryType currentLogType = EventLogEntryType.Warning;
            LogToDatabaseApplicationLog(logText, currentLogType, actionDictionary);
        }

        /// <summary>
        /// logs text message as information
        /// </summary>
        /// <param name="logText"></param>
        public void LogInformation(string logText, Dictionary<string, dynamic> actionDictionary = null)
        {
            EventLogEntryType currentLogType = EventLogEntryType.Information;
            LogToDatabaseApplicationLog(logText, currentLogType, actionDictionary);
        }

        /// <summary>
        /// determin if activation of a type of message is needed
        /// </summary>
        /// <param name="currentLogType"></param>
        /// <param name="activationSetting"></param>
        /// <returns></returns>
        private bool DeterminActivationSetting(EventLogEntryType currentLogType, string activationSetting)
        {
            bool activate = false;
            switch (activationSetting)
            {
                case "Error":
                    if (currentLogType <= EventLogEntryType.Error)
                        activate = true;
                    break;
                case "Warning":
                    if (currentLogType <= EventLogEntryType.Warning)
                        activate = true;
                    break;
                case "Information":
                    if (currentLogType <= EventLogEntryType.Information)
                        activate = true;
                    break;
            }
            return activate;
        }

        /// <summary>
        /// logs data to the database app log
        /// </summary>
        /// <param name="logText"></param>
        /// <param name="currentLogType"></param>
        private void LogToDatabaseApplicationLog(string logText, EventLogEntryType currentLogType, Dictionary<string, dynamic> actionDictionary = null)
        {
            if (DeterminActivationSetting(currentLogType, _autoConfiguration.GetValue("DatabaseAppLogLevel")))
            {
                ApplicationLog currentLogEntry = new ApplicationLog
                {
                    ApplicationName = _autoConfiguration.GetValue("ApplicationName"),
                    EnvironmentName = _autoConfiguration.GetValue("EnvironmentName"),
                    ExecutionTime = DateTime.Now.ToUniversalTime(),
                    ExecutionDuration = -5555
                };

                if (actionDictionary != null)
                {
                    if (actionDictionary.ContainsKey("ApplicationName"))
                        currentLogEntry.ApplicationName = actionDictionary["ApplicationName"];
                    if (actionDictionary.ContainsKey("EnvironmentName"))
                        currentLogEntry.EnvironmentName = actionDictionary["EnvironmentName"];
                    if (actionDictionary.ContainsKey("ExecutionTime"))
                        currentLogEntry.ExecutionTime = actionDictionary["ExecutionTime"];
                    if (actionDictionary.ContainsKey("ExecutionDuration"))
                        currentLogEntry.ExecutionDuration = actionDictionary["ExecutionDuration"];
                    if (actionDictionary.ContainsKey("MethodName"))
                        currentLogEntry.MethodName = actionDictionary["MethodName"];
                    if (actionDictionary.ContainsKey("ServiceName"))
                        currentLogEntry.ServiceName = actionDictionary["ServiceName"];
                }

                switch (currentLogType)
                {
                    case EventLogEntryType.Error:
                        {
                            currentLogEntry.CustomData = _autoConfiguration.GetValue("ApplicationNameInErrorLog") + " - Error Detected";
                            currentLogEntry.Exception = logText.Length <= 2000 ? logText : logText.Substring(0, 2000);

                            break;
                        }
                    case EventLogEntryType.Warning:
                        {
                            currentLogEntry.CustomData = logText.Length <= 2000 ? logText : logText.Substring(0, 2000);

                            break;
                        }
                    case EventLogEntryType.Information:
                        {
                            currentLogEntry.CustomData = logText.Length <= 2000 ? logText : logText.Substring(0, 2000);

                            break;
                        }
                }

                _repositoryAuditLog.Insert(currentLogEntry);
            }

        }

        ///// <summary>
        ///// logs data to the AppCenter
        ///// </summary>
        ///// <param name="logText"></param>
        ///// <param name="currentLogType"></param>
        //private void LogToAppCenterLog(string logText, EventLogEntryType currentLogType, Dictionary<string, dynamic> actionDictionary = null)
        //{
        //    if (DeterminActivationSetting(currentLogType, _autoConfiguration.GetValue("AppCenterAppLogLevel")))
        //    {
        //        Dictionary<string, string> currentLogEntry = new Dictionary<string, string>();
        //        currentLogEntry.Add("ApplicationName", _autoConfiguration.GetValue("ApplicationName"));
        //        currentLogEntry.Add("EnvironmentName", _autoConfiguration.GetValue("EnvironmentName"));
        //        currentLogEntry.Add("ExecutionTime", DateTime.Now.ToUniversalTime().ToString());

        //        if (actionDictionary != null)
        //        {
        //            if (actionDictionary.ContainsKey("ExecutionDuration"))
        //                currentLogEntry.Add("ExecutionDuration", (actionDictionary["ExecutionDuration"]).ToString());
        //            if (actionDictionary.ContainsKey("MethodName"))
        //                currentLogEntry.Add("MethodName", actionDictionary["MethodName"]);
        //            if (actionDictionary.ContainsKey("ServiceName"))
        //                currentLogEntry.Add("ServiceName", actionDictionary["ServiceName"]);
        //        }

        //        switch (currentLogType)
        //        {
        //            case EventLogEntryType.Error:
        //                {
        //                    currentLogEntry.Add("CustomData", _autoConfiguration.GetValue("ApplicationNameInErrorLog") + " - Error Detected");
        //                    currentLogEntry.Add("Exception", logText.Length <= 2000 ? logText : logText.Substring(0, 2000));

        //                    break;
        //                }
        //            case EventLogEntryType.Warning:
        //                {
        //                    currentLogEntry.Add("CustomData", logText.Length <= 2000 ? logText : logText.Substring(0, 2000));

        //                    break;
        //                }
        //            case EventLogEntryType.Information:
        //                {
        //                    currentLogEntry.Add("CustomData", logText.Length <= 2000 ? logText : logText.Substring(0, 2000));

        //                    break;
        //                }

        //        }

        //        Analytics.TrackEvent(_autoConfiguration.GetValue("AppCenterAppLogLevel"), currentLogEntry);
        //    }

        //}
    }
}
