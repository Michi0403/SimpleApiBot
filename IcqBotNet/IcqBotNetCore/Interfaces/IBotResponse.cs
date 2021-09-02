using IcqBotNetCore.BusinessObjects.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcqBotNetCore.Interfaces
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
