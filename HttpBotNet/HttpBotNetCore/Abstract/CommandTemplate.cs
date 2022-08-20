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
    /// <summary>
    /// Command Template for IBotCommands
    /// </summary>
    [DataContract]
    public abstract class CommandTemplate : IBotCommand
    {
        /// <summary>
        /// Base Adress for Api Method
        /// </summary>
        [DataMember]
        public abstract string RouteBaseAdress { get; set; }
        /// <summary>
        /// protected Base Adress for Api Method
        /// </summary>
        [DataMember]
        protected string _routeBaseAdress;
        /// <summary>
        /// Property of HttpMethod
        /// </summary>
        protected HttpMethod HttpMethod { get; set; }
        /// <summary>
        /// httpMethodRef
        /// </summary>

        [DataMember]
        public string _httpMethod { get => HttpMethod.ToString(); set => HttpMethod = new HttpMethod(value); }
        /// <summary>
        /// HttpClient ref
        /// </summary>
        [NonSerialized]
        public HttpClient _httpClient;
        /// <summary>
        /// Token for Command Template
        /// </summary>
        [DataMember]
        protected string _token;
        /// <summary>
        /// Constructor just intended for serialization reasons
        /// </summary>
        public CommandTemplate()
        {

        }
        /// <summary>
        /// Default Constructor for Command Template, should be used by a new Command
        /// </summary>
        /// <param name="client">httpClientRef</param>
        /// <param name="token">Token for Api</param>
        /// <param name="parameter">Parameter for Command</param>
        /// <param name="httpMethod">http Method as String</param>
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

        /// <summary>
        /// Property of Parameter
        /// </summary>
        public Dictionary<ParamTypeEnum,string> Parameter { get; set; } = new Dictionary<ParamTypeEnum, string>();
        /// <summary>
        /// Process this CommandTemplate
        /// </summary>
        /// <returns></returns>
        public virtual Task ProcessCommand()
        {
            return _httpClient.SendAsync(new HttpRequestMessage(HttpMethod, this.ToString()) { Version = new Version(2, 0) });
        }
        /// <summary>
        /// Override of object ToString() to implement an easy logic to resolve Params
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try
            {
                StringBuilder fullUrl = new StringBuilder();
                fullUrl.Append(_httpClient.BaseAddress.OriginalString.TrimEnd('/') + '/' + _routeBaseAdress.TrimEnd('/').TrimStart('/') + '?');
                foreach (KeyValuePair<ParamTypeEnum, string> entry in Parameter)
                {
                    fullUrl.Append(entry.Key.Value + '=' + entry.Value + '&');
                }
                return fullUrl.ToString().TrimEnd('&');
            }
            catch (Exception ex)
            {
                Console.WriteLine("CommandTemplate ToString() failed");
                Console.WriteLine(ex.ToString());
                return String.Empty;
            }
           
        }
    }
}
