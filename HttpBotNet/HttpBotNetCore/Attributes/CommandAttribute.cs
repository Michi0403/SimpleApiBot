using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotNetCore.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class CommandAttribute : Attribute
    {
        private Type commandType;

        public CommandAttribute(Type commandType)
        {
            this.commandType = commandType;
        }
        public Type CommandType { get { return commandType; } }
    }
}
