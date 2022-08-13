using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotNetCore.Interfaces
{
    public interface IBotResponse
    {
        public List<List<(ParamTypeEnum, object)>> Request
        {
            get;
        }
        public List<List<(ParamTypeEnum, object)>> Response
        {
            get;
        }
    }
}
