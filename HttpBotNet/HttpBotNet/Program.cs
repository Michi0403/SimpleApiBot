using HttpBotNet.Serializer;
using BotNetCore.Abstract;
using BotNetCore.Bot;
using BotNetCore.BusinessObjects;
using BotNetCore.BusinessObjects.Commands.IcqCommands.Self;
using BotNetCore.Extensions;
using BotNetCore.Factories;
using BotNetCore.Helper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;

namespace HttpBotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataFileExtension.Singleton.Instance.Serializer = new SimpleDataContractSerializer();
            StringExtension.Singleton.Instance.Serializer = new SimpleJsonSerializer();
            Config cfg = new Config();

            if (!File.Exists(@$"{cfg.SettingConfig.PathToCert.TrimEnd('\\') + '\\' + cfg.SettingConfig.CertFileName + ".pfx"}"  ))
            {
                Console.WriteLine("Create and Store SSL Certificate");
                CertificateUtil.MakeCert(cfg.SettingConfig.PathToCert, cfg.SettingConfig.CertFileName, cfg.SettingConfig.PasswordForPK);
                CertificateUtil.SaveCertForUser(cfg.SettingConfig.PathToCert, cfg.SettingConfig.CertFileName, cfg.SettingConfig.PasswordForPK);
            }
            cfg.CommandList.Clear();

            var bot = new IcqBot();
            bot.Initialize(cfg);
            cfg.CommandList.Clear();
            var commandFactory = new IcqCommandFactory(bot.HttpClient, cfg.SettingConfig.Token);
            Dictionary<ParamTypeEnum, string> testxxx = new Dictionary<ParamTypeEnum, string>() { { IcqParamTypeEnum.lastEventId, "0" }, { IcqParamTypeEnum.pollTime, "5" } };

            ConcurrentDictionary<ParamTypeEnum, string> parameter = new ConcurrentDictionary<ParamTypeEnum, string>(testxxx);
            commandFactory.TryAddCommandToQueue(commandFactory.CreateCommand((ApiCommandEnum)IcqApiCommandEnum.GetEvents, BotNetCore.BusinessObjects.Enums.HttpEnums.HttpMethodEnum.Get, parameter: parameter));

            //var test = commandFactory.TryRunQueue();
            //Console.ReadLine();
            //commandFactory.TryAddCommandToQueue(commandFactory.CreateCommand(ApiCommandEnum.SelfGet));
            //Dictionary<ParamTypeEnum, string> testxxxsdf = new Dictionary<ParamTypeEnum, string>() { { ParamTypeEnum.lastEventId, "1" }, { ParamTypeEnum.pollTime, "5" } };

            //ConcurrentDictionary<ParamTypeEnum, string> parametersdf = new ConcurrentDictionary<ParamTypeEnum, string>(testxxxsdf);
            //commandFactory.TryAddCommandToQueue(commandFactory.CreateCommand(ApiCommandEnum.GetEvents,parameter: parametersdf));

            //Dictionary<ParamTypeEnum, string> sendMessageParameter = new Dictionary<ParamTypeEnum, string>() { { ParamTypeEnum.chatId, "691762017@chat.agent" }, { ParamTypeEnum.text, "- @[247319424]" } };

            //commandFactory.TryAddCommandToQueue(commandFactory.CreateCommand(ApiCommandEnum.SendText,HTTPMethodEnum.Get,sendMessageParameter.ToConcurrentDictionary()));

            cfg.CommandList = commandFactory.CommandQueue.Cast<CommandTemplate>().ToList();
            var testdasdagf = commandFactory.TryRunFullQueue();
            cfg.Save(cfg.SettingConfig.PathToThisConfig + "demoCfg.xml");

            //var test = bot.BotResponseFactory.ResponseBag;



            //Console.WriteLine(bot.BotResponseFactory.ResponseBag.ToString());
            Console.ReadLine();
        }
    }
}
