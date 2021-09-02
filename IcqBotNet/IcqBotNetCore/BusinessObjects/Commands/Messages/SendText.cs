using IcqBotNetCore.Abstract;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IcqBotNetCore.BusinessObjects.Commands.Messages
{
    [DataContract]
    public class SendText : CommandTemplate
    {
        public SendText(string token, HttpClient client, Dictionary<string, string> parameter) : base(client, token, parameter) 
        {
            _routeBaseAdress = @"/messages/sendText";
        }

        [DataMember]
        public override string RouteBaseAdress { get => _routeBaseAdress; set { _routeBaseAdress = value; } }


        public override Task ProcessCommand()
        {
            return _httpClient.SendAsync(new HttpRequestMessage(HttpMethod, this.ToString()) { Version = new Version(2, 0) });
        }
    }
}
