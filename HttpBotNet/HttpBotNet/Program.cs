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
using BotNetCore.BusinessObjects.Responses;
using System.Threading;
using BotNetCore.BusinessObjects.Responses.ResponeComposite;
using System.Reflection;
using System.ComponentModel;

namespace HttpBotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            //Parallel.Invoke(() => LetIcqBotDoStuff(),() => LetTelegramBotDoStuff());
            //CertificateUtil.MakeCert(Path.GetPathRoot(Assembly.GetExecutingAssembly().Location) + @"\CertForBuilding\","SnkToBuildSigned","p@ssw()rt");
            //Task icqBotTask = Task.Run(() => LetIcqBotDoStuff());
            //Console.WriteLine("Started an icq bot");
            Task telegramBotTask = Task.Run(() => LetTelegramBotDoStuff());
            Console.WriteLine("Started a telegram bot");
            //Task.WaitAll(new Task[] { icqBotTask, telegramBotTask });
            Task.WaitAll(new Task[] { telegramBotTask });
            Console.WriteLine("Command Line Implementation ended here");
        }

        private static void LetTelegramBotDoStuff()
        {
            try
            {
                IDataFileExtension.Singleton.Instance.Serializer = new SimpleDataContractSerializer();
                StringExtension.Singleton.Instance.Serializer = new SimpleJsonSerializer();
                Config telegramCfg = new Config();
                telegramCfg.SettingConfig.Nick = "@iLikeToFartBot";
                telegramCfg.SettingConfig.PathToThisConfig= Directory.GetCurrentDirectory().TrimEnd('\\')+ '\\'+"telegramConfig.xml";
                telegramCfg.SettingConfig.PathToCert = telegramCfg.SettingConfig.PathToCert.TrimEnd('\\') + '\\' + "telegramCert\\";
                telegramCfg.SettingConfig.CertFileName = "TelegramBotXCertFile";
                telegramCfg.SettingConfig.PathForHttpData= telegramCfg.SettingConfig.PathForHttpData.TrimEnd('\\') + '\\';
                telegramCfg.SettingConfig.ApiRoute = @"https://api.telegram.org/";
                File.Delete(telegramCfg.SettingConfig.PathToThisConfig);
                telegramCfg.Save(telegramCfg.SettingConfig.PathToThisConfig);
                if (!File.Exists(@$"{telegramCfg.SettingConfig.PathToCert.TrimEnd('\\') + '\\' + telegramCfg.SettingConfig.CertFileName + ".pfx"}"))
                {
                    Console.WriteLine("Create and Store SSL Certificate");
                    CertificateUtil.MakeCert(telegramCfg.SettingConfig.PathToCert, telegramCfg.SettingConfig.CertFileName, telegramCfg.SettingConfig.PasswordForPK);
                    CertificateUtil.SaveCertForUser(telegramCfg.SettingConfig.PathToCert, telegramCfg.SettingConfig.CertFileName, telegramCfg.SettingConfig.PasswordForPK);
                }
                telegramCfg.CommandList.Clear();

                var bot = new TelegramBot();
                bot.Initialize(telegramCfg);
                telegramCfg.CommandList.Clear();

                var commandFactory = bot.BotCommandFactory;

                //Dictionary<ParamTypeEnum, string> testxxx = new Dictionary<ParamTypeEnum, string>() { };
                //ConcurrentDictionary<ParamTypeEnum, string> parameter = new ConcurrentDictionary<ParamTypeEnum, string>(testxxx);
                //if (commandFactory.CreateCommand((ApiCommandEnum)TelegramApiCommandEnum.getMe, HttpMethodEnum.Get, parameter: parameter) is IBotCommand botcommand2)
                //    commandFactory.TryAddCommandToQueue(botcommand2);

                //Dictionary<ParamTypeEnum, string> sendmessagetest = new Dictionary<ParamTypeEnum, string>(){ { TelegramParamTypeEnum.chat_id, "@Michael Flesh" }, { TelegramParamTypeEnum.text, "Haha" } };
                //ConcurrentDictionary<ParamTypeEnum, string> paramsendmessage = new ConcurrentDictionary<ParamTypeEnum, string>(sendmessagetest);
                //if (commandFactory.CreateCommand((ApiCommandEnum)TelegramApiCommandEnum.sendMessage, HttpMethodEnum.Get, parameter: parameter) is IBotCommand botcommand3)
                //    commandFactory.TryAddCommandToQueue(botcommand3);

                Dictionary<ParamTypeEnum, string> testgetupdates = new Dictionary<ParamTypeEnum, string>() {  };
                ConcurrentDictionary<ParamTypeEnum, string> getupdatesparams = new ConcurrentDictionary<ParamTypeEnum, string>(testgetupdates);
                if (commandFactory.CreateCommand((ApiCommandEnum)TelegramApiCommandEnum.getUpdates, HttpMethodEnum.Get, parameter: getupdatesparams) is IBotCommand botcommand4)
                    commandFactory.TryAddCommandToQueue(botcommand4 );

                telegramCfg.Save(telegramCfg.SettingConfig.PathToThisConfig.TrimEnd('\\'));
                bool yeah = commandFactory.TryRunFullQueue(true);

                Thread.Sleep(10000);
                Console.WriteLine(yeah.ToString());

                ConcurrentBag<IBotResponse> botResponseBag = new ConcurrentBag<IBotResponse>();
                botResponseBag = bot.BotResponseFactory.ResponseBag;

                if(botResponseBag != null)
                {
                    for (int counter = 0; counter < botResponseBag.Count; counter++)
                    {
                        IBotResponse response = null;
                        botResponseBag.TryTake(out response);
                        var response2 = response as Response;
                        if (response2 != null)
                            response2.Save(@$"{telegramCfg.SettingConfig.PathForHttpData}" + @$"ResponseFinal_" + Path.GetRandomFileName() + "_" + DateTime.Now.ToLongTimeString().Replace(" ", "_").Replace(":", "") + ".xml");

                    }
                }

                //Test for deserialization of responses
                 List<IBotResponse> deserializedResponses = new List<IBotResponse>();
                    GeneralHelper.TryCreateDirectoryForThisFile(
                    telegramCfg.SettingConfig.PathForHttpData.TrimEnd('\\') + '\\');
                var httpresponsesAndSoOnWhatever = Directory.GetFiles(@$"{telegramCfg.SettingConfig.PathForHttpData.TrimEnd('\\')}" + '\\',"*.xml");
                foreach (string pathOfFile in httpresponsesAndSoOnWhatever)
                {
                    try
                    {
                        Response response = new Response();
                        deserializedResponses.Add(response.Load(pathOfFile));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }

                Console.WriteLine("Deserialized : " + deserializedResponses.Count + " Responses");

                Console.WriteLine("Seek for /IchBraucheAufmerksamkeit");
                List<ComponentParam> ListOfMessages = new List<ComponentParam>();
                foreach(IBotResponse messagesInResponse in deserializedResponses)
                {
                    //Look in Responses for something todo
                    Console.WriteLine("Inspect Responses for specific Composites");
                    var kiddies = messagesInResponse.Response.ReturnValue();

                    foreach(var entryttt in kiddies)
                    {
                        try
                        {
                            Console.WriteLine("Value paramTypeEnum " + entryttt.paramTypeEnum.Value);
                            Console.WriteLine("Value telegramParamtypeEnum " + TelegramParamTypeEnum.message.ToString());
                            if (entryttt.paramTypeEnum.Value.ToString() == TelegramParamTypeEnum.message.ToString())
                            {
                                ListOfMessages.Add(entryttt);
                            }
                        }
                        catch (Exception ex)
                        {
                                Console.WriteLine("Error in adding message");
                                Console.WriteLine(ex.ToString());
                        }
                           
                    }

                }

                foreach(ComponentParam foundMessage in ListOfMessages)
                {
                    Console.WriteLine("Found Message");
                    if (foundMessage is ParamTypeEnumComposite paramTypeEnumComposite)
                    {
                        Console.WriteLine("Is Composite");
                        foreach(ComponentParam param in paramTypeEnumComposite.ReturnValue())
                        {
                            if(param is ParamTypeEnumComposite param2)
                            {
                                Console.WriteLine("Is Inner Composite");
                                Console.WriteLine("---param in ListOfMessages inner Composite---");
                                Console.WriteLine($@"   Param: " + param.paramTypeEnum.Value);
                                Console.WriteLine($@"   Value: " + param.value);
                            }
                            Console.WriteLine("---param in ListOfMessages Composite---");
                            Console.WriteLine($@"   Param: " + param.paramTypeEnum.Value);
                            Console.WriteLine($@"   Value: " + param.value);
                        }
                    }
                    else if(foundMessage is ParamTypeEnumLeaf paramTypeEnumLeaf)
                    {
                        Console.WriteLine("---ParamTypeEnumLeaf in ListOfMessages---");
                        Console.WriteLine($@"   Param: " + foundMessage.paramTypeEnum.Value);
                        Console.WriteLine($@"   Value: " + foundMessage.value);
                    }
                }

                Console.WriteLine("Collect Information of /IchBraucheAufmerksamkeit messages and filter for");

                //foreach(var entry in ListOfMessages.Where(x => ))

                Console.WriteLine("Telegram Implementation ended here");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Telegram Bot Failed");
                Console.WriteLine(ex.Message);
            }
        }

        private static void LetIcqBotDoStuff()
        {
            try
            {
                IDataFileExtension.Singleton.Instance.Serializer = new SimpleDataContractSerializer();
                StringExtension.Singleton.Instance.Serializer = new SimpleJsonSerializer();
                Config cfg = new Config();
                cfg = cfg.Load(cfg.SettingConfig.PathToThisConfig);

                if (!File.Exists(@$"{cfg.SettingConfig.PathToCert.TrimEnd('\\') + '\\' + cfg.SettingConfig.CertFileName + ".pfx"}"))
                {
                    Console.WriteLine("Create and Store SSL Certificate");
                    CertificateUtil.MakeCert(cfg.SettingConfig.PathToCert, cfg.SettingConfig.CertFileName, cfg.SettingConfig.PasswordForPK);
                    CertificateUtil.SaveCertForUser(cfg.SettingConfig.PathToCert, cfg.SettingConfig.CertFileName, cfg.SettingConfig.PasswordForPK);
                }
                cfg.CommandList.Clear();

                var bot = new IcqBot();
                bot.Initialize(cfg);
                cfg.CommandList.Clear();
                var commandFactory = bot.BotCommandFactory;

                Dictionary<ParamTypeEnum, string> test2 = new Dictionary<ParamTypeEnum, string>() { { IcqParamTypeEnum.lastEventId, "7" }, { IcqParamTypeEnum.pollTime, "60" } };
                ConcurrentDictionary<ParamTypeEnum, string> parameter23 = new ConcurrentDictionary<ParamTypeEnum, string>(test2);
                if (commandFactory.CreateCommand((ApiCommandEnum)IcqApiCommandEnum.GetEvents, HttpMethodEnum.Get, parameter: parameter23) is IBotCommand botcommand3)
                    commandFactory.TryAddCommandToQueue(botcommand3);

                Dictionary<ParamTypeEnum, string> testxxx = new Dictionary<ParamTypeEnum, string>() { };
                ConcurrentDictionary<ParamTypeEnum, string> parameter = new ConcurrentDictionary<ParamTypeEnum, string>(testxxx);
                if (commandFactory.CreateCommand((ApiCommandEnum)IcqApiCommandEnum.SelfGet, HttpMethodEnum.Get, parameter: parameter) is IBotCommand botcommand2)
                    commandFactory.TryAddCommandToQueue(botcommand2);

                Dictionary<ParamTypeEnum, string> sendMessageParameter = new Dictionary<ParamTypeEnum, string>() { { IcqParamTypeEnum.chatId, "691762017@chat.agent" }, { IcqParamTypeEnum.text, "- @[247319424]" } };
                ConcurrentDictionary<ParamTypeEnum, string> parameter2 = new ConcurrentDictionary<ParamTypeEnum, string>(sendMessageParameter);

                if (commandFactory.CreateCommand((ApiCommandEnum)IcqApiCommandEnum.SendText, HttpMethodEnum.Get, parameter: parameter2) is IBotCommand botcommand4)
                    commandFactory.TryAddCommandToQueue(botcommand4);

                cfg.Save(cfg.SettingConfig.PathToThisConfig.TrimEnd('\\'));
                bool yeah = commandFactory.TryRunFullQueue(true);

                Thread.Sleep(10000);
                Console.WriteLine(yeah.ToString());
                ConcurrentBag<IBotResponse> responseBag = new ConcurrentBag<IBotResponse>();
                responseBag = bot.BotResponseFactory.ResponseBag;
                for (int counter = 0; counter < responseBag.Count; counter++)
                {
                    IBotResponse response = null;
                    responseBag?.TryTake(out response);
                    var response2 = response as Response;
                    if (response2 != null)
                        response2.Save(@$"{cfg.SettingConfig.PathForHttpData}" + @$"ResponseFinal_" + Path.GetRandomFileName() + "_" + DateTime.Now.ToLongTimeString().Replace(" ", "_").Replace(":", "") + ".xml");

                }

                //Test for deserialization of responses
                List<IBotResponse> deserializedResponses = new List<IBotResponse>();
                var httpresponsesAndSoOnWhatever = Directory.GetFiles(@$"{cfg.SettingConfig.PathForHttpData}", "*.xml");
                foreach (string pathOfFile in httpresponsesAndSoOnWhatever)
                {
                    try
                    {
                        Response response = new Response();
                        deserializedResponses.Add(response.Load(pathOfFile));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }


                Console.WriteLine("IcqBot Implementation ended here");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Icq Bot Failed");
                Console.WriteLine(ex.Message);
            }
 
        }
    }
}
