using IcqBotNetCore.Abstract;
using IcqBotNetCore.BusinessObjects.Commands;
using IcqBotNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IcqBotNetCore.BusinessObjects
{
    /// <summary>
    /// Config file that provides Necessary settings for IcqBot
    /// </summary>
    [DataContract]
    public class Config : IDataFile
    {
        /// <summary>
        /// List to store Commands for Serializer
        /// </summary>
        [DataMember]
        public List<CommandTemplate> CommandList { get; set; } = new List<CommandTemplate>();
        /// <summary>
        /// Stores in XML the Bot Applicationsettings
        /// </summary>
        [DataMember]
        public SettingConfig SettingConfig { get; set; } = new SettingConfig();

        [OnDeserialized]
        internal void OnSerializingMethod(StreamingContext context)
        {
            if (CommandList == null)
            {
                CommandList = new List<CommandTemplate>();
            }
            if (SettingConfig == null)
            {
                SettingConfig = new SettingConfig();
            }
        }
    }
}
