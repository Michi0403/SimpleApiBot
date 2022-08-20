using BotNetCore.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Collections.Concurrent;
using BotNetCore.Interfaces;
using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
using BotNetCore.BusinessObjects.Enums.HttpEnums;

namespace BotNetCore.Factories
{
    public class TelegramCommandFactory : IBotCommandFactoryTemplate
    {


        public TelegramCommandFactory(HttpClient httpClientRef, string token) : base(httpClientRef, token)
        {
        }

        public override IBotCommand CreateCommand(ApiCommandEnum apiCommandEnum, HttpMethodEnum httpMethod, ConcurrentDictionary<ParamTypeEnum, string> parameter = null, MultipartFormDataContent multipartFormDataContent = null)
        {
            throw new NotImplementedException();
        }

        public override bool TryCreateCommandAndPutToQueue(ApiCommandEnum apiCommandEnum, HttpMethodEnum httpMethod, ConcurrentDictionary<ParamTypeEnum, string> parameter = null, MultipartFormDataContent multipartFormDataContent = null)
        {
            throw new NotImplementedException();
        }
    }
}
