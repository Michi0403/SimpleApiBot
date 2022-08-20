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
using BotNetCore.Attributes;
using BotNetCore.BusinessObjects.Commands.TelegramCommands;
using BotNetCore.Extensions;
using System.Reflection;

namespace BotNetCore.Factories
{
    public class TelegramCommandFactory : IBotCommandFactoryTemplate
    {


        public TelegramCommandFactory(HttpClient httpClientRef, string token) : base(httpClientRef, token)
        {
        }

        public override IBotCommand CreateCommand(ApiCommandEnum apiCommandEnum, HttpMethodEnum httpMethod, ConcurrentDictionary<ParamTypeEnum, string> parameter = null, MultipartFormDataContent multipartFormDataContent = null)
        {
            try
            {
                IBotCommand botCommand = null;

                TelegramApiCommandEnum telegramApiCommandEnum = apiCommandEnum as TelegramApiCommandEnum;
                if (telegramApiCommandEnum is null) return new getMe(null, null, null, null);

                foreach (var member in typeof(TelegramApiCommandEnum).GetMembers().Where(x => x.Name == telegramApiCommandEnum.Value))
                {
                    Object[] memberAttributes = member.GetCustomAttributes(true);
                    if (memberAttributes.Length > 0)
                    {
                        CommandAttribute commandAttribute = (CommandAttribute)memberAttributes.FirstOrDefault(x => x.GetType().Name == "CommandAttribute");
                        if (commandAttribute != null)
                        {
                            if (commandAttribute.CommandType is Type commandType)
                            {

                                //dynamically invoking attribute type for future plugins or external access
                                ConstructorInfo constructorInfo = commandType.GetConstructor(
                                new Type[]
                                {
                                    typeof(HttpClient),
                                    typeof(string),
                                    typeof(Dictionary<ParamTypeEnum, string>),
                                    typeof(string)
                                });
                                botCommand = (IBotCommand)constructorInfo.Invoke(
                                new object[] { _httpClient, _token, parameter.ToParamTypeCommandTemplateList(), httpMethod.Value });
                                return botCommand;
                            }
                        }
                        else
                        {
                            throw new ArgumentNullException($"No CommandAttribute defined for {telegramApiCommandEnum}");
                        }
                    }
                }
                if (botCommand == null) throw new ApplicationException("Could not create ICommand, Method properties invalid");
                return botCommand;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public override bool TryCreateCommandAndPutToQueue(ApiCommandEnum apiCommandEnum, HttpMethodEnum httpMethod, ConcurrentDictionary<ParamTypeEnum, string> parameter = null, MultipartFormDataContent multipartFormDataContent = null)
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
