using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BotNetCore.BusinessObjects.Enums.ApiCommandEnums
{
    [DataContract]
    public class ParamTypeEnum : TypeSafeStringEnum
    {
        [DataMember]
        public static readonly ParamTypeEnum token = new ParamTypeEnum("token");
        protected ParamTypeEnum(string value) : base(value)
        {
        }
    }
}
