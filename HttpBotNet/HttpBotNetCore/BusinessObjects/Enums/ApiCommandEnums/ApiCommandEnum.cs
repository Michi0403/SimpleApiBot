using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BotNetCore.BusinessObjects.Enums.ApiCommandEnums
{
    [DataContract]
    public class ApiCommandEnum : TypeSafeStringEnum
    {
        protected ApiCommandEnum(string value) : base(value)
        {
        }
    }
}
