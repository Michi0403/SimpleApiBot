using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BotNetCore.BusinessObjects
{
    /// <summary>
    /// General Botsettings
    /// </summary>
    [DataContract]
    public class SettingConfig
    {
        /// <summary>
        /// Token from Metabot in Icq
        /// </summary>
        [DataMember]
        /// <summary>
        /// Optional BotId
        /// </summary>
        [DataMember]
        public string BotId = @"760774823";
        /// <summary>
        /// bot nick
        /// </summary>
        [DataMember]
        public string Nick = @"MysterionBot";
        /// <summary>
        /// BaseAdress of connected WebApi
        /// </summary>
        [DataMember]
        public string ApiRoute = @"https://api.icq.net/bot/v1/";
        /// <summary>
        /// Path to SSL Certificate, you can let your Bot generate a certificate include IcqBotNetCore.Helper Namespace with Certificateutil
        /// </summary>
        [DataMember]
        public string PathToCert = Environment.CurrentDirectory.TrimEnd('\\') + '\\';
        /// <summary>
        /// Cert Filename
        /// </summary>
        [DataMember]
        public string CertFileName = "MyCert";
        /// <summary>
        /// Password for generated private keyfile, don't share it
        /// </summary>
        [DataMember]
        public string PasswordForPK = "P@55w0rd";
        /// <summary>
        /// Necessary requestheader
        /// </summary>
        [DataMember]
        public List<string> AcceptedHeader = new List<string>() { @"application/json" };
        /// <summary>
        /// Path to this Configfile
        /// </summary>
        [DataMember]
        public string PathToThisConfig = Environment.CurrentDirectory.TrimEnd('\\') + '\\' + "demoCfg.xml";
        /// <summary>
        /// Path for Path for Http Data
        /// </summary>
        [DataMember]
        public string PathForHttpData = Environment.CurrentDirectory.TrimEnd('\\') + '\\' + "httpData\\";
    }
}