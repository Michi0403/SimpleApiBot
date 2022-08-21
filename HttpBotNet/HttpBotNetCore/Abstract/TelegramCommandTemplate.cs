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

    public abstract class TelegramCommandTemplate : CommandTemplate
    {
    
        public TelegramCommandTemplate(HttpClient client, string token, Dictionary<ParamTypeEnum, string> parameter, string httpMethod):base(client,token,parameter,httpMethod)
        {

        }

        public override string ToString()
        {
            try
            {
                StringBuilder fullUrl = new StringBuilder();

                string value = string.Empty;
                if (Parameter.Count > 0 && Parameter.ContainsKey(ParamTypeEnum.token))
                {
                    Parameter.TryGetValue(ParamTypeEnum.token, out value);
                    Parameter.Remove(ParamTypeEnum.token);
                }
                if(value == string.Empty && !string.IsNullOrWhiteSpace(_token))
                {
                    value = _token;
                } else if(value == string.Empty)
                    throw new Exception("no Token provided to TelegramCommandTemplate");
                fullUrl.Append(_httpClient.BaseAddress.OriginalString.TrimEnd('/') + '/' + "bot" + value.TrimEnd('/') + '/' + _routeBaseAdress.TrimEnd('/').TrimStart('/'));

                if(Parameter.Count>0)
                {
                    fullUrl.Append('?');
                }

                foreach (KeyValuePair<ParamTypeEnum, string> entry in Parameter)
                {
                    fullUrl.Append(entry.Key.Value + '=' + entry.Value + '&');
                }
                 return fullUrl.ToString().TrimEnd('&');
            }
            catch (Exception ex)
            {
                Console.WriteLine("To String failed");
                Console.WriteLine(ex.ToString());
                return String.Empty;
            }
           
        }
    }
}
