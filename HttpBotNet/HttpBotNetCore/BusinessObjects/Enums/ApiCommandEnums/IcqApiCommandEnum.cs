using BotNetCore.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BotNetCore.BusinessObjects.Enums.ApiCommandEnums
{
    [DataContract]
    public sealed class IcqApiCommandEnum : ApiCommandEnum
    {
        [DataMember, CommandAttribute(typeof(Commands.IcqCommands.Self.SelfGet))]
        public static readonly IcqApiCommandEnum AnswerCallbackQuery = new IcqApiCommandEnum("AnswerCallbackQuery");
        [DataMember]
        public static readonly IcqApiCommandEnum BlockUser = new IcqApiCommandEnum("BlockUser");
        [DataMember]
        public static readonly IcqApiCommandEnum DeleteMember = new IcqApiCommandEnum("DeleteMember");
        [DataMember]
        public static readonly IcqApiCommandEnum DeleteMessage = new IcqApiCommandEnum("DeleteMessage");
        [DataMember]
        public static readonly IcqApiCommandEnum EditText = new IcqApiCommandEnum("EditText");
        [DataMember]
        public static readonly IcqApiCommandEnum GetAdmins = new IcqApiCommandEnum("GetAdmins");
        [DataMember]
        public static readonly IcqApiCommandEnum GetBlockedUsers = new IcqApiCommandEnum("GetBlockedUsers");
        [DataMember, CommandAttribute(typeof(Commands.IcqCommands.Events.EventsGet))]
        public static readonly IcqApiCommandEnum GetEvents = new IcqApiCommandEnum("GetEvents");
        [DataMember]
        public static readonly IcqApiCommandEnum GetFileInfo = new IcqApiCommandEnum("GetFileInfo");
        [DataMember]
        public static readonly IcqApiCommandEnum GetInfo = new IcqApiCommandEnum("GetInfo");
        [DataMember]
        public static readonly IcqApiCommandEnum GetMembers = new IcqApiCommandEnum("GetMembers");
        [DataMember]
        public static readonly IcqApiCommandEnum PinMessage = new IcqApiCommandEnum("PinMessage");
        [DataMember]
        public static readonly IcqApiCommandEnum ResolvePending = new IcqApiCommandEnum("ResolvePending");
        [DataMember, CommandAttribute(typeof(Commands.IcqCommands.Self.SelfGet))]
        public static readonly IcqApiCommandEnum SelfGet = new IcqApiCommandEnum("SelfGet");
        [DataMember]
        public static readonly IcqApiCommandEnum SendActions = new IcqApiCommandEnum("SendActions");
        [DataMember]
        public static readonly IcqApiCommandEnum SendFile = new IcqApiCommandEnum("SendFile");
        [DataMember, CommandAttribute(typeof(Commands.IcqCommands.Messages.SendText))]
        public static readonly IcqApiCommandEnum SendText = new IcqApiCommandEnum("SendText");
        [DataMember]
        public static readonly IcqApiCommandEnum SendVoice = new IcqApiCommandEnum("SendVoice");
        [DataMember]
        public static readonly IcqApiCommandEnum SetAbout = new IcqApiCommandEnum("SetAbout");
        [DataMember]
        public static readonly IcqApiCommandEnum SetAvatar = new IcqApiCommandEnum("SetAvatar");
        [DataMember]
        public static readonly IcqApiCommandEnum SetRules = new IcqApiCommandEnum("SetRules");
        [DataMember]
        public static readonly IcqApiCommandEnum SetTitle = new IcqApiCommandEnum("SetTitle");
        [DataMember]
        public static readonly IcqApiCommandEnum UnblockUser = new IcqApiCommandEnum("UnblockUser");
        [DataMember]
        public static readonly IcqApiCommandEnum UnpinMessage = new IcqApiCommandEnum("UnpinMessage");
        public IcqApiCommandEnum(string value) : base(value)
        {
        }
    }
}
