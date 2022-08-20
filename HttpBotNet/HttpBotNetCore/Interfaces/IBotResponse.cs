using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
using BotNetCore.BusinessObjects.Responses.ResponeComposite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotNetCore.Interfaces
{
    /// <summary>
    /// IBotResponse Interface, defines Get Properties and inherits of IDatafile
    /// </summary>
    public interface IBotResponse : IDataFile
    {
        /// <summary>
        /// Request content
        /// </summary>
        public ParamTypeEnumComposite Request
        {
            get;
        }
        /// <summary>
        /// Response content
        /// </summary>
        public ParamTypeEnumComposite Response
        {
            get;
        }
    }
}
