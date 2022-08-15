using BotNetCore.BusinessObjects.Commands;
using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
using BotNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BotNetCore.Abstract
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

        public CommandTemplate()
        {

        }
        public CommandTemplate(HttpClient client, string token, Dictionary<ParamTypeEnum, string> parameter, string httpMethod)
        {
            this._httpClient = client;
            this._token = token;
            this.Parameter.Add(ParamTypeEnum.token, token);
            if (parameter != null)
                foreach (var param in parameter)
                    this.Parameter.Add(param.Key, param.Value);
            this.HttpMethod = new HttpMethod(httpMethod);
        }


        [DataMember]
        public Dictionary<ParamTypeEnum, string> Parameter = new Dictionary<ParamTypeEnum, string>();
        public virtual Task ProcessCommand()
        {
            return _httpClient.SendAsync(new HttpRequestMessage(HttpMethod, this.ToString()) { Version = new Version(2, 0) });
        }

        public override string ToString()
        {

            StringBuilder fullUrl = new StringBuilder();
            fullUrl.Append(_httpClient.BaseAddress.OriginalString.TrimEnd('/') + '/' + _routeBaseAdress.TrimEnd('/').TrimStart('/') + '?');
            foreach (KeyValuePair<ParamTypeEnum, string> entry in Parameter)
            {
                fullUrl.Append(entry.Key.Value + '=' + entry.Value +'&');
            }
            return fullUrl.ToString().TrimEnd('&');
        }
    }
}
