using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace BLTS.WebUi.Utilities
{
    public class ReflectionTools
    {
        public ReflectionTools()
        { }

        /// <summary>
        /// converts object of type object into object of type T using CultureInfo("en-US")
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectValue"></param>
        /// <returns></returns>
        public T ConvertObjectToType<T>(dynamic objectValue)
        {
            object returnObject = ConvertObjectToType(objectValue, typeof(T), new CultureInfo("en-US"));
            if (returnObject != null)
                return (T)returnObject;
            else
                return default(T);
        }

        /// <summary>
        /// converts an object of type object to the specified type
        /// </summary>
        /// <param name="objectValue"></param>
        /// <param name="conversionObjectType"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public dynamic ConvertObjectToType(dynamic objectValue, Type conversionObjectType, CultureInfo culture)
        {
            switch (Type.GetTypeCode(conversionObjectType))
            {
                #region Numerics
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    {
                        //lets try clean it up just incase, to prevent a numeric conversion failure due to extra non numeric data
                        string numericTrimmedString = string.Join("", objectValue.ToString().Split(' ', '$', '\\', '/', '%', '#', '(', ')', '-'));
                        if (!string.IsNullOrWhiteSpace(numericTrimmedString))
                            objectValue = Convert.ChangeType(numericTrimmedString, conversionObjectType, culture);
                        else
                            objectValue = null;
                        break;
                    }
                #endregion

                #region Date objects
                case TypeCode.DateTime:
                    {
                        if (!string.IsNullOrWhiteSpace(objectValue.ToString()))
                        {
                            DateTime dateAttempt;
                            double excelDateFormat;
                            if (objectValue is DateTime)
                            {
                                //no change needed
                            }
                            else if (DateTime.TryParse(objectValue.ToString(), out dateAttempt))
                            {
                                objectValue = dateAttempt;
                            }
                            else if (Double.TryParse(objectValue.ToString(), out excelDateFormat))
                            {
                                objectValue = DateTime.FromOADate(excelDateFormat);
                            }
                            else
                            {
                                DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
                                objectValue = Convert.ToDateTime(objectValue.ToString(), usDtfi);
                            }
                        }
                        else
                            objectValue = null;
                        break;
                    }
                #endregion

                #region  string, bool and default
                case TypeCode.Boolean:
                    {
                        objectValue = objectValue.ToString();
                        break;
                    }
                case TypeCode.String:
                case TypeCode.Char:
                default:
                    {
                        objectValue = Convert.ChangeType(objectValue, conversionObjectType, culture);
                        break;
                    }
                    #endregion
            }
            return objectValue;
        }

        public ConcurrentBag<T> ConvertBagToType<T>(T sampleForTValue, ConcurrentBag<dynamic> collectionToConvert)
        {
            return new ConcurrentBag<T>(collectionToConvert.Cast<T>());
        }

        /// <summary>
        /// Deserialize, decompress and convert from byte array
        /// </summary>
        /// <param name="byteArrayToConvert"></param>
        /// <returns></returns>
        public dynamic ByteArrayToObject(byte[] byteArrayToConvert)
        {
            dynamic returnObject;

            using (MemoryStream dataOutputStream = new MemoryStream())
            using (DeflateStream compressor = new DeflateStream(dataOutputStream, CompressionMode.Decompress))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                dataOutputStream.Write(byteArrayToConvert, 0, byteArrayToConvert.Length);
                dataOutputStream.Seek(0, SeekOrigin.Begin);

                returnObject = formatter.Deserialize(compressor);
            }

            return returnObject;
        }

        /// <summary>
        /// Serialize, compress and convert to byte array
        /// </summary>
        /// <param name="objectToConvert"></param>
        /// <returns></returns>
        public byte[] ObjectToByteArray(dynamic objectToConvert)
        {
            byte[] returnByteArray;

            using (MemoryStream compressedDataOutputStream = new MemoryStream())
            using (DeflateStream compressor = new DeflateStream(compressedDataOutputStream, CompressionMode.Compress))
            using (MemoryStream serializationStream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(serializationStream, objectToConvert);

                byte[] serializedByteArray = serializationStream.ToArray();
                compressor.Write(serializedByteArray, 0, serializedByteArray.Length);
                compressor.Close();

                returnByteArray = compressedDataOutputStream.ToArray();
            }

            return returnByteArray;
        }

        #region Object Factory to instantiate null objects, create copies and other neat things

        /// <summary>
        /// Instantiates an object. Must pass PropertyType.AssemblyQualifiedName for factory to operate
        /// returns instantiated object - optionally if the data type is defined in the DB, this will also create those objects SchemaName.TableName
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public dynamic Create(string typeAssemblyQualifiedName)
        {
            // resolve the type
            Type targetType = ResolveType(typeAssemblyQualifiedName);
            if (targetType == null)
                throw new ArgumentException("Unable to resolve object type: " + typeAssemblyQualifiedName);

            return Create(targetType);
        }

        /// <summary>
        /// creates an object of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public dynamic Create<T>() where T : new()
        {
            return new T();
        }

        /// <summary>
        /// creates object of specified type
        /// </summary>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public dynamic Create(Type targetType)
        {
            return Activator.CreateInstance(targetType);

            // get the default constructor and instantiate
            //Type[] types = new Type[0];
            //ConstructorInfo info = targetType.GetConstructor(types);
            //object targetObject = null; 

            //if (info == null) //must not have found the constructor
            //  if (targetType.BaseType.UnderlyingSystemType.FullName.Contains("Enum"))
            //    targetObject = Activator.CreateInstance(targetType);
            //  else
            //    throw new ArgumentException("Unable to instantiate type: " + targetType.AssemblyQualifiedName + " - Constructor not found");
            //else
            //  targetObject = info.Invoke(null);

            //if (targetObject == null)
            //  throw new ArgumentException("Unable to instantiate type: " + targetType.AssemblyQualifiedName + " - Unknown Error");
            //return targetObject;
        }

        /// <summary>
        /// creates ConcurrentBag of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ConcurrentBag<T> CreateBag<T>()
        {
            return new ConcurrentBag<T>();
        }

        /// <summary>
        /// creates ConcurrentBag of example type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="targetTypeSample"></param>
        /// <returns></returns>
        public ConcurrentBag<T> CreateBag<T>(T targetTypeSample)
        {
            return new ConcurrentBag<T>();
            //Type currentOutputType = typeof(ConcurrentBag<>);
            //Type[] collectionType = { typeof(T) };

            //Type outputBagType = currentOutputType.MakeGenericType(collectionType);

            //return (ConcurrentBag<T>)Activator.CreateInstance(outputBagType);
        }

        /// <summary>
        /// creates ConcurrentDictionary of type, type passed as params
        /// </summary>
        /// <param name="targetTypeKey"></param>
        /// <param name="targetTypeValue"></param>
        /// <returns></returns>
        public dynamic CreateDictionary(Type targetTypeKey, Type targetTypeValue)
        {
            Type currentOutputType = typeof(ConcurrentDictionary<,>);
            Type[] dictionaryType = { targetTypeKey, targetTypeValue };

            Type outputdictionaryType = currentOutputType.MakeGenericType(dictionaryType);

            return Activator.CreateInstance(outputdictionaryType);
        }

        /// <summary>
        /// Loads the assembly of an object. Must pass PropertyType.AssemblyQualifiedName for factory to operate
        /// Returns the object type.
        /// </summary>
        /// <param name="typeString"></param>
        /// <returns></returns>
        public Type ResolveType(string typeAssemblyQualifiedName)
        {
            int commaIndex = typeAssemblyQualifiedName.IndexOf(",");
            string className = typeAssemblyQualifiedName.Substring(0, commaIndex).Trim();
            string assemblyName = typeAssemblyQualifiedName.Substring(commaIndex + 1).Trim();

            if (className.Contains("[]"))
                className = className.Remove(className.IndexOf("[]"), 2);

            return ResolveType(className, assemblyName);
        }

        /// <summary>
        /// Resolves the string name of the object class into a Type
        /// </summary>
        /// <param name="className"></param>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public Type ResolveType(string className, string assemblyName)
        {
            // Get the assembly containing the handler
            Assembly assembly = Assembly.GetExecutingAssembly();
            try
            {
                int commaIndex = assembly.FullName.IndexOf(",");
                string currentAssemblyName = assembly.FullName.Substring(0, commaIndex).Trim();

                if (currentAssemblyName != assemblyName)
                    assembly = Assembly.Load(assemblyName);
            }
            catch
            {
                try
                {
                    assembly = Assembly.LoadWithPartialName(assemblyName);//yes yes this is obsolete but it is only a backup call
                }
                catch (Exception currentException)
                {
                    throw new ArgumentException("Can't load assembly " + assemblyName);
                }
            }

            // Get the handler
            return assembly.GetType(className, false, false);
        }


        /// <summary>
        /// object duplication method
        /// </summary>
        /// <param name="sourceObject"></param>
        public T CreateCopy<T>(T sourceObject)
        {
            T targetObject = (T)Create(sourceObject.GetType());
            return targetObject;
        }

        /// <summary>
        /// execution of method by name
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <param name="classConstructorParameters"></param>
        /// <param name="methodName"></param>
        /// <param name="methodParameters"></param>
        /// <returns></returns>
        public object ExecuteMethod(string assemblyName, string className, object[] classConstructorParameters, string methodName, object[] methodParameters)
        {
            try
            {
                Type currentClassType = ResolveType(className, assemblyName);
                dynamic requestedClassObject = Create(currentClassType);

                MethodInfo method = currentClassType.GetMethod(methodName);
                return method.Invoke(requestedClassObject, methodParameters);
            }
            catch (Exception executionError)
            {
                throw executionError;
            }
        }
        #endregion
    }
}
