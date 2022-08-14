using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotNetCore.Abstract
{
    public abstract class ComponentParam
    {
        public string value;
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
