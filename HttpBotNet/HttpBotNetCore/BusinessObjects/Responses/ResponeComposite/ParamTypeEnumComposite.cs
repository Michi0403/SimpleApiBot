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
    [DataContract]
    public class ParamTypeEnumComposite : ComponentParam , IDataFile
    {
        [DataMember]
        List<ComponentParam> children = new List<ComponentParam>();
        public ParamTypeEnumComposite(ParamTypeEnum paramTypeEnum, string value) : base(paramTypeEnum, value)
        {

        }

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

        public override List<(ParamTypeEnum, string)> ReturnValue()
        {
            try
            {
                List<(ParamTypeEnum, string)> returnValues = new List<(ParamTypeEnum, string)>();
                returnValues.Add((this.paramTypeEnum, this.value));
                Console.WriteLine("added : <"+this.paramTypeEnum.Value.ToString() + " value: " + this.value.ToString()+ " >to List");
                foreach (var child in children)
                {
                    returnValues.Add((child.paramTypeEnum, child.value));
                    Console.WriteLine("added : <" + child.paramTypeEnum.Value.ToString() + " value: " + child.value.ToString() + " >to List");
                }
                return returnValues;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<(ParamTypeEnum, string)>();
            }
        }

        public override bool ShowValues()
        {
            try
            {
                Console.WriteLine(this.paramTypeEnum.Value.ToString() + " value: "+ this.value.ToString());
                foreach(var child in children)
                {
                    Console.WriteLine(child.paramTypeEnum.Value.ToString() + " value: " + child.value.ToString());
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
