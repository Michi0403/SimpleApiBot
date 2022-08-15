using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
using BotNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BotNetCore.Abstract
{
    [DataContract]
    public abstract class ComponentParam : IDataFile
    {
        [DataMember]
        public string value;
        [DataMember]
        public ParamTypeEnum paramTypeEnum;
        public ComponentParam(ParamTypeEnum paramTypeEnum, string value)
        {
            this.value = value;
            this.paramTypeEnum = paramTypeEnum;
        }

        public abstract bool Add(ComponentParam leaf);
        public abstract bool Remove(ComponentParam leaf);


        public abstract bool ShowValues();

        public abstract List<(ParamTypeEnum,string)> ReturnValue();

    }
}
