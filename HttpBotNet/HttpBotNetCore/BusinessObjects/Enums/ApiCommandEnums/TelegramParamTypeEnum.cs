﻿using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BotNetCore.BusinessObjects.Enums.ApiCommandEnums
{
    [DataContract]
    public sealed class TelegramParamTypeEnum : ParamTypeEnum
    {
        [DataMember]
        public static readonly TelegramParamTypeEnum Content = new TelegramParamTypeEnum("Content");
        [DataMember]
        public static readonly TelegramParamTypeEnum Headers = new TelegramParamTypeEnum("Headers");
        [DataMember]
        public static readonly TelegramParamTypeEnum Properties = new TelegramParamTypeEnum("Properties");
        [DataMember]
        public static readonly TelegramParamTypeEnum RequestUri = new TelegramParamTypeEnum("RequestUri");
        [DataMember]
        public static readonly TelegramParamTypeEnum Major = new TelegramParamTypeEnum("Major");
        [DataMember]
        public static readonly TelegramParamTypeEnum Method = new TelegramParamTypeEnum("Method");
        [DataMember]
        public static readonly TelegramParamTypeEnum Minor = new TelegramParamTypeEnum("Minor");
        [DataMember]
        public static readonly TelegramParamTypeEnum Options = new TelegramParamTypeEnum("Options");
        [DataMember]
        public static readonly TelegramParamTypeEnum Build = new TelegramParamTypeEnum("Build");
        [DataMember]
        public static readonly TelegramParamTypeEnum Key = new TelegramParamTypeEnum("Key");
        [DataMember]
        public static readonly TelegramParamTypeEnum Revision = new TelegramParamTypeEnum("Revision");
        [DataMember]
        public static readonly TelegramParamTypeEnum MajorRevision = new TelegramParamTypeEnum("MajorRevision");
        [DataMember]
        public static readonly TelegramParamTypeEnum MinorRevision = new TelegramParamTypeEnum("MinorRevision");
        [DataMember]
        public static readonly new TelegramParamTypeEnum Value = new TelegramParamTypeEnum("Value");
        [DataMember]
        public static readonly TelegramParamTypeEnum Version = new TelegramParamTypeEnum("Version");
        [DataMember]
        public static readonly TelegramParamTypeEnum VersionPolicy = new TelegramParamTypeEnum("VersionPolicy");
        [DataMember]
        public static readonly TelegramParamTypeEnum Request = new TelegramParamTypeEnum("Request");
        [DataMember]
        public static readonly TelegramParamTypeEnum ok = new TelegramParamTypeEnum("ok");

        [DataMember]
        public static readonly TelegramParamTypeEnum id = new TelegramParamTypeEnum("id");

        [DataMember]
        public static readonly TelegramParamTypeEnum is_bot = new TelegramParamTypeEnum("is_bot");

        [DataMember]
        public static readonly TelegramParamTypeEnum first_name = new TelegramParamTypeEnum("first_name");

        [DataMember]
        public static readonly TelegramParamTypeEnum username = new TelegramParamTypeEnum("username");

        [DataMember]
        public static readonly TelegramParamTypeEnum can_join_groups = new TelegramParamTypeEnum("can_join_groups");

        [DataMember]
        public static readonly TelegramParamTypeEnum can_read_all_group_messages = new TelegramParamTypeEnum("can_read_all_group_messages");

        [DataMember]
        public static readonly TelegramParamTypeEnum supports_inline_queries = new TelegramParamTypeEnum("supports_inline_queries");

        [DataMember]
        public static readonly TelegramParamTypeEnum result = new TelegramParamTypeEnum("result");

        public TelegramParamTypeEnum(string value) : base(value)
        {
        }
    }
}