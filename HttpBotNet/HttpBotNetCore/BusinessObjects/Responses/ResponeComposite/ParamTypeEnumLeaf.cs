using BotNetCore.Abstract;
using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotNetCore.BusinessObjects.Responses.ResponeComposite
{
    public class ParamTypeEnumLeaf : ComponentParam
    {
        public ParamTypeEnumLeaf(ParamTypeEnum paramTypeEnum, string value):base(paramTypeEnum,value)
        {

        }

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

        public override List<(ParamTypeEnum, string)> ReturnValue()
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

        public override bool ShowValues()
        {
            try
            {
                Console.WriteLine($"Hi I am Leaf. My ParamTypeEnum is {paramTypeEnum} and my Value is {value}");
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
