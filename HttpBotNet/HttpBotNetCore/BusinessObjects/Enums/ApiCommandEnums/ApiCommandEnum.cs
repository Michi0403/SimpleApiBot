using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BotNetCore.BusinessObjects.Enums.ApiCommandEnums
{
    /// <summary>
    /// TypeSafe EnumPattern implementation of ApiCommandEnum
    /// </summary>
    [DataContract]
    public class ApiCommandEnum : TypeSafeStringEnum
    {
        /// <summary>
        /// Constructor for ApiCommandEnums calls Constructor of TypeSafeStringEnum
        /// </summary>
        /// <param name="value"></param>
        protected ApiCommandEnum(string value) : base(value)
        {
        }
    }
}
