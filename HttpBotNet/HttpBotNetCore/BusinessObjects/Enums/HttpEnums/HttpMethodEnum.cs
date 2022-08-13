using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BotNetCore.BusinessObjects.Enums.HttpEnums
{
    [DataContract]
    public sealed class HttpMethodEnum : TypeSafeStringEnum
    {
        [DataMember]
        public static readonly HttpMethodEnum Delete = new HttpMethodEnum("Delete");
        [DataMember]
        public static readonly HttpMethodEnum Get = new HttpMethodEnum("Get");
        [DataMember]
        public static readonly HttpMethodEnum Head = new HttpMethodEnum("Head");
        [DataMember]
        public static readonly HttpMethodEnum Options = new HttpMethodEnum("Options");
        [DataMember]
        public static readonly HttpMethodEnum Patch = new HttpMethodEnum("Patch");
        [DataMember]
        public static readonly HttpMethodEnum Post = new HttpMethodEnum("Post");
        [DataMember]
        public static readonly HttpMethodEnum Put = new HttpMethodEnum("Put");
        [DataMember]
        public static readonly HttpMethodEnum Trace = new HttpMethodEnum("Trace");

        public HttpMethodEnum(string value) : base(value)
        {
        }
    }
}
