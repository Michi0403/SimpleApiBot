using BotNetCore.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotNetCore.Helper.Publisher
{
    /// <summary>
    /// Experimental implementation to find infos in responses
    /// </summary>
    public class OnFoundPublisher
    {
        /// <summary>
        /// Self to define method
        /// </summary>
        public Action OnFound { get; set; }
        /// <summary>
        /// Generic Found Method
        /// </summary>
        /// <typeparam name="T">type of used genericeventsargs type</typeparam>
        /// <param name="genericEventArgs"></param>

        public void Found<T>(GenericEventArgs<T> genericEventArgs)
        {
            try
            {
                OnFound?.DynamicInvoke(genericEventArgs);
            }    
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
