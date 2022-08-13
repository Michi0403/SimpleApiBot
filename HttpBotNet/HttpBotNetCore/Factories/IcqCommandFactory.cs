using BotNetCore.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Collections.Concurrent;
using BotNetCore.Interfaces;

namespace BotNetCore.Factories
{
    public class IcqCommandFactory : IBotCommandFactoryTemplate
    {
       

        public IcqCommandFactory(HttpClient httpClientRef, string token) : base(httpClientRef, token)
        {
        }

    }
}
