using IcqBotNetCore.Abstract;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IcqBotNetCore.BusinessObjects.Commands.Events
{
    /// <summary>
    /// 
    /// 
    /// </summary>
    [DataContract]
    public class EventsGet : CommandTemplate
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="client"></param>
        public EventsGet(string token, HttpClient client, Dictionary<string,string> parameter) : base(client, token, parameter) 
        {
            _routeBaseAdress = @"/events/get";
        }

        [DataMember]
        public override string RouteBaseAdress { get => _routeBaseAdress; set { _routeBaseAdress = value; } }


        public override Task ProcessCommand()
        {
            return _httpClient.SendAsync(new HttpRequestMessage(HttpMethod, this.ToString()) { Version = new Version(2, 0)});
        }
    }
}
