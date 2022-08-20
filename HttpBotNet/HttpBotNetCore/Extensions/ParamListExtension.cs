using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace BotNetCore.Extensions
{
    /// <summary>
    /// Extension to convert Dictionaries, Lists, ConcurrentDictionaries and so on
    /// </summary>
    static public class ParamListExtension
    {
        /// <summary>
        /// Casts ConcurrentDictionary ParamTypeEnum string to Dictionary ParamTypeEnum string
        /// </summary>
        /// <param name="listToConvert"></param>
        /// <returns></returns>
        public static Dictionary<ParamTypeEnum, string> ToParamTypeCommandTemplateList(this ConcurrentDictionary<ParamTypeEnum, string> listToConvert)
        {
            Dictionary<ParamTypeEnum, string> returnDictionary = new Dictionary<ParamTypeEnum, string>();
            foreach (var entry in listToConvert)
            {
                returnDictionary.Add(entry.Key, entry.Value);
            }
            return returnDictionary;
        }

        /// <summary>
        /// Casts ConcurrentDictionary ParamTypeEnum string to Dictionary string string 
        /// </summary>
        /// <param name="listToConvert"></param>
        /// <returns></returns>
        [Obsolete("Not in use anymore but maybe useful at a later point",false)]
        public static Dictionary<string,string> ToGenericCommandTemplateList(this ConcurrentDictionary<IcqParamTypeEnum, string> listToConvert)
        {
            try
            {
                Dictionary<string, string> returnDictionary = new Dictionary<string, string>();
                foreach (var entry in listToConvert)
                {
                    returnDictionary.Add(entry.Key.ToString(), entry.Value);
                }
                return returnDictionary;
            }
            catch (Exception ex)
            {
                Console.WriteLine("To Generic Command Template List failed");
                Console.WriteLine(ex.ToString());
                return new Dictionary<string, string>();
            }
            
        }

        /// <summary>
        /// Casts Dictionary IcqParamTypeEnum string  ConcurrentDictionary IcqParamTypeEnum string
        /// </summary>
        /// <param name="listToConvert"></param>
        /// <returns></returns>
        public static ConcurrentDictionary<IcqParamTypeEnum, string> ToConcurrentDictionary(this Dictionary<IcqParamTypeEnum, string> listToConvert)
        {
            try
            {
                ConcurrentDictionary<IcqParamTypeEnum, string> returnDictionary = new ConcurrentDictionary<IcqParamTypeEnum, string>();

                if (listToConvert == null) throw new ArgumentNullException("listToConvert was null! Can't cast ToConcurrentDictionary");
                foreach (var entry in listToConvert)
                {
                    returnDictionary.TryAdd(entry.Key, entry.Value);
                }
                return returnDictionary;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not convert to Concurrent Dictionary");
                Console.WriteLine(ex.ToString());
                return new ConcurrentDictionary<IcqParamTypeEnum, string>();
            }

        }

        /// <summary>
        /// Extension to convert  Dictionary ParamTypeEnum string to ConcurrentDictionary ParamTypeEnum string
        /// </summary>
        /// <param name="listToConvert">Dictionary to convert from</param>
        /// <returns>Converted ConcurrentDictionary</returns>
        public static ConcurrentDictionary<ParamTypeEnum, string> ToConcurrentDictionary(this Dictionary<ParamTypeEnum, string> listToConvert)
        {
            try
            {
                if(listToConvert == null) throw new ArgumentNullException("listToConvert was null! Can't cast ToConcurrentDictionary");
                ConcurrentDictionary<ParamTypeEnum, string> returnDictionary = new ConcurrentDictionary<ParamTypeEnum, string>();
                foreach (var entry in listToConvert)
                {
                    returnDictionary.TryAdd(entry.Key, entry.Value);
                }
                return returnDictionary;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not convert to Concurrent Dictionary");
                Console.WriteLine(ex.ToString());
                return new ConcurrentDictionary<ParamTypeEnum, string>();
            }
            
        }
    }
}
