using BLTS.WebUi.DataAccessLayer;
using BLTS.WebUi.Models;
using BLTS.WebUi.Utilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Caching;

namespace BLTS.WebUi.Configuration
{
    /// <summary>
    /// provides easy access to set and retrieve application level variables 
    /// </summary>
    public class AutoConfiguration
    {
        private IRepository<OperationalConfiguration, long> _repositoryOperationalConfiguration;
        private readonly IConfiguration _configuration;

        public AutoConfiguration(IRepository<OperationalConfiguration, long> repositoryOperationalConfiguration
                               , IConfiguration configuration)
        {
            _repositoryOperationalConfiguration = repositoryOperationalConfiguration;
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

            return memoryCache.AddOrGetExistingCacheEntry(nameof(AutoConfiguration) + nameof(VerifyConfigDataLoaded),
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

            #region load config data from DB storage

            ConcurrentBag<OperationalConfiguration> configurationColletion = new ConcurrentBag<OperationalConfiguration>(_repositoryOperationalConfiguration.GetAll());

            configurationColletion.AsParallel()
                                  .Where(singleConfigurationValue => singleConfigurationValue.IsConnectionString == false)
                                  .ForAll(singleConfigurationValue => applicationVariableDictionary.TryAdd(singleConfigurationValue.PropertyName,
                                                                                            (singleConfigurationValue.IntegerValue.HasValue) ? singleConfigurationValue.IntegerValue :
                                                                                            (singleConfigurationValue.StringValue != null) ? singleConfigurationValue.StringValue :
                                                                                            (singleConfigurationValue.BoolValue.HasValue) ? singleConfigurationValue.BoolValue :
                                                                                            (singleConfigurationValue.DateValue.HasValue) ? singleConfigurationValue.DateValue :
                                                                                            (singleConfigurationValue.LongValue.HasValue) ? singleConfigurationValue.LongValue :
                                                                                            (singleConfigurationValue.DecimalValue.HasValue) ? singleConfigurationValue.DecimalValue :
                                                                                            (singleConfigurationValue.GuidValue.HasValue) ? singleConfigurationValue.GuidValue : (dynamic)"No Value Assigned"
                                                                                            ));

            configurationColletion.Where(singleConfigurationValue => singleConfigurationValue.IsConnectionString == true)
                                  .AsParallel()
                                  .ForAll(singleConfigurationValue => applicationVariableDictionary["ConnectionStrings"].TryAdd(singleConfigurationValue.PropertyName, singleConfigurationValue.StringValue));

            configurationColletion.Clear();
            configurationColletion = null;
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
        /// returns the requested setting value or null
        /// </summary>
        /// <param name="requestedConfigurationName"></param>
        /// <returns></returns>
        public string GetConnectionString(string requestedConnectionStringName)
        {
            string currentReturnObject = null;
            if (VerifyConfigDataLoaded()["ConnectionStrings"].ContainsKey(requestedConnectionStringName))
                currentReturnObject = (string)VerifyConfigDataLoaded()["ConnectionStrings"][requestedConnectionStringName];

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

            if (isUpdateDatabase == true)
            {

                OperationalConfiguration currentWorkingObject = _repositoryOperationalConfiguration.GetAll().Where(singleConfig => singleConfig.PropertyName == requestedConfigurationName).FirstOrDefault();

                if (currentWorkingObject == null)
                {
                    currentWorkingObject = new OperationalConfiguration();
                    currentWorkingObject.PropertyName = requestedConfigurationName;
                    currentWorkingObject.Description = requestedConfigurationName;
                    currentWorkingObject.IsEnabled = true;
                    currentWorkingObject.IsConnectionString = false;
                    //_repositoryOperationalConfiguration.InsertAndGetId(currentWorkingObject);
                    _repositoryOperationalConfiguration.Insert(currentWorkingObject);
                }

                AssignValueToCorrectProperty(newAssignmentValue, currentWorkingObject);

                _repositoryOperationalConfiguration.Update(currentWorkingObject);
            }
        }

        /// <summary>
        /// used to update existing values if they need to change during runtime via funct call (value returned)
        /// </summary>
        /// <param name="requestedConfigurationName"></param>
        /// <param name="valueFactory"></param>
        public dynamic SetValue<T>(string requestedConfigurationName, Func<T> valueFactory, bool isUpdateDatabase = false)
        {
            VerifyConfigDataLoaded().AddOrUpdate(requestedConfigurationName, valueFactory.Invoke(), (Func<string, dynamic, dynamic>)((key, existingValue) => valueFactory.Invoke()));

            if (isUpdateDatabase == true)
            {
                OperationalConfiguration currentWorkingObject = _repositoryOperationalConfiguration.GetAll().Where(singleConfig => singleConfig.PropertyName == requestedConfigurationName).FirstOrDefault();

                if (currentWorkingObject == null)
                {
                    currentWorkingObject = new OperationalConfiguration();
                    currentWorkingObject.PropertyName = requestedConfigurationName;
                    currentWorkingObject.Description = requestedConfigurationName;
                    currentWorkingObject.IsEnabled = true;
                    currentWorkingObject.IsConnectionString = false;
                    //_repositoryOperationalConfiguration.InsertAndGetId(currentWorkingObject);
                    _repositoryOperationalConfiguration.Insert(currentWorkingObject);
                }

                AssignValueToCorrectProperty(GetValue(requestedConfigurationName), currentWorkingObject);

                _repositoryOperationalConfiguration.Update(currentWorkingObject);

            }

            return VerifyConfigDataLoaded()[requestedConfigurationName];
        }

        /// <summary>
        /// used to insert new values if they do not yet exist via funct call (existing or new value returned)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestedConfigurationName"></param>
        /// <param name="valueFactory"></param>
        /// <param name="isUpdateDatabase"></param>
        /// <returns></returns>
        public dynamic SetOrGetValue<T>(string requestedConfigurationName, Func<T> valueFactory, bool isUpdateDatabase = false)
        {
            if (VerifyConfigDataLoaded().ContainsKey(requestedConfigurationName))
                return VerifyConfigDataLoaded()[requestedConfigurationName];
            else
                return SetValue<T>(requestedConfigurationName, valueFactory, isUpdateDatabase);
        }

        /// <summary>
        /// used to update existing values if they need to change during runtime
        /// </summary>
        /// <param name="requestedConfigurationName"></param>
        /// <param name="newAssignmentValue"></param>
        public void SetConnectionString(string requestedConfigurationName, string newAssignmentValue, bool isUpdateDatabase = false)
        {
            VerifyConfigDataLoaded()["ConnectionStrings"].AddOrUpdate(requestedConfigurationName, newAssignmentValue, (Func<string, dynamic, dynamic>)((key, existingValue) => newAssignmentValue));
            if (isUpdateDatabase == true)
            {
                OperationalConfiguration currentWorkingObject = _repositoryOperationalConfiguration.GetAll().Where(singleConfig => singleConfig.PropertyName == requestedConfigurationName).FirstOrDefault();

                if (currentWorkingObject == null)
                {
                    currentWorkingObject = new OperationalConfiguration();
                    currentWorkingObject.PropertyName = requestedConfigurationName;
                    currentWorkingObject.Description = requestedConfigurationName;
                    currentWorkingObject.IsEnabled = true;
                    currentWorkingObject.IsConnectionString = true;
                    //_repositoryOperationalConfiguration.InsertAndGetId(currentWorkingObject);
                    _repositoryOperationalConfiguration.Insert(currentWorkingObject);
                }
                currentWorkingObject.StringValue = newAssignmentValue;

                _repositoryOperationalConfiguration.Update(currentWorkingObject);
            }
        }

        /// <summary>
        /// deletes the requested setting value
        /// </summary>
        /// <param name="requestedConfigurationName"></param>
        /// <returns></returns>
        public void DeleteValue(string requestedConfigurationName, bool isUpdateDatabase = false)
        {
            if (VerifyConfigDataLoaded().ContainsKey(requestedConfigurationName))
                VerifyConfigDataLoaded().TryRemove(requestedConfigurationName, out _);

            if (isUpdateDatabase == true)
            {
                OperationalConfiguration currentWorkingObject = _repositoryOperationalConfiguration.GetAll().Where(singleConfig => singleConfig.PropertyName == requestedConfigurationName).FirstOrDefault();
                if (currentWorkingObject != null)
                {
                    _repositoryOperationalConfiguration.Delete(currentWorkingObject);
                }

            }
        }

        /// <summary>
        /// assign the value to the correct property based on its data type
        /// </summary>
        /// <param name="newAssignmentValue"></param>
        /// <param name="currentWorkingObject"></param>
        private void AssignValueToCorrectProperty(dynamic newAssignmentValue, OperationalConfiguration currentWorkingObject)
        {
            currentWorkingObject.BoolValue = null;
            currentWorkingObject.DateValue = null;
            currentWorkingObject.DecimalValue = null;
            currentWorkingObject.GuidValue = null;
            currentWorkingObject.IntegerValue = null;
            currentWorkingObject.LongValue = null;
            currentWorkingObject.StringValue = null;


            switch (Type.GetTypeCode(newAssignmentValue.GetType()))
            {
                case TypeCode.Boolean:
                    {
                        currentWorkingObject.BoolValue = Convert.ChangeType(newAssignmentValue, typeof(bool));
                        break;
                    }

                case TypeCode.DateTime:
                    {
                        currentWorkingObject.DateValue = Convert.ChangeType(newAssignmentValue, typeof(DateTime));
                        break;
                    }

                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    {
                        currentWorkingObject.DecimalValue = Convert.ChangeType(newAssignmentValue, typeof(decimal));
                        break;
                    }

                case TypeCode.Object:
                    {
                        if (newAssignmentValue.GetType() == typeof(Guid))
                            currentWorkingObject.GuidValue = Convert.ChangeType(newAssignmentValue, typeof(Guid));
                        break;
                    }

                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                    {
                        currentWorkingObject.IntegerValue = Convert.ChangeType(newAssignmentValue, typeof(int));
                        break;
                    }

                case TypeCode.Int64:
                case TypeCode.UInt64:
                    {
                        currentWorkingObject.LongValue = Convert.ChangeType(newAssignmentValue, typeof(long));
                        break;
                    }


                case TypeCode.String:
                case TypeCode.Char:
                default:
                    {
                        currentWorkingObject.StringValue = Convert.ChangeType(newAssignmentValue, typeof(string), new CultureInfo("en-US"));
                        break;
                    }
            }
        }
    }
}
