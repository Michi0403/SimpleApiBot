using BotNetCore.BusinessObjects;
using BotNetCore.BusinessObjects.Commands;
using BotNetCore.BusinessObjects.Commands.IcqCommands.Events;
using BotNetCore.BusinessObjects.Commands.IcqCommands.Self;
using BotNetCore.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BotNetCore.Extensions;
using BotNetCore.BusinessObjects.Commands.IcqCommands.Messages;
using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
using BotNetCore.BusinessObjects.Enums.HttpEnums;

namespace BotNetCore.Abstract
{
    [DataContract]
    public abstract class IBotCommandFactoryTemplate
    {
        /// <summary>
        /// ConcurrentQueue for IBotCommand, used by factory
        /// </summary>
        [DataMember]
        public ConcurrentQueue<IBotCommand> CommandQueue { get; set; } = new ConcurrentQueue<IBotCommand>();
        public HttpClient HttpClient { get => _httpClient; }
        public string Token { get => _token; }

        protected HttpClient _httpClient { get; set; }
        protected string _token = string.Empty;



        /// <summary>
        /// Construktor, pass httpClientRef from iBot, and authenticate token for Api
        /// </summary>
        /// <param name="httpClientRef"></param>
        /// <param name="token"></param>
        public IBotCommandFactoryTemplate(HttpClient httpClientRef, string token)
        {
            _httpClient = httpClientRef;
            this._token = token;
        }

        public void RefreshHttpClientRefAndToken(HttpClient httpClientRef, string token)
        {
            _httpClient = httpClientRef;
            this._token = token;
        }

        /// <summary>
        /// Creates IBotCommand
        /// </summary>
        /// <param name="command">IcqBotNetCore.BusinessObjects.Commands.ApiCommand</param>
        /// <param name="httpMethod">IcqBotNetCore.BusinessObjects.Commands.HttpMethodEnum</param>
        /// <param name="parameter">ConcurrentDictionary<ParamTypeEnum, string></param>
        /// <param name="multipartFormDataContent">MultipartFormDataContent</param>
        /// <returns></returns>
        public virtual IBotCommand CreateCommand(   //TODO adapt to new enum structure
                                                    ApiCommandEnum apiCommandEnum, 
                                                    HttpMethodEnum httpMethod, 
                                                    ConcurrentDictionary<ParamTypeEnum, string> parameter = null, 
                                                    MultipartFormDataContent multipartFormDataContent = null) 
                                                    =>
                                                         apiCommandEnum switch
                                                        {
                                                            //apiCommandEnum.SelfGet => new Self(_token,_httpClient),
                                                            //IcqApiCommandEnum.GetEvents => new EventsGet(_token,_httpClient,parameter.ToGenericCommandTemplateList()),
                                                            //IcqApiCommandEnum.SendText => new SendText(_token,_httpClient,parameter.ToGenericCommandTemplateList()),
            
                                                            _ => throw new ArgumentException(message: "Invalid enum Value", paramName: nameof(httpMethod)),
                                                        };
        /// <summary>
        /// Default Command is Self Get
        /// </summary>
        /// <param name="command">IcqBotNetCore.BusinessObjects.Commands.ApiCommand</param>
        /// <param name="httpMethod">IcqBotNetCore.BusinessObjects.Commands.HttpMethodEnum</param>
        /// <param name="parameter">ConcurrentDictionary<ParamTypeEnum, string></param>
        /// <param name="multipartFormDataContent">MultipartFormDataContent</param>
        /// <returns></returns>
        public virtual bool TryCreateCommandAndPutToQueue(ApiCommandEnum apiCommandEnum,
                                                    HttpMethodEnum httpMethod,
                                                    ConcurrentDictionary<ParamTypeEnum, string> parameter = null,
                                                    MultipartFormDataContent multipartFormDataContent = null)
        {
            try
            {
                IBotCommand iBotCommand = null;
                //switch (command)
                //{
                //    case IcqApiCommandEnum.AnswerCallbackQuery:
                //        break;
                //    case IcqApiCommandEnum.BlockUser:
                //        break;
                //    case IcqApiCommandEnum.DeleteMember:
                //        break;
                //    case IcqApiCommandEnum.DeleteMessage:
                //        break;
                //    case IcqApiCommandEnum.EditText:
                //        break;
                //    case IcqApiCommandEnum.GetAdmins:
                //        break;
                //    case IcqApiCommandEnum.GetBlockedUsers:
                //        break;
                //    case IcqApiCommandEnum.GetEvents:
                //        iBotCommand = new EventsGet(_token, _httpClient, parameter.ToGenericCommandTemplateList());
                //        break;
                //    case IcqApiCommandEnum.GetFileInfo:
                //        break;
                //    case IcqApiCommandEnum.GetInfo:
                //        break;
                //    case IcqApiCommandEnum.GetMembers:
                //        break;
                //    case IcqApiCommandEnum.PinMessage:
                //        break;
                //    case IcqApiCommandEnum.ResolvePending:
                //        break;
                //    case IcqApiCommandEnum.SelfGet:
                //        iBotCommand = new Self(_token, _httpClient);
                //        break;
                //    case IcqApiCommandEnum.SendActions:
                //        break;
                //    case IcqApiCommandEnum.SendFile:
                //        break;
                //    case IcqApiCommandEnum.SendText:
                //        iBotCommand = new SendText(_token, _httpClient, parameter.ToGenericCommandTemplateList());
                //        break;
                //    case IcqApiCommandEnum.SendVoice:
                //        break;
                //    case IcqApiCommandEnum.SetAbout:
                //        break;
                //    case IcqApiCommandEnum.SetAvatar:
                //        break;
                //    case IcqApiCommandEnum.SetRules:
                //        break;
                //    case IcqApiCommandEnum.SetTitle:
                //        break;
                //    case IcqApiCommandEnum.UnblockUser:
                //        break;
                //    case IcqApiCommandEnum.UnpinMessage:
                //        break;
                //    default:
                //        iBotCommand = new Self(_token, _httpClient);
                //        break;
                //}
                
                return TryAddCommandToQueue(iBotCommand);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Cast ConcurrentDictionary<ParamTypeEnum,string> to ConcurrentDictionary<string,string>  
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public virtual ConcurrentDictionary<string,string> CreateParamSet(ConcurrentDictionary<IcqParamTypeEnum,string> parameter )
        {
            try
            {
                ConcurrentDictionary<string, string> paramSet = new ConcurrentDictionary<string, string>();
                Parallel.ForEach(parameter, entry => { paramSet.TryAdd(entry.Key.ToString(), entry.Value); });
                return paramSet;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return new ConcurrentDictionary<string, string>();
            }
        }


        public virtual bool TryAddCommandToQueue(IBotCommand iBotCommand)
        {
            try
            {
                CommandQueue.Enqueue(iBotCommand);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public virtual bool TryAddListIBotCommandToQueue(List<IBotCommand> iBotCommands)
        {
            try
            {
                foreach (IBotCommand command in iBotCommands)
                    CommandQueue.Enqueue(command);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public virtual bool TryRunFullQueue()
        {
            try
            {
                Parallel.ForEach(CommandQueue, command =>
                    {
                        command.ProcessCommand();  
                });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public virtual bool TryRunNext()
        {
            try
            {
                IBotCommand iCommand;
                return CommandQueue.TryDequeue(out iCommand);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
