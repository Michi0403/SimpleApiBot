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
        [DataMember]
        public static readonly ParamTypeEnum isRootTrunk = new ParamTypeEnum("RootTrunk of JSON Doc;");
        [DataMember]
        public static readonly ParamTypeEnum IsObject = new ParamTypeEnum("Object of JSON Doc;");
        [DataMember]
        public static readonly ParamTypeEnum IsArray = new ParamTypeEnum("Array of JSON Doc;");
        [DataMember]
        public static readonly ParamTypeEnum NameLessArrayMember = new ParamTypeEnum("NameLessArrayMember");
        protected ParamTypeEnum(string value) : base(value)
        {
        }
    }
}
