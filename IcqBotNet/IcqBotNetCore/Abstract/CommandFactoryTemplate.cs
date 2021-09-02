using IcqBotNetCore.BusinessObjects;
using IcqBotNetCore.BusinessObjects.Commands;
using IcqBotNetCore.BusinessObjects.Commands.Events;
using IcqBotNetCore.BusinessObjects.Commands.Self;
using IcqBotNetCore.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using IcqBotNetCore.Extensions;
using IcqBotNetCore.BusinessObjects.Commands.Messages;

namespace IcqBotNetCore.Abstract
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
        public virtual IBotCommand CreateCommand(   ApiCommandEnum command = ApiCommandEnum.SelfGet, 
                                                    HTTPMethodEnum httpMethod = HTTPMethodEnum.Get, 
                                                    ConcurrentDictionary<ParamTypeEnum, string> parameter = null, 
                                                    MultipartFormDataContent multipartFormDataContent = null) 
                                                    => 
                                                         command switch
                                                        {
                                                            ApiCommandEnum.SelfGet => new Self(_token,_httpClient),
                                                            ApiCommandEnum.GetEvents => new EventsGet(_token,_httpClient,parameter.ToGenericCommandTemplateList()),
                                                            ApiCommandEnum.SendText => new SendText(_token,_httpClient,parameter.ToGenericCommandTemplateList()),
            
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
        public virtual bool TryCreateCommandAndPutToQueue(ApiCommandEnum command = ApiCommandEnum.SelfGet,
                                                    HTTPMethodEnum httpMethod = HTTPMethodEnum.Get,
                                                    ConcurrentDictionary<ParamTypeEnum, string> parameter = null,
                                                    MultipartFormDataContent multipartFormDataContent = null)
        {
            try
            {
                IBotCommand iBotCommand = null;
                switch (command)
                {
                    case ApiCommandEnum.AnswerCallbackQuery:
                        break;
                    case ApiCommandEnum.BlockUser:
                        break;
                    case ApiCommandEnum.DeleteMember:
                        break;
                    case ApiCommandEnum.DeleteMessage:
                        break;
                    case ApiCommandEnum.EditText:
                        break;
                    case ApiCommandEnum.GetAdmins:
                        break;
                    case ApiCommandEnum.GetBlockedUsers:
                        break;
                    case ApiCommandEnum.GetEvents:
                        iBotCommand = new EventsGet(_token, _httpClient, parameter.ToGenericCommandTemplateList());
                        break;
                    case ApiCommandEnum.GetFileInfo:
                        break;
                    case ApiCommandEnum.GetInfo:
                        break;
                    case ApiCommandEnum.GetMembers:
                        break;
                    case ApiCommandEnum.PinMessage:
                        break;
                    case ApiCommandEnum.ResolvePending:
                        break;
                    case ApiCommandEnum.SelfGet:
                        iBotCommand = new Self(_token, _httpClient);
                        break;
                    case ApiCommandEnum.SendActions:
                        break;
                    case ApiCommandEnum.SendFile:
                        break;
                    case ApiCommandEnum.SendText:
                        iBotCommand = new SendText(_token, _httpClient, parameter.ToGenericCommandTemplateList());
                        break;
                    case ApiCommandEnum.SendVoice:
                        break;
                    case ApiCommandEnum.SetAbout:
                        break;
                    case ApiCommandEnum.SetAvatar:
                        break;
                    case ApiCommandEnum.SetRules:
                        break;
                    case ApiCommandEnum.SetTitle:
                        break;
                    case ApiCommandEnum.UnblockUser:
                        break;
                    case ApiCommandEnum.UnpinMessage:
                        break;
                    default:
                        iBotCommand = new Self(_token, _httpClient);
                        break;
                }
                
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
        public virtual ConcurrentDictionary<string,string> CreateParamSet(ConcurrentDictionary<ParamTypeEnum,string> parameter )
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
