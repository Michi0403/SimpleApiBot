using IcqBotNetCore.Abstract;
using IcqBotNetCore.BusinessObjects;
using IcqBotNetCore.EventArgs;
using IcqBotNetCore.Helper;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace IcqBotNetCore.Interfaces
{
    public interface IBot
    {
        HttpClient HttpClient
        {
            get;
        }

        HttpClientHandler HttpClientHandler
        {
            get;
        }

        LoggingHandler LoggingHandler
        {
            get;
        }

        Config Config
        {
            get;
        }
        IBotCommandFactoryTemplate BotCommandFactory
        {
            get;
            set;
        }

        ResponseFactoryTemplate BotResponseFactory
        {
            get;
            set;
        }

        string Token
        {
            get;
        }

        void Initialize(Config config);
        bool ProcessCommands();

        void ResponseContentIncoming(object sender, GenericEventArgs<string, byte[]> e);
        void RequestContentIncoming(object sender, GenericEventArgs<string> e);
        void RequestIncoming(object sender, GenericEventArgs<HttpRequestMessage> e);
    }
}