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
using System.Threading;

namespace BotNetCore.Abstract
{
    /// <summary>
    /// Template for IBotCommand Factories
    /// </summary>
    [DataContract]
    public abstract class IBotCommandFactoryTemplate
    {
        /// <summary>
        /// ConcurrentQueue for IBotCommand, used by factory
        /// </summary>
        [DataMember]
        public ConcurrentQueue<IBotCommand> CommandQueue { get; set; } = new ConcurrentQueue<IBotCommand>();
        /// <summary>
        /// HttpClientref used for this IBotCommandFactory
        /// </summary>
        public HttpClient HttpClient { get => _httpClient; }
        /// <summary>
        /// Apis usually use tokens for authentication
        /// </summary>
        public string Token { get => _token; }
        /// <summary>
        /// HttpClientRef for this Factory
        /// </summary>
        protected HttpClient _httpClient { get; set; }
        /// <summary>
        /// Token for ApiBot
        /// </summary>
        protected string _token = string.Empty;



        /// <summary>
        /// Construktor, pass httpClientRef from iBot, and authenticate token for Api
        /// </summary>
        /// <param name="httpClientRef"></param>
        /// <param name="token"></param>
        protected IBotCommandFactoryTemplate(HttpClient httpClientRef, string token)
        {
            _httpClient = httpClientRef;
            this._token = token;
        }
        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <param name="httpClientRef">httpClientRef</param>
        /// <param name="token">token</param>
        public void RefreshHttpClientRefAndToken(HttpClient httpClientRef, string token)
        {
            _httpClient = httpClientRef;
            this._token = token;
        }

        /// <summary>
        /// Creates IBotCommand
        /// </summary>
        /// <param name="apiCommandEnum">BotNetCore.BusinessObjects.Enums.ApiCommandEnums</param>
        /// <param name="httpMethod">IcqBotNetCore.BusinessObjects.Commands.HttpMethodEnum</param>
        /// <param name="parameter">ConcurrentDictionary ParamTypeEnum string </param>
        /// <param name="multipartFormDataContent">MultipartFormDataContent</param>
        /// <returns></returns>
        abstract public IBotCommand CreateCommand(
            ApiCommandEnum apiCommandEnum,
            HttpMethodEnum httpMethod,
            ConcurrentDictionary<ParamTypeEnum, string> parameter = null,
            MultipartFormDataContent multipartFormDataContent = null);

        //=>
        //     apiCommandEnum switch
        //    {
        //        //apiCommandEnum.SelfGet => new Self(_token,_httpClient),
        //        //IcqApiCommandEnum.GetEvents => new EventsGet(_token,_httpClient,parameter.ToGenericCommandTemplateList()),
        //        //IcqApiCommandEnum.SendText => new SendText(_token,_httpClient,parameter.ToGenericCommandTemplateList()),

        //        _ => throw new ArgumentException(message: "Invalid enum Value", paramName: nameof(httpMethod)),
        //    };
        /// <summary>
        /// Default Command is Self Get
        /// </summary>
        /// <param name="apiCommandEnum">IcqBotNetCore.BusinessObjects.Commands.ApiCommand</param>
        /// <param name="httpMethod">IcqBotNetCore.BusinessObjects.Commands.HttpMethodEnum</param>
        /// <param name="parameter">ConcurrentDictionary ParamTypeEnum string</param>
        /// <param name="multipartFormDataContent">MultipartFormDataContent</param>
        /// <returns></returns>
        abstract public bool TryCreateCommandAndPutToQueue(
            ApiCommandEnum apiCommandEnum,
            HttpMethodEnum httpMethod,
            ConcurrentDictionary<ParamTypeEnum, string> parameter = null,
            MultipartFormDataContent multipartFormDataContent = null);
        //{
        //    try
        //    {
        //        IBotCommand iBotCommand = null;
        //        //switch (command)
        //        //{
        //        //    case IcqApiCommandEnum.AnswerCallbackQuery:
        //        //        break;
        //        //    case IcqApiCommandEnum.BlockUser:
        //        //        break;
        //        //    case IcqApiCommandEnum.DeleteMember:
        //        //        break;
        //        //    case IcqApiCommandEnum.DeleteMessage:
        //        //        break;
        //        //    case IcqApiCommandEnum.EditText:
        //        //        break;
        //        //    case IcqApiCommandEnum.GetAdmins:
        //        //        break;
        //        //    case IcqApiCommandEnum.GetBlockedUsers:
        //        //        break;
        //        //    case IcqApiCommandEnum.GetEvents:
        //        //        iBotCommand = new EventsGet(_token, _httpClient, parameter.ToGenericCommandTemplateList());
        //        //        break;
        //        //    case IcqApiCommandEnum.GetFileInfo:
        //        //        break;
        //        //    case IcqApiCommandEnum.GetInfo:
        //        //        break;
        //        //    case IcqApiCommandEnum.GetMembers:
        //        //        break;
        //        //    case IcqApiCommandEnum.PinMessage:
        //        //        break;
        //        //    case IcqApiCommandEnum.ResolvePending:
        //        //        break;
        //        //    case IcqApiCommandEnum.SelfGet:
        //        //        iBotCommand = new Self(_token, _httpClient);
        //        //        break;
        //        //    case IcqApiCommandEnum.SendActions:
        //        //        break;
        //        //    case IcqApiCommandEnum.SendFile:
        //        //        break;
        //        //    case IcqApiCommandEnum.SendText:
        //        //        iBotCommand = new SendText(_token, _httpClient, parameter.ToGenericCommandTemplateList());
        //        //        break;
        //        //    case IcqApiCommandEnum.SendVoice:
        //        //        break;
        //        //    case IcqApiCommandEnum.SetAbout:
        //        //        break;
        //        //    case IcqApiCommandEnum.SetAvatar:
        //        //        break;
        //        //    case IcqApiCommandEnum.SetRules:
        //        //        break;
        //        //    case IcqApiCommandEnum.SetTitle:
        //        //        break;
        //        //    case IcqApiCommandEnum.UnblockUser:
        //        //        break;
        //        //    case IcqApiCommandEnum.UnpinMessage:
        //        //        break;
        //        //    default:
        //        //        iBotCommand = new Self(_token, _httpClient);
        //        //        break;
        //        //}
                
        //        return TryAddCommandToQueue(iBotCommand);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return false;
        //    }
        //}

        /// <summary>
        /// Cast ConcurrentDictionary ParamTypeEnum string to ConcurrentDictionary string string
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

        /// <summary>
        /// Try add iBotCommand to iBotCommandQueue of this Factory
        /// </summary>
        /// <param name="iBotCommand">command to put factory in collection</param>
        /// <returns>success/fail bool</returns>
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
        /// <summary>
        /// Try to put all iBotCommands of this List in collection
        /// </summary>
        /// <param name="iBotCommands">commandcollection to put in factory collection</param>
        /// <returns>success/fail bool</returns>
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
        /// <summary>
        /// Tries to let all Commands in CommandQueue of this Factory, run
        /// </summary>
        /// <param name="waitForCompletion">waits in 1 second step till all commands have been sended</param>
        /// <returns>success/fail bool</returns>
        public virtual bool TryRunFullQueue(bool waitForCompletion=false)
        {
            try
            {
                ParallelLoopResult parallelLoopResult = Parallel.ForEach(CommandQueue, command =>
                    {
                        command.ProcessCommand();  
                });
                if(waitForCompletion)
                {
                    while(!parallelLoopResult.IsCompleted)
                    {
                        Thread.Sleep(1000);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Try run next Command in Queue
        /// </summary>
        /// <returns></returns>
        public virtual bool TryRunNext()
        {
            try
            {
                IBotCommand iCommand;
                CommandQueue.TryDequeue(out iCommand);
                iCommand.ProcessCommand();
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
