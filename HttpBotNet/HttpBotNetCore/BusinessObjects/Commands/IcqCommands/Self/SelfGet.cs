using BotNetCore.Abstract;
using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BotNetCore.BusinessObjects.Commands.IcqCommands.Self
{
    [DataContract ]
    public class SelfGet : CommandTemplate
    {
        public SelfGet(HttpClient client, string token, Dictionary<ParamTypeEnum, string> parameter, string httpMethod) : base(client, token, parameter, httpMethod) 
        {
            _routeBaseAdress = @"/self/get";
        }

        [DataMember]
        public override string RouteBaseAdress { get => _routeBaseAdress; set { _routeBaseAdress = value; } }


        public override Task ProcessCommand()
        {
            return _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, this.ToString()) { Version = new Version(2, 0) });
        }
    }
}
