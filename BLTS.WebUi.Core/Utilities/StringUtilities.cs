using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace BLTS.WebUi.Utilities
{
    public class StringUtilities
    {
        private ReflectionTools _reflectionTools;
        public StringUtilities(ReflectionTools reflectionTools)
        {
            _reflectionTools = reflectionTools;
        }

        #region  string arrays
        private string[] smallNumbers = new string[] { "Zero", "One", "Two",
                "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
                "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen",
                "Seventeen", "Eighteen", "Nineteen" };

        private string[] _tens = new string[] { "", "", "Twenty", "Thirty", "Forty",
                "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        private string[] scaleNumbers = new string[] { "", "Thousand", "Million", "Billion" };
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentWorkingString"></param>
        /// <param name="IsOnlyEnds">trim ends vs remove all</param>
        /// <returns></returns>
        public string RemoveKnownSymbols(string currentWorkingString, bool IsOnlyEnds = true)
        {
            //ugly chars to remove from the front and end
            char[] charsToRemove = { ' ', '~', '`', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '=', '+', '\'', ';', ',', '{', '}', '[', ']' };

            if (!string.IsNullOrWhiteSpace(currentWorkingString))
                if (IsOnlyEnds)
                    return currentWorkingString.Trim(charsToRemove);
                else
                    return string.Join(" ", currentWorkingString.Split(charsToRemove));
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentWorkingString"></param>
        /// <returns></returns>
        public string FormatDynamicObjectPropertyName(string currentWorkingString)
        {
            if (!string.IsNullOrWhiteSpace(currentWorkingString))
            {
                TextInfo textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
                string cleanedPropertyName = RemoveKnownSymbols(currentWorkingString, false);

                //prep for title casing
                cleanedPropertyName = cleanedPropertyName.ToLower();

                //sets proper casing for all words - TitleCaseFtw
                List<string> testStringArray = new List<string>();
                testStringArray.AddRange(cleanedPropertyName.Split(' '));
                for (int index = testStringArray.Count - 1; index >= 0; index--)
                    testStringArray[index] = textInfo.ToTitleCase(testStringArray[index]);

                //remove white space
                cleanedPropertyName = string.Join("", testStringArray);

                return cleanedPropertyName;
            }
            else
                return null;
        }

        /// <summary>
        /// converts a collection to a value seperated string
        /// </summary>
        /// <param name="objectCollection"></param>
        /// <returns></returns>
        public string ConvertObjectCollectionToString<T>(ConcurrentBag<T> objectCollection, string separator = ", ")
        {
            StringBuilder compositString = new StringBuilder();
            bool firstTimeThroughLoop = new bool();
            firstTimeThroughLoop = true;

            foreach (T singleObject in objectCollection)
            {
                if (!firstTimeThroughLoop)
                    compositString.Append(separator);
                compositString.Append(singleObject);
                firstTimeThroughLoop = false;
            }
            return compositString.ToString();
        }

        /// <summary>
        /// converts a string (list) to a value ConcurrentBag of type T
        /// </summary>
        /// <param name="valueCollection"></param>
        /// <returns></returns>
        public ConcurrentBag<T> ConvertStringToObjectList<T>(string valueCollection, string separator = ",")
        {
            ConcurrentBag<T> returnCollection = new ConcurrentBag<T>();
            if (!string.IsNullOrWhiteSpace(valueCollection))
                foreach (string singleListItem in valueCollection.Split(separator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                    returnCollection.Add(_reflectionTools.ConvertObjectToType<T>(singleListItem));

            return returnCollection;
        }

        /// <summary>
        /// Check to see if any regex format is valid in the test string
        /// </summary>
        /// <param name="stringTestValue">string to test against regex</param>
        /// <param name="regexMatchPattern">regex format, if delimited, will search all and return true if any are true</param>
        /// <param name="delimiter">tripple pipe is default if null</param>
        /// <returns></returns>
        public bool IsRegexMatchAny(string stringTestValue, string regexMatchPattern, RegexOptions regexOptions = RegexOptions.CultureInvariant, string[] delimiter = null)
        {
            bool isMatch = false;
            if (delimiter == null)//set default if not specified
                delimiter = new string[] { "|||" };

            string[] formatCollection = regexMatchPattern.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

            if (formatCollection.Length > 1)
            {
                Parallel.ForEach(formatCollection, (singleRegExTextFormat, loopState) =>
                {
                    if (Regex.IsMatch(stringTestValue, singleRegExTextFormat, regexOptions))
                    {
                        isMatch = true;
                        loopState.Break();
                    }
                });
            }
            else
                isMatch = Regex.IsMatch(stringTestValue, formatCollection[0], regexOptions);

            return isMatch;
        }

        /// <summary>
        /// applies the specified reg ex formatting using a delimited list
        /// </summary>
        /// <param name="regexInputFormat">detect each format and apply the same indexed output format</param>
        /// <param name="regexOutputFormat">apply this format based on index of regexInputFormat</param>
        /// <param name="elementStringValue">apply the regex requested formatting to this string value and send as output</param>
        /// <param name="delimiter">splits the string using this value to apply nexted regex formats - default replacement value for null is ||| as used in BI overrides</param>
        /// <returns>outputs the regex formatted value of elementStringValue</returns>
        public string ApplyRegEx(string regexInputFormat, string regexOutputFormat, string elementStringValue, string[] delimiter = null)
        {
            if (!string.IsNullOrWhiteSpace(elementStringValue))
            {
                string[] outputformatCollection = null;//array for easy 100% orderly collections that are lighter weight than dictionaries
                string[] formatCollection = null;
                if (delimiter == null)//set default if not specified
                    delimiter = new string[] { "|||" };

                try
                {
                    outputformatCollection = regexOutputFormat.Split(delimiter, StringSplitOptions.None);//break out the individual instructions to be run in order
                    formatCollection = regexInputFormat.Split(delimiter, StringSplitOptions.None);

                    if (formatCollection != null && formatCollection.Length > 0
                       && outputformatCollection != null && outputformatCollection.Length == formatCollection.Length)
                    {

                        for (int index = 0; index < formatCollection.Length; index++)//cycle through the formatting collection in correct order
                        {
                            string currentRegexMatch = formatCollection[index];
                            string currentoutputFormat = outputformatCollection.ElementAtOrDefault(index);

                            try
                            {
                                if (string.IsNullOrWhiteSpace(currentoutputFormat.Trim()) || Regex.IsMatch(elementStringValue, currentRegexMatch, RegexOptions.CultureInvariant))//if the output format is specified then a match is required
                                    elementStringValue = Regex.Replace(elementStringValue, currentRegexMatch, currentoutputFormat, RegexOptions.CultureInvariant);
                                else
                                    elementStringValue = string.Empty;
                            }
                            catch (Exception regExError)
                            {
                                throw new System.InvalidOperationException("Apply Regex Failure - Field Value: " + elementStringValue + " - Search Pattern: " + currentRegexMatch + " - Output Format: " + currentoutputFormat + " - Generated an exception. Please correct this RegEx. Details: " + regExError.ToString(), regExError);
                            }

                        }

                    }
                }
                catch (Exception regExError)
                {
                    throw new System.InvalidOperationException("Regex Metadata Failure - Field Value: " + elementStringValue + " - Search Pattern: " + regexInputFormat + " - Output Format: " + regexOutputFormat + " - Generated an exception. Please correct this RegEx. Details: " + regExError.ToString(), regExError);
                }
            }

            return elementStringValue;
        }
    }
}
