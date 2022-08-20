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
    public sealed class IcqParamTypeEnum : ParamTypeEnum
    {
        [DataMember]
        public static readonly IcqParamTypeEnum notDefined = new IcqParamTypeEnum("notDefined");
        [DataMember]
        public static readonly IcqParamTypeEnum about = new IcqParamTypeEnum("about");
        [DataMember]
        public static readonly IcqParamTypeEnum addedBy = new IcqParamTypeEnum("addedBy");
        [DataMember]
        public static readonly IcqParamTypeEnum admins = new IcqParamTypeEnum("admins");
        [DataMember]
        public static readonly IcqParamTypeEnum bold = new IcqParamTypeEnum("bold");
        [DataMember]
        public static readonly IcqParamTypeEnum callbackData = new IcqParamTypeEnum("callbackData");
        [DataMember]
        public static readonly IcqParamTypeEnum caption = new IcqParamTypeEnum("caption");
        [DataMember]
        public static readonly IcqParamTypeEnum chat = new IcqParamTypeEnum("chat");
        [DataMember]
        public static readonly IcqParamTypeEnum chatId = new IcqParamTypeEnum("chatId");
        [DataMember]
        public static readonly IcqParamTypeEnum code = new IcqParamTypeEnum("code");
        [DataMember]
        public static readonly IcqParamTypeEnum creator = new IcqParamTypeEnum("creator");
        [DataMember]
        public static readonly IcqParamTypeEnum description = new IcqParamTypeEnum("description");
        [DataMember]
        public static readonly IcqParamTypeEnum editedTimestamp = new IcqParamTypeEnum("editedTimestamp");
        [DataMember]
        public static readonly IcqParamTypeEnum eventId = new IcqParamTypeEnum("eventId");
        [DataMember]
        public static readonly IcqParamTypeEnum events = new IcqParamTypeEnum("events");
        [DataMember]
        public static readonly IcqParamTypeEnum fileId = new IcqParamTypeEnum("fileId");
        [DataMember]
        public static readonly IcqParamTypeEnum filename = new IcqParamTypeEnum("filename");
        [DataMember]
        public static readonly IcqParamTypeEnum firstName = new IcqParamTypeEnum("firstName");
        [DataMember]
        public static readonly IcqParamTypeEnum format = new IcqParamTypeEnum("format");
        [DataMember]
        public static readonly IcqParamTypeEnum forwardChatId = new IcqParamTypeEnum("forwardChatId");
        [DataMember]
        public static readonly IcqParamTypeEnum forwardMsgId = new IcqParamTypeEnum("forwardMsgId");
        [DataMember]
        public static readonly IcqParamTypeEnum from = new IcqParamTypeEnum("from");
        [DataMember]
        public static readonly IcqParamTypeEnum inlineKeyboardMarkup = new IcqParamTypeEnum("inlineKeyboardMarkup");
        [DataMember]
        public static readonly IcqParamTypeEnum inline_code = new IcqParamTypeEnum("inline_code");
        [DataMember]
        public static readonly IcqParamTypeEnum isBot = new IcqParamTypeEnum("isBot");
        [DataMember]
        public static readonly IcqParamTypeEnum italic = new IcqParamTypeEnum("italic");
        [DataMember]
        public static readonly IcqParamTypeEnum language = new IcqParamTypeEnum("language");
        [DataMember]
        public static readonly IcqParamTypeEnum lastEventId = new IcqParamTypeEnum("lastEventId");
        [DataMember]
        public static readonly IcqParamTypeEnum lastName = new IcqParamTypeEnum("lastName");
        [DataMember]
        public static readonly IcqParamTypeEnum length = new IcqParamTypeEnum("length");
        [DataMember]
        public static readonly IcqParamTypeEnum link = new IcqParamTypeEnum("link");
        [DataMember]
        public static readonly IcqParamTypeEnum mention = new IcqParamTypeEnum("mention");
        [DataMember]
        public static readonly IcqParamTypeEnum msgId = new IcqParamTypeEnum("msgId");
        [DataMember]
        public static readonly IcqParamTypeEnum newMembers = new IcqParamTypeEnum("newMembers");
        [DataMember]
        public static readonly IcqParamTypeEnum nick = new IcqParamTypeEnum("nick");
        [DataMember]
        public static readonly IcqParamTypeEnum offset = new IcqParamTypeEnum("offset");
        [DataMember]
        public static readonly IcqParamTypeEnum ok = new IcqParamTypeEnum("ok");
        [DataMember]
        public static readonly IcqParamTypeEnum ordered_list = new IcqParamTypeEnum("ordered_list");
        [DataMember]
        public static readonly IcqParamTypeEnum parseMode = new IcqParamTypeEnum("parseMode");
        [DataMember]
        public static readonly IcqParamTypeEnum parts = new IcqParamTypeEnum("parts");
        [DataMember]
        public static readonly IcqParamTypeEnum payload = new IcqParamTypeEnum("payload");
        [DataMember]
        public static readonly IcqParamTypeEnum photo = new IcqParamTypeEnum("photo");
        [DataMember]
        public static readonly IcqParamTypeEnum pollTime = new IcqParamTypeEnum("pollTime");
        [DataMember]
        public static readonly IcqParamTypeEnum pre = new IcqParamTypeEnum("pre");
        [DataMember]
        public static readonly IcqParamTypeEnum quote = new IcqParamTypeEnum("quote");
        [DataMember]
        public static readonly IcqParamTypeEnum removedBy = new IcqParamTypeEnum("removedBy");
        [DataMember]
        public static readonly IcqParamTypeEnum replyMsgId = new IcqParamTypeEnum("replyMsgId");
        [DataMember]
        public static readonly IcqParamTypeEnum showAlert = new IcqParamTypeEnum("showAlert");
        [DataMember]
        public static readonly IcqParamTypeEnum size = new IcqParamTypeEnum("size");
        [DataMember]
        public static readonly IcqParamTypeEnum strikethrough = new IcqParamTypeEnum("strikethrough");
        [DataMember]
        public static readonly IcqParamTypeEnum style = new IcqParamTypeEnum("style");
        [DataMember]
        public static readonly IcqParamTypeEnum text = new IcqParamTypeEnum("text");
        [DataMember]
        public static readonly IcqParamTypeEnum timestamp = new IcqParamTypeEnum("timestamp");
        [DataMember]
        public static readonly IcqParamTypeEnum title = new IcqParamTypeEnum("title");
        [DataMember]
        new public static readonly IcqParamTypeEnum token = new IcqParamTypeEnum("token");
        [DataMember]
        public static readonly IcqParamTypeEnum type = new IcqParamTypeEnum("type");
        [DataMember]
        public static readonly IcqParamTypeEnum underline = new IcqParamTypeEnum("underline");
        [DataMember]
        public static readonly IcqParamTypeEnum unordered_list = new IcqParamTypeEnum("unordered_list");
        [DataMember]
        public static readonly IcqParamTypeEnum url = new IcqParamTypeEnum("url");
        [DataMember]
        public static readonly IcqParamTypeEnum userId = new IcqParamTypeEnum("userId");
        [DataMember]
        public static readonly IcqParamTypeEnum Content = new IcqParamTypeEnum("Content");
        [DataMember]
        public static readonly IcqParamTypeEnum Headers = new IcqParamTypeEnum("Headers");
        [DataMember]
        public static readonly IcqParamTypeEnum Properties = new IcqParamTypeEnum("Properties");
        [DataMember]
        public static readonly IcqParamTypeEnum RequestUri = new IcqParamTypeEnum("RequestUri");
        [DataMember]
        public static readonly IcqParamTypeEnum Major = new IcqParamTypeEnum("Major");
        [DataMember]
        public static readonly IcqParamTypeEnum Method = new IcqParamTypeEnum("Method");
        [DataMember]
        public static readonly IcqParamTypeEnum Minor = new IcqParamTypeEnum("Minor");
        [DataMember]
        public static readonly IcqParamTypeEnum Options = new IcqParamTypeEnum("Options");
        [DataMember]
        public static readonly IcqParamTypeEnum Build = new IcqParamTypeEnum("Build");
        [DataMember]
        public static readonly IcqParamTypeEnum Key = new IcqParamTypeEnum("Key");
        [DataMember]
        public static readonly IcqParamTypeEnum Revision = new IcqParamTypeEnum("Revision");
        [DataMember]
        public static readonly IcqParamTypeEnum MajorRevision = new IcqParamTypeEnum("MajorRevision");
        [DataMember]
        public static readonly IcqParamTypeEnum MinorRevision = new IcqParamTypeEnum("MinorRevision");
        [DataMember]
        public static readonly new IcqParamTypeEnum Value = new IcqParamTypeEnum("Value");
        [DataMember]
        public static readonly IcqParamTypeEnum Version = new IcqParamTypeEnum("Version");
        [DataMember]
        public static readonly IcqParamTypeEnum VersionPolicy = new IcqParamTypeEnum("VersionPolicy");
        [DataMember]
        public static readonly IcqParamTypeEnum Request = new IcqParamTypeEnum("Request");
        [DataMember]
        public static readonly IcqParamTypeEnum NamelessArray = new IcqParamTypeEnum("NamelessArray");

        public IcqParamTypeEnum(string value) : base(value)
        {
        }
    }
}