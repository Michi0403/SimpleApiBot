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
    public sealed class TelegramApiCommandEnum : ApiCommandEnum
    {
        [DataMember, CommandAttribute(typeof(Commands.TelegramCommands.getMe))]
        public static readonly TelegramApiCommandEnum getMe = new TelegramApiCommandEnum("getMe");
        public TelegramApiCommandEnum(string value) : base(value)
        {
        }
    }
}
