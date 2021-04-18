using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BLTS.WebUi.Logs
{
    public interface IApplicationLogTools
    {
        void LogError(Exception applicationError, Dictionary<string, dynamic> actionDictionary = null, [CallerMemberName] string callerMemberName = null);
        void LogError(string logText, Dictionary<string, dynamic> actionDictionary = null, [CallerMemberName] string callerMemberName = null);
        void LogInformation(string logText, Dictionary<string, dynamic> actionDictionary = null, [CallerMemberName] string callerMemberName = null);
        void LogWarning(string logText, Dictionary<string, dynamic> actionDictionary = null, [CallerMemberName] string callerMemberName = null);
    }
}