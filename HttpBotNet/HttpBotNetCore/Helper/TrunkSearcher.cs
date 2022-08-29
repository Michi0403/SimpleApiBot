using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
using BotNetCore.BusinessObjects.Responses.ResponeComposite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotNetCore.Helper
{
    /// <summary>
    /// Search Methods for members in Response Trunks
    /// </summary>
    static public class TrunkSearcher
    {
        /// <summary>
        /// Returns Composite of Objects which contained the found param...
        /// </summary>
        /// <param name="compositeToSearchIn"></param>
        public static List<ParamTypeEnumComposite> ReadParamTypeEnumCompositeRecursive(ParamTypeEnumComposite compositeToSearchIn, ParamTypeEnum paramToSearchFor)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<ParamTypeEnumComposite>();
            }
        }
    }
}
