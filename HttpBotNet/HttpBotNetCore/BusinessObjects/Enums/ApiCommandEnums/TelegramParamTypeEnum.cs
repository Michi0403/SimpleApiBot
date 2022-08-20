using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
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

        [DataMember]
        public static readonly TelegramParamTypeEnum chat_id = new TelegramParamTypeEnum("chat_id");


        [DataMember]
        public static readonly TelegramParamTypeEnum text = new TelegramParamTypeEnum("text");


        [DataMember]
        public static readonly TelegramParamTypeEnum parse_mode = new TelegramParamTypeEnum("parse_mode");


        [DataMember]
        public static readonly TelegramParamTypeEnum entities = new TelegramParamTypeEnum("entities");


        [DataMember]
        public static readonly TelegramParamTypeEnum disable_web_page_preview = new TelegramParamTypeEnum("disable_web_page_preview");

        [DataMember]
        public static readonly TelegramParamTypeEnum disable_notification = new TelegramParamTypeEnum("disable_notification");

        [DataMember]
        public static readonly TelegramParamTypeEnum protect_content = new TelegramParamTypeEnum("protect_content");

        [DataMember]
        public static readonly TelegramParamTypeEnum reply_to_message_id = new TelegramParamTypeEnum("reply_to_message_id");

        [DataMember]
        public static readonly TelegramParamTypeEnum allow_sending_without_reply = new TelegramParamTypeEnum("allow_sending_without_reply");

        [DataMember]
        public static readonly TelegramParamTypeEnum reply_markup = new TelegramParamTypeEnum("reply_markup");

        [DataMember]
        public static readonly TelegramParamTypeEnum from_chat_id = new TelegramParamTypeEnum("from_chat_id");

        [DataMember]
        public static readonly TelegramParamTypeEnum message_id = new TelegramParamTypeEnum("message_id");

        [DataMember]
        public static readonly TelegramParamTypeEnum caption = new TelegramParamTypeEnum("caption");

        [DataMember]
        public static readonly TelegramParamTypeEnum Skip = new TelegramParamTypeEnum("Skip");

        [DataMember]
        public static readonly TelegramParamTypeEnum description = new TelegramParamTypeEnum("description");

        [DataMember]
        public static readonly TelegramParamTypeEnum error_code = new TelegramParamTypeEnum("error_code");

        [DataMember]
        public static readonly TelegramParamTypeEnum update_id = new TelegramParamTypeEnum("update_id");

        [DataMember]
        public static readonly TelegramParamTypeEnum last_name = new TelegramParamTypeEnum("last_name");

        [DataMember]
        public static readonly TelegramParamTypeEnum language_code = new TelegramParamTypeEnum("language_code");

        [DataMember]
        public static readonly TelegramParamTypeEnum from = new TelegramParamTypeEnum("from");

        [DataMember]
        public static readonly TelegramParamTypeEnum type = new TelegramParamTypeEnum("type");

        [DataMember]
        public static readonly TelegramParamTypeEnum chat = new TelegramParamTypeEnum("chat");

        [DataMember]
        public static readonly TelegramParamTypeEnum date = new TelegramParamTypeEnum("date");

        [DataMember]
        public static readonly TelegramParamTypeEnum offset = new TelegramParamTypeEnum("offset");

        [DataMember]
        public static readonly TelegramParamTypeEnum length = new TelegramParamTypeEnum("length");

        [DataMember]
        public static readonly TelegramParamTypeEnum message = new TelegramParamTypeEnum("message");


        [DataMember]
        public static readonly TelegramParamTypeEnum title = new TelegramParamTypeEnum("title");


        [DataMember]
        public static readonly TelegramParamTypeEnum all_members_are_administrators = new TelegramParamTypeEnum("all_members_are_administrators");


        [DataMember]
        public static readonly TelegramParamTypeEnum user = new TelegramParamTypeEnum("user");


        [DataMember]
        public static readonly TelegramParamTypeEnum status = new TelegramParamTypeEnum("status");


        [DataMember]
        public static readonly TelegramParamTypeEnum old_chat_member = new TelegramParamTypeEnum("old_chat_member");


        [DataMember]
        public static readonly TelegramParamTypeEnum new_chat_member = new TelegramParamTypeEnum("new_chat_member");


        [DataMember]
        public static readonly TelegramParamTypeEnum my_chat_member = new TelegramParamTypeEnum("my_chat_member");
        

        [DataMember]
        public static readonly TelegramParamTypeEnum new_chat_participant = new TelegramParamTypeEnum("new_chat_participant");
        [DataMember]
        public static readonly TelegramParamTypeEnum new_chat_members = new TelegramParamTypeEnum("new_chat_members");
        public TelegramParamTypeEnum(string value) : base(value)
        {
        }
    }
}