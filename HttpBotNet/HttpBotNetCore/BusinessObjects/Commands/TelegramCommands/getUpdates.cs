using BotNetCore.Abstract;
using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BotNetCore.BusinessObjects.Commands.TelegramCommands
{
    /// <summary>
    /// getUpdates TelegramCommand, inherits from TelegramCommandTemplate 
    /// </summary>
    [DataContract ]
    public class getUpdates : TelegramCommandTemplate
    {
        /// <summary>
        /// Default and Necessary Constructor
        /// </summary>
        /// <param name="client">httpClientRef</param>
        /// <param name="token">token for httpClient</param>
        /// <param name="parameter">param for httpRequest</param>
        /// <param name="httpMethod">which httpMethod to take</param>
        public getUpdates(HttpClient client, string token, Dictionary<ParamTypeEnum, string> parameter, string httpMethod) : base(client, token, parameter, httpMethod)
        {
            _routeBaseAdress = @"/getUpdates";
        }
        /// <summary>
        /// Another Constructor for adding later a behavior to receive new events only
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token"></param>
        /// <param name="parameter"></param>
        /// <param name="httpMethod"></param>
        /// <param name="incrementYourself"></param>
        /// <exception cref="NotImplementedException"></exception>
        public getUpdates(HttpClient client, string token, Dictionary<ParamTypeEnum, string> parameter, string httpMethod, bool incrementYourself = false) : this(client, token, parameter, httpMethod) 
        {
            _routeBaseAdress = @"/getUpdates";
            if(incrementYourself)
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// RouteBaseAdress for this Command
        /// </summary>
        [DataMember]
        public override string RouteBaseAdress { get => _routeBaseAdress; set { _routeBaseAdress = value; } }

        /// <summary>
        /// Process HTTPRequest
        /// </summary>
        /// <returns></returns>
        public override Task ProcessCommand()
        {
            return _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, this.ToString()) { Version = new Version(2, 0) });
        }
    }
}
