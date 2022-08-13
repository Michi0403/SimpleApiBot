using BotNetCore.Abstract;
using BotNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotNetCore.Extensions
{
    public static class ICommandExtension
    {
        //public partial class Singleton
        //{
        //    public static Singleton Instance { get; set; } = new Singleton();
        //    private Singleton()
        //    {
        //    }
        //    public ISerializer Serializer { get; set; }
        //}

        public static List<CommandTemplate> ToGenericCommandTemplateList (this List<IBotCommand> listToConvert)
        {
            return listToConvert.Cast<CommandTemplate>().ToList();
        }

        public static void PopulateHttpParams<T>(this T command) where T : CommandTemplate, new()
        {

        }
    }
}
