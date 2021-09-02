using IcqBotNetCore.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Collections.Concurrent;
using IcqBotNetCore.Interfaces;

namespace IcqBotNetCore.Factories
{
    public class IcqCommandFactory : IBotCommandFactoryTemplate
    {
       

        public IcqCommandFactory(HttpClient httpClientRef, string token) : base(httpClientRef, token)
        {
        }

    }
}
