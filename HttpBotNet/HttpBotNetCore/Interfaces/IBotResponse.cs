using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
using BotNetCore.BusinessObjects.Responses.ResponeComposite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotNetCore.Interfaces
{
    public interface IBotResponse : IDataFile
    {
        public ParamTypeEnumComposite Request
        {
            get;
        }
        public ParamTypeEnumComposite Response
        {
            get;
        }
    }
}
