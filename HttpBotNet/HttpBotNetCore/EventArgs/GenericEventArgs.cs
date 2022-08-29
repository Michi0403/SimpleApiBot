using BotNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotNetCore.EventArgs
{
    /// <summary>
    /// Generic Event Args 1st Level
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericEventArgs<T> : System.EventArgs , IGenericEventArgs
    {
        /// <summary>
        /// Stores the first Level of EventData
        /// </summary>
        public T EventData { get; private set; }
        /// <summary>
        /// Constructor Base Class
        /// </summary>
        public GenericEventArgs() : base()
        {

        }
        /// <summary>
        /// takes EventData 1st Level
        /// </summary>
        /// <param name="EventData"></param>
        public GenericEventArgs(T EventData)
        {
            this.EventData = EventData;
        }
    }
    /// <summary>
    /// Generic Event Args 2nd Level
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class GenericEventArgs<T, U> : GenericEventArgs<T>
    {
        /// <summary>
        /// Stores the 2nd Level of EventData
        /// </summary>
        public U EventData2 { get; private set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public GenericEventArgs() : base()
        {

        }
        /// <summary>
        /// Calls Base Method, saves 2nd Level of EventData and gives first Level to the base Class
        /// </summary>
        /// <param name="EventData"></param>
        /// <param name="EventData2"></param>
        public GenericEventArgs(T EventData, U EventData2) : base (EventData)
        {
            this.EventData2 = EventData2;
        }
    }
}
