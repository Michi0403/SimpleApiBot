using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
using BotNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BotNetCore.Abstract
{
    [DataContract]
    public abstract class ResponseTemplate : IBotResponse
    {
        [DataMember]
        protected List<List<(ParamTypeEnum, object)>> _respond;
        
        [DataMember]
        protected List<List<(ParamTypeEnum, object)>> _request;

        public ResponseTemplate(List<List<(ParamTypeEnum, object)>> request, List<List<(ParamTypeEnum, object)>> respond)
        {
            _respond = respond;
            _request = request;
        }

        public List<List<(ParamTypeEnum, object)>> Request => _request;

        public List<List<(ParamTypeEnum, object)>> Response => _respond;
    }
}
