using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BotNetCore.BusinessObjects.Enums
{
    [DataContract]
    public class TypeSafeStringEnum
    {
        protected TypeSafeStringEnum(string value)
        {
            this.Value = value;
        }
        [DataMember]
        public string Value { get; private set; }

        public TypeSafeStringEnum GetMe()
        {
            return this;
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
