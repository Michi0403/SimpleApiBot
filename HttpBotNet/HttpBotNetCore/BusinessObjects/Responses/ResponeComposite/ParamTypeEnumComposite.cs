using BotNetCore.Abstract;
using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
using BotNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BotNetCore.BusinessObjects.Responses.ResponeComposite
{
    /// <summary>
    /// Default Composite for ParamTypeEnum used to store children of ParamTypeEnum and value
    /// </summary>
    [DataContract]
    public class ParamTypeEnumComposite : ComponentParam , IDataFile
    {
        /// <summary>
        /// Store for Children of ParamTypeEnum and value
        /// </summary>
        [DataMember]
        public List<ComponentParam> children = new List<ComponentParam>();
        /// <summary>
        /// Default Constructor, use this
        /// </summary>
        /// <param name="paramTypeEnum">ParamTypeEnum for Composite Component</param>
        /// <param name="value">value for Param for Composite Component</param>
        public ParamTypeEnumComposite(ParamTypeEnum paramTypeEnum, string value) : base(paramTypeEnum, value)
        {

        }
        /// <summary>
        /// Add leaf component to children of this composite
        /// </summary>
        /// <param name="leaf">leaf to add</param>
        /// <returns>success/failure bool</returns>
        public override bool Add(ComponentParam leaf)
        {
            try
            {
                children.Add(leaf);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Remove leaf component from children of this composite
        /// </summary>
        /// <param name="leaf">leaf to remove</param>
        /// <returns>success/failure bool</returns>
        public override bool Remove(ComponentParam leaf)
        {
            try
            {
                children.Remove(leaf);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Returns all Values 
        /// </summary>
        /// <returns>Returns all Values includes all Child Values of this composite</returns>
        public override List<ComponentParam> ReturnValue()
        {
            try
            {
                List<ComponentParam> returnValues = new List<ComponentParam>();
                returnValues.Add(this);//(this.paramTypeEnum, this.value));
                foreach (var child in children)
                {
                    if(child is ParamTypeEnumComposite paramTypeEnumComposite) returnValues.AddRange(paramTypeEnumComposite.ReturnValue());
                    if(child is ParamTypeEnumLeaf leaf) returnValues.Add(leaf);
                }
                return returnValues;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<ComponentParam>();
            }
        }
        /// <summary>
        /// Show Values of all
        /// </summary>
        /// <returns></returns>
        public override bool ShowValues()
        {
            try
            {
                Console.WriteLine("Composite ParamTypeEnum: " + this.paramTypeEnum.Value.ToString() + " value: "+ this.value.ToString());
                foreach(var child in children)
                {
                    if (child is ParamTypeEnumComposite paramTypeEnumComposite) paramTypeEnumComposite.ShowValues();
                    Console.WriteLine("ParamTypeEnum: "+ child.paramTypeEnum.Value.ToString() + " value: " + child.value.ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
