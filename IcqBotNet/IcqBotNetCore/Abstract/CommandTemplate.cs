using IcqBotNetCore.BusinessObjects.Commands;
using IcqBotNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IcqBotNetCore.Abstract
{
    [DataContract]

    public abstract class CommandTemplate : IBotCommand
    {
        [DataMember]
        public abstract string RouteBaseAdress { get; set; }

        [DataMember]
        protected string _routeBaseAdress;

        protected HttpMethod HttpMethod { get; set; }


        [DataMember]
        public string _httpMethod { get => HttpMethod.ToString(); set => HttpMethod = new HttpMethod(value); }
        [NonSerialized]
        public HttpClient _httpClient;
        [DataMember]
        protected string _token;


        public CommandTemplate(HttpClient client, string token, Dictionary<string, string> parameter = null, string httpMethod = "get")
        {
            this._httpClient = client;
            this._token = token;
            this.Parameter.Add("token", token);
            if (parameter != null)
                foreach (var param in parameter)
                    this.Parameter.Add(param.Key, param.Value);
            this.HttpMethod = new HttpMethod(httpMethod);
        }


        [DataMember]
        public Dictionary<string, string> Parameter = new Dictionary<string, string>();
        public virtual Task ProcessCommand()
        {
            return _httpClient.SendAsync(new HttpRequestMessage(HttpMethod, this.ToString()) { Version = new Version(2, 0) });
        }

        public override string ToString()
        {

            StringBuilder fullUrl = new StringBuilder();
            fullUrl.Append(_httpClient.BaseAddress.OriginalString.TrimEnd('/') + '/' + _routeBaseAdress.TrimEnd('/').TrimStart('/') + '?');
            foreach (KeyValuePair<string, string> entry in Parameter)
            {
                fullUrl.Append(entry.Key + '=' + entry.Value +'&');
            }
            return fullUrl.ToString().TrimEnd('&');
        }
    }
}
