using IcqBotNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcqBotNetCore.EventArgs
{
    public class GenericEventArgs<T> : System.EventArgs , IGenericEventArgs
    {
        public T EventData { get; private set; }

        public GenericEventArgs() : base()
        {

        }

        public GenericEventArgs(T EventData)
        {
            this.EventData = EventData;
        }
    }

    public class GenericEventArgs<T, U> : GenericEventArgs<T>
    {
        public U EventData2 { get; private set; }

        public GenericEventArgs() : base()
        {

        }

        public GenericEventArgs(T EventData, U EventData2) : base (EventData)
        {
            this.EventData2 = EventData2;
        }
    }
}
