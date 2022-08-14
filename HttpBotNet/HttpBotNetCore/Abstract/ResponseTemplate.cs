using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
using BotNetCore.BusinessObjects.Responses.ResponeComposite;
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
        protected ParamTypeEnumComposite _respond;
        
        [DataMember]
        protected ParamTypeEnumComposite _request;

        public ResponseTemplate(ParamTypeEnumComposite request, ParamTypeEnumComposite respond)
        {
            _respond = respond;
            _request = request;
        }

        public ParamTypeEnumComposite Request => _request;

        public ParamTypeEnumComposite Response => _respond;
    }
}
