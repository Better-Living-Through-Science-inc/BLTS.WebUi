﻿using BLTS.WebUi.Models;
using BLTS.WebUi.Utilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Caching;

namespace BLTS.WebUi.Configurations
{
    /// <summary>
    /// provides easy access to set and retrieve application level variables 
    /// </summary>
    public class ConfigurationManager
    {
        private IApiRepository<OperationalConfiguration, long> _repositoryOperationalConfiguration;
        private readonly IConfiguration _configuration;

        public ConfigurationManager(//IApiRepository<OperationalConfiguration, long> repositoryOperationalConfiguration
                                    //, 
                                    IConfiguration configuration)
        {
            //_repositoryOperationalConfiguration = repositoryOperationalConfiguration;
            _configuration = configuration;
            
            // preload the config data on startup
            VerifyConfigDataLoaded();
        }

        /// <summary>
        /// cache access method
        /// </summary>
        /// <returns></returns>
        private ConcurrentDictionary<string, dynamic> VerifyConfigDataLoaded()
        {
            MemoryCache memoryCache = MemoryCache.Default;

            return memoryCache.AddOrGetExistingCacheEntry(nameof(ConfigurationManager) + nameof(VerifyConfigDataLoaded),
                                                           GenerateCurrentConfigSettings,
                                                           new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddHours(1) });
        }

        /// <summary>
        /// Load config from DB replacementknownPropertiesDictionary knownProperties
        /// </summary>
        /// <returns></returns>
        private ConcurrentDictionary<string, dynamic> GenerateCurrentConfigSettings()
        {
            Stopwatch methodTimer = new Stopwatch();
            methodTimer.Start();

            ConcurrentDictionary<string, dynamic> applicationVariableDictionary = new ConcurrentDictionary<string, dynamic>();


            #region begin extract of primary app vars from config files
            _configuration.AsEnumerable().AsParallel()
                          .Where(singleConfigurationValue => singleConfigurationValue.Key.Contains("ConnectionStrings", StringComparison.InvariantCultureIgnoreCase) == false
                                                          && !string.IsNullOrWhiteSpace(singleConfigurationValue.Value))
                          .ForAll(singleConfigurationValue => applicationVariableDictionary.TryAdd(singleConfigurationValue.Key.Replace("Values:", "").Replace("App:", ""), singleConfigurationValue.Value));
            #endregion

            #region begin extract of connection strings in the config data
            applicationVariableDictionary.TryAdd("ConnectionStrings", new ConcurrentDictionary<string, string>());
            _configuration.AsEnumerable().AsParallel()
                          .Where(singleConfigurationValue => singleConfigurationValue.Key.Contains("ConnectionStrings:", StringComparison.InvariantCultureIgnoreCase) == true
                                                          && !string.IsNullOrWhiteSpace(singleConfigurationValue.Value))
                          .ForAll(singleConfigurationValue => applicationVariableDictionary["ConnectionStrings"].TryAdd(singleConfigurationValue.Key.Replace("ConnectionStrings:", ""), singleConfigurationValue.Value));
            #endregion

            #region assign config data from application and environment data
            applicationVariableDictionary.TryAdd("ProcessorCount", Environment.ProcessorCount);
            applicationVariableDictionary.TryAdd("EnvironmentName", Environment.MachineName);
            applicationVariableDictionary.TryAdd("EnvironmentType", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

            if (!applicationVariableDictionary.ContainsKey("ApplicationName"))
            {
                string applicationName = Environment.GetEnvironmentVariable("ApplicationName");

                //if (string.IsNullOrWhiteSpace(applicationName))
                //    applicationName = _configuration.GetValue<string>("Values:ApplicationName");

                if (string.IsNullOrWhiteSpace(applicationName))
                    applicationName = Process.GetCurrentProcess().ProcessName;

                if (string.IsNullOrWhiteSpace(applicationName))
                    applicationName = "ApplicationNameUnknown";

                applicationVariableDictionary.TryAdd("ApplicationName", applicationName);
            }
            applicationVariableDictionary.AddOrUpdate("ApplicationNameInErrorLog", applicationVariableDictionary["ApplicationName"], (Func<string, dynamic, dynamic>)((key, existingValue) => applicationVariableDictionary["ApplicationName"]));
            #endregion

            return applicationVariableDictionary;
        }

        /// <summary>
        /// returns the full config dictionary
        /// </summary>
        /// <returns></returns>
        public ConcurrentDictionary<string, dynamic> GetAll()
        {
            return VerifyConfigDataLoaded();
        }

        /// <summary>
        /// returns the requested setting value or null
        /// </summary>
        /// <param name="requestedConfigurationName"></param>
        /// <returns></returns>
        public dynamic GetValue(string requestedConfigurationName)
        {
            dynamic currentReturnObject = null;

            if (VerifyConfigDataLoaded().ContainsKey(requestedConfigurationName))
                currentReturnObject = VerifyConfigDataLoaded()[requestedConfigurationName];

            return currentReturnObject;
        }

        /// <summary>
        /// used to update existing values if they need to change during runtime via  value
        /// </summary>
        /// <param name="requestedConfigurationName"></param>
        /// <param name="newAssignmentValue"></param>
        public void SetValue(string requestedConfigurationName, dynamic newAssignmentValue, bool isUpdateDatabase = false)
        {
            VerifyConfigDataLoaded().AddOrUpdate(requestedConfigurationName, newAssignmentValue, (Func<string, dynamic, dynamic>)((key, existingValue) => newAssignmentValue));
        }

    }
}
