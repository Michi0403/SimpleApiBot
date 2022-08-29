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
        public static Queue<ParamTypeEnumComposite> ReadParamTypeEnumCompositeRecursive(ParamTypeEnumComposite compositeToSearchIn, ParamTypeEnum paramToSearchFor)
        {
            try
            {
                Queue<ParamTypeEnumComposite> paramTypeEnumComposites = new Queue<ParamTypeEnumComposite>();
                if(compositeToSearchIn != null)
                    SearchAndAdd(compositeToSearchIn, paramToSearchFor, paramTypeEnumComposites);
                else
                    throw new ArgumentNullException("compositeToSearchIn was null, can't search in a null value for ParamTypeEnum's");

                return paramTypeEnumComposites;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Queue<ParamTypeEnumComposite>();
            }
        }

        private static void SearchAndAdd(ParamTypeEnumComposite compositeToSearchIn, ParamTypeEnum paramToSearchFor, Queue<ParamTypeEnumComposite> paramTypeEnumComposites)
        {
            try
            {
                foreach(var entry in compositeToSearchIn.children)
                {
                    if(entry.paramTypeEnum.Value == paramToSearchFor.Value)
                    {
                        paramTypeEnumComposites.Enqueue(compositeToSearchIn);
                    }
                    if(entry is ParamTypeEnumComposite paramTypeEnumComposite)
                    {
                        SearchAndAdd( paramTypeEnumComposite,paramToSearchFor,paramTypeEnumComposites);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
