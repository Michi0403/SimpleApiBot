using BotNetCore.Abstract;
using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
using BotNetCore.BusinessObjects.Responses.ResponeComposite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotNetCore.BusinessObjects.Responses
{
    public class Response : ResponseTemplate
    {
        public Response(ParamTypeEnumComposite request, ParamTypeEnumComposite respond) : base ( request, respond)
        {

        }
        //public Response(List<List<(TelegramParamTypeEnum, object)>> request, List<List<(TelegramParamTypeEnum, object)>> respond) : base(request, respond)
        //{

        //}
    }
}
