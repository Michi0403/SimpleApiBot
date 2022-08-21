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
    /// <summary>
    /// Template for Components (Component Pattern)
    /// </summary>
    [DataContract]
    public abstract class ComponentParam : IDataFile
    {
        /// <summary>
        /// string Value of Component
        /// </summary>
        [DataMember]
        public string value;
        /// <summary>
        /// ParamTypeEnum of Component
        /// </summary>
        [DataMember]
        public ParamTypeEnum paramTypeEnum;
        /// <summary>
        /// Default Constructor of Base Class ComponentParam
        /// </summary>
        /// <param name="paramTypeEnum">ParamTypeEnum</param>
        /// <param name="value">value for Param</param>
        protected ComponentParam(ParamTypeEnum paramTypeEnum, string value)
        {
            this.value = value;
            this.paramTypeEnum = paramTypeEnum;
        }
        /// <summary>
        /// Add to Composite, should be empty for Leafs
        /// </summary>
        /// <param name="leaf"></param>
        /// <returns>Success/Failure bool</returns>
        public abstract bool Add(ComponentParam leaf);
        /// <summary>
        /// Remove from Composite, should be empty for Leafs
        /// </summary>
        /// <param name="leaf"></param>
        /// <returns>Success/Failure bool</returns>
        public abstract bool Remove(ComponentParam leaf);

        /// <summary>
        /// Show Values
        /// </summary>
        /// <returns>Success/Failure bool</returns>
        public abstract bool ShowValues();
        /// <summary>
        /// Return Value of all adressed Components
        /// </summary>
        /// <returns></returns>
        public abstract List<ComponentParam> ReturnValue();

    }
}
