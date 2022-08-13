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
using BotNetCore.BusinessObjects.Commands.IcqCommands.Self;
using System.Reflection;
using BotNetCore.Attributes;
using BotNetCore.Extensions;

namespace BotNetCore.Factories
{
    public class IcqCommandFactory : IBotCommandFactoryTemplate
    {
       

        public IcqCommandFactory(HttpClient httpClientRef, string token) : base(httpClientRef, token)
        {
        }
        /// <summary>
        /// Locates with help of Attributes right Command from apiCommandEnum, passes the parameters and returns a IBotCommand.
        /// </summary>
        /// <param name="apiCommandEnum"></param>
        /// <param name="httpMethod"></param>
        /// <param name="parameter"></param>
        /// <param name="multipartFormDataContent"></param>
        /// <returns></returns>
        public override IBotCommand CreateCommand(
        ApiCommandEnum apiCommandEnum,
        HttpMethodEnum httpMethod,
        ConcurrentDictionary<ParamTypeEnum, string> parameter = null,
        MultipartFormDataContent multipartFormDataContent = null)
        {
            try
            {
                IBotCommand botCommand = null;

                IcqApiCommandEnum icqApiCommandEnum = apiCommandEnum as IcqApiCommandEnum;
                if (icqApiCommandEnum is null) return new SelfGet(null, null,null,null);

                foreach(var member in typeof(IcqApiCommandEnum).GetMembers().Where(x => x.Name == icqApiCommandEnum.Value))
                {
                    Object[] memberAttributes = member.GetCustomAttributes(true);
                    if(memberAttributes.Length>0)
                    {
                        CommandAttribute commandAttribute = (CommandAttribute) memberAttributes.FirstOrDefault(x => x.GetType().Name == "CommandAttribute");
                        if (commandAttribute!= null)
                        {
                            if(commandAttribute.CommandType is Type commandType) {
                                
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

                                //botCommand = (IBotCommand)Activator.CreateInstance(
                                //    commandType.GetType(),BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.OptionalParamBinding,null,
                                //    new object[] { _httpClient, _token, parameter, httpMethod.Value }, null);
                                return botCommand;
                            }
                        }
                        else
                        {
                            throw new ArgumentNullException($"No CommandAttribute defined for {icqApiCommandEnum}");
                        }
                    }
                }
                if(botCommand == null) throw new ApplicationException("Could not create ICommand, Method properties invalid");
                return botCommand;
            }
            catch (Exception ex)
            {
                 Console.WriteLine(ex);
                return null;
            }
        }

        /// <summary>
        /// Default Command is Self Get
        /// </summary>
        /// <param name="command">IcqBotNetCore.BusinessObjects.Commands.ApiCommand</param>
        /// <param name="httpMethod">IcqBotNetCore.BusinessObjects.Commands.HttpMethodEnum</param>
        /// <param name="parameter">ConcurrentDictionary<ParamTypeEnum, string></param>
        /// <param name="multipartFormDataContent">MultipartFormDataContent</param>
        /// <returns></returns>
        public override bool TryCreateCommandAndPutToQueue(ApiCommandEnum apiCommandEnum, HttpMethodEnum httpMethod, ConcurrentDictionary<ParamTypeEnum, string> parameter = null, MultipartFormDataContent multipartFormDataContent = null)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
