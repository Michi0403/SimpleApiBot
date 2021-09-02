using IcqBotNetCore.Abstract;
using IcqBotNetCore.BusinessObjects.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcqBotNetCore.BusinessObjects.Responses
{
    public class Response : ResponseTemplate
    {
        public Response(List<List<(ParamTypeEnum, object)>> request, List<List<(ParamTypeEnum, object)>> respond) : base ( request, respond)
        {

        }
    }
}
