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
    /// Leaf/children for Composite Trunk
    /// </summary>
    [DataContract]
    public class ParamTypeEnumLeaf : ComponentParam, IDataFile
    {
        /// <summary>
        /// Default Constructor calls Constructor of Baseclass ComponentParam
        /// </summary>
        /// <param name="paramTypeEnum">ParamTypeEnum for this leaf</param>
        /// <param name="value">value for param of this leaf</param>
        public ParamTypeEnumLeaf(ParamTypeEnum paramTypeEnum, string value):base(paramTypeEnum,value)
        {

        }
        /// <summary>
        /// I have nothing todo with that I am a leaf
        /// </summary>
        /// <param name="leaf"></param>
        /// <returns></returns>
        public override bool Add(ComponentParam leaf)
        {
            try
            {
                Console.WriteLine("Not my responsibility I am a Leaf");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// I have nothing todo with that I am a leaf
        /// </summary>
        /// <param name="leaf"></param>
        /// <returns></returns>
        public override bool Remove(ComponentParam leaf)
        {
            try
            {
                Console.WriteLine("Not my responsibility I am a Leaf");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// I have nothing todo with that I am a leaf
        /// </summary>
        /// <param name="leaf"></param>
        /// <returns></returns>
        public override List<ComponentParam> ReturnValue()
        {
            try
            {
                Console.WriteLine("Not my responsibility I am a Leaf");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// I have nothing todo with that I am a leaf
        /// </summary>
        /// <param name="leaf"></param>
        /// <returns></returns>
        public override bool ShowValues()
        {
            try
            {
                Console.WriteLine("ParamTypeEnum: " + paramTypeEnum.Value.ToString() + " value: " + value);
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
