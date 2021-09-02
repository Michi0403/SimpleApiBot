using IcqBotNetCore.BusinessObjects.Commands;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace IcqBotNetCore.Extensions
{
    static public class ParamListExtension
    {
        /// <summary>
        /// Casts ConcurrentDictionary<ParamTypeEnum, string> to Dictionary<string,string>
        /// </summary>
        /// <param name="listToConvert"></param>
        /// <returns></returns>
        public static Dictionary<string,string> ToGenericCommandTemplateList(this ConcurrentDictionary<ParamTypeEnum, string> listToConvert)
        {
            Dictionary<string, string> returnDictionary = new Dictionary<string, string>();
            foreach(var entry in listToConvert)
            {
                returnDictionary.Add(entry.Key.ToString(), entry.Value);
            }
            return returnDictionary;
        }

        /// <summary>
        /// Casts Dictionary<ParamTypeEnum, string>  ConcurrentDictionary<ParamTypeEnum, string> 
        /// </summary>
        /// <param name="listToConvert"></param>
        /// <returns></returns>
        public static ConcurrentDictionary<ParamTypeEnum, string> ToConcurrentDictionary(this Dictionary<ParamTypeEnum, string> listToConvert)
        {
            ConcurrentDictionary<ParamTypeEnum, string> returnDictionary = new ConcurrentDictionary<ParamTypeEnum, string>();
            foreach (var entry in listToConvert)
            {
                returnDictionary.TryAdd(entry.Key, entry.Value);
            }
            return returnDictionary;
        }
    }
}
