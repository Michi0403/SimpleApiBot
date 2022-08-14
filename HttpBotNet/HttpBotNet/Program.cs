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
using BotNetCore.BusinessObjects.Enums.HttpEnums;
using BotNetCore.Interfaces;

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

            Dictionary<ParamTypeEnum, string> test2 = new Dictionary<ParamTypeEnum, string>() { { IcqParamTypeEnum.lastEventId, "1" }, { IcqParamTypeEnum.pollTime, "5" } };
            ConcurrentDictionary<ParamTypeEnum, string> parameter23 = new ConcurrentDictionary<ParamTypeEnum, string>(test2);
            if (commandFactory.CreateCommand((ApiCommandEnum)IcqApiCommandEnum.GetEvents, HttpMethodEnum.Get, parameter: parameter23) is IBotCommand botcommand3)
                if (botcommand3 != null) commandFactory.TryAddCommandToQueue(botcommand3);

            //Dictionary<ParamTypeEnum, string> testxxx = new Dictionary<ParamTypeEnum, string>() { };
            //ConcurrentDictionary<ParamTypeEnum, string> parameter = new ConcurrentDictionary<ParamTypeEnum, string>(testxxx);
            //if (commandFactory.CreateCommand((ApiCommandEnum)IcqApiCommandEnum.SelfGet, HttpMethodEnum.Get, parameter: parameter)is IBotCommand botcommand2)
            //    if (botcommand2 != null) commandFactory.TryAddCommandToQueue(botcommand2);

            //Dictionary<ParamTypeEnum, string> sendMessageParameter = new Dictionary<ParamTypeEnum, string>() { { IcqParamTypeEnum.chatId, "691762017@chat.agent" }, { IcqParamTypeEnum.text, "- @[247319424]" } };
            //ConcurrentDictionary<ParamTypeEnum, string> parameter2 = new ConcurrentDictionary<ParamTypeEnum, string>(sendMessageParameter);

            //if (commandFactory.CreateCommand((ApiCommandEnum)IcqApiCommandEnum.SendText, HttpMethodEnum.Get, parameter: parameter2) is IBotCommand botcommand4)
            //    if (botcommand4 != null) commandFactory.TryAddCommandToQueue(botcommand4);



            //var test = commandFactory.TryRunQueue();
            //Console.ReadLine();
            //commandFactory.TryAddCommandToQueue(commandFactory.CreateCommand(ApiCommandEnum.SelfGet));
            //Dictionary<ParamTypeEnum, string> testxxxsdf = new Dictionary<ParamTypeEnum, string>() { { ParamTypeEnum.lastEventId, "1" }, { ParamTypeEnum.pollTime, "5" } };

            //ConcurrentDictionary<ParamTypeEnum, string> parametersdf = new ConcurrentDictionary<ParamTypeEnum, string>(testxxxsdf);
            //commandFactory.TryAddCommandToQueue(commandFactory.CreateCommand(ApiCommandEnum.GetEvents, parameter: parametersdf));

            //Dictionary<ParamTypeEnum, string> sendMessageParameter = new Dictionary<ParamTypeEnum, string>() { { ParamTypeEnum.chatId, "691762017@chat.agent" }, { ParamTypeEnum.text, "- @[247319424]" } };

            //commandFactory.TryAddCommandToQueue(commandFactory.CreateCommand(ApiCommandEnum.SendText, HTTPMethodEnum.Get, sendMessageParameter.ToConcurrentDictionary()));

            cfg.CommandList = commandFactory.CommandQueue.Cast<CommandTemplate>().ToList();
            cfg.Save(cfg.SettingConfig.PathToThisConfig.TrimEnd('\\')+ '\\' + "demoCfg.xml");
            bool yeah = commandFactory.TryRunFullQueue();
            //var test = bot.BotResponseFactory.ResponseBag;
            Console.WriteLine(yeah.ToString());


            //Console.WriteLine(bot.BotResponseFactory.ResponseBag.ToString());
            Console.ReadLine();
        }
    }
}
