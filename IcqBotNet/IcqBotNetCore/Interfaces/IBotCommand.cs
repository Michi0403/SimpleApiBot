using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IcqBotNetCore.Interfaces
{
    public interface IBotCommand
    {
        public Task ProcessCommand();
    }
}
