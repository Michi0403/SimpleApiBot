using IcqBotNetCore.Abstract;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IcqBotNetCore.BusinessObjects.Commands.Self
{
    [DataContract]
    public class Self : CommandTemplate
    {
        public Self(string token, HttpClient client) : base(client, token) 
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
