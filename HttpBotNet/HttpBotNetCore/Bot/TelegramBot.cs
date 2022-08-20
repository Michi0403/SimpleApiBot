using BotNetCore.BusinessObjects;
using BotNetCore.EventArgs;
using BotNetCore.Factories;
using BotNetCore.Helper;
using BotNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BotNetCore.Extensions;
using System.IO;
using BotNetCore.BusinessObjects.Responses;
using BotNetCore.Abstract;
using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;

namespace BotNetCore.Bot
{
    /// <summary>
    /// Initialize with for example serialized Config
    /// 
    /// This Bot uses HTTPCLIENT AND IS NOT DISPOSEABLE, Never create 2 Instances on Runtime.
    /// 
    /// </summary>
    public class TelegramBot : IBot
    {
        static object _lock1 = new object();
        static object _lock2 = new object();
        private HttpClient _httpClient;
        private HttpClientHandler _handler;
        private Config _config;
        private TelegramCommandFactory _commandfactory;
        private TelegramResponseFactory _responsefactory;
        private LoggingHandler _loggingHandler;
        /// <summary>
        /// Get HttpClientref of TelegramBot
        /// </summary>
        public HttpClient HttpClient => _httpClient;
        /// <summary>
        /// Get HttpClientHandlerref of TelegramBot
        /// </summary>
        public HttpClientHandler HttpClientHandler => _handler;
        /// <summary>
        /// Get Config for Telegram Bot
        /// </summary>
        public Config Config => _config;
        /// <summary>
        /// Get Set Telegram command Factory for Telegram Bot
        /// </summary>
        public IBotCommandFactoryTemplate BotCommandFactory { get => _commandfactory; set { _commandfactory = (TelegramCommandFactory)value; } }
        /// <summary>
        /// Get Set Telegram Response Factory for Telegram Bot
        /// </summary>
        public ResponseFactoryTemplate BotResponseFactory { get => _responsefactory; set => _responsefactory = (TelegramResponseFactory)value; }
        /// <summary>
        /// Get Token of this Telegram Bot
        /// </summary>
        public string Token => _config.SettingConfig.Token;
        /// <summary>
        /// Property LoggingHandler ref of Telegram Bot
        /// </summary>
        public LoggingHandler LoggingHandler => _loggingHandler;


        /// <summary>
        /// Running Twice will only change the config and DefaultRequestHeaders
        /// </summary>
        /// <param name="config">IcqBotNetCore.BusinessObjects a config builded </param>
        public void Initialize(Config config)
        {
            Monitor.TryEnter(_lock1);
            try
            {
                Monitor.TryEnter(_lock2);

                if (_httpClient == null)
                {
                        if(_config != null && config != null)
                            _config = config;
                    else if(_config == null && config != null)
                            _config = config;
                    else if(_config == null) _config = new Config();
                    
                    _handler = new HttpClientHandler();

                    _handler.ClientCertificates.Add(new X509Certificate2(@$"{    _config.SettingConfig.PathToCert.TrimEnd('\\') + '\\' + _config.SettingConfig.CertFileName + ".pfx"}",
                                                                                config.SettingConfig.PasswordForPK,
                                                                                X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet));
                    _handler.SslProtocols = SslProtocols.Tls13 | SslProtocols.Tls12 | SslProtocols.None | SslProtocols.Tls;
                    _handler.ClientCertificateOptions = ClientCertificateOption.Automatic;

                    _loggingHandler = new LoggingHandler(_handler);

                    _loggingHandler.OnRequestContentAvailable += RequestContentIncoming;
                    _loggingHandler.OnResponseContentAvailable += ResponseContentIncoming;
                    _loggingHandler.OnRequestAvailable += RequestIncoming;

                    _httpClient = new HttpClient(_loggingHandler);
                    _httpClient.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                    _httpClient.DefaultRequestHeaders.Accept.Clear();
                    foreach (string acceptedHeader in _config.SettingConfig.AcceptedHeader)
                    {
                        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptedHeader));
                    }
                    _httpClient.BaseAddress = new Uri(@$"{_config.SettingConfig.ApiRoute}");
                    _commandfactory = new TelegramCommandFactory(_httpClient, _config.SettingConfig.Token);
                    _responsefactory = new TelegramResponseFactory();
                    
                }
                else if (config != null)
                {
                    _loggingHandler.OnRequestAvailable -= RequestIncoming;
                    _loggingHandler.OnRequestContentAvailable -= RequestContentIncoming;
                    _loggingHandler.OnResponseContentAvailable -= ResponseContentIncoming;
                    _loggingHandler.OnRequestAvailable += RequestIncoming;
                    _loggingHandler.OnRequestContentAvailable += RequestContentIncoming;
                    _loggingHandler.OnResponseContentAvailable += ResponseContentIncoming;

                    _config = config;
                    _httpClient.DefaultRequestHeaders.Accept.Clear();
                    foreach (string acceptedHeader in _config.SettingConfig.AcceptedHeader)
                    {
                        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptedHeader));
                    }
                    _httpClient.BaseAddress = new Uri(@$"{_config.SettingConfig.ApiRoute}");
                    if (_commandfactory == null || (_commandfactory.HttpClient == null | _commandfactory.Token == null))
                        _commandfactory.RefreshHttpClientRefAndToken(this._httpClient, this._config.SettingConfig.Token);
                }
                else throw new Exception("Whatever happened here is not intended, ask the guru for Error 1337");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //throw ex;
            }
            finally
            {
                Monitor.Exit(_lock1);
                Monitor.Exit(_lock2);
            }
        }

        /// <summary>
        /// Process all Commands stored in this Factory
        /// </summary>
        /// <returns>success/fail bool</returns>
        public bool ProcessCommands()
        {
            try
            {
                return _commandfactory.TryRunFullQueue();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// HttpResponse Content incoming -> Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">HttpRequest Content for Event</param>
        public void ResponseContentIncoming(object sender, GenericEventArgs<string, byte[]> e)
        {
            try
            {
                if(e!=null& !string.IsNullOrWhiteSpace( e.EventData))
                {
                    string fullpath = @$"{Config.SettingConfig.PathForHttpData}".TrimEnd('\\') + '\\' + Path.GetRandomFileName() + DateTime.Now.ToLongTimeString().Replace(" ", "_").Replace(":", "")+ ".json";
                    e.EventData.Save(fullpath);
                    var responseContentTask = StringExtension.DeserializeByteArrayToJSONDocumentAsync(e.EventData2);
                    int counter = 0;
                    while (!responseContentTask.IsCompleted)
                    {
                        counter++;
                        responseContentTask.Wait(1);
                        if (counter % 5000 == 0)
                        {
                            throw new TimeoutException("Deserializing failed of " + e.EventData2.ToString());
                        }
                    }
                    var responseContent = responseContentTask.Result;

                    _responsefactory.TryAddResponseToBag( _responsefactory.CreateResponseFromRequestAndJson(e.EventData, responseContent));
                    //DynamicResponse ResponseAsList = new DynamicResponse();
                    //foreach(var param in Enum.GetValues( typeof(ParamTypeResponeEnum)))
                    //{
                        //var singleParam = StringExtension.DeserializeAnonymousType(e.EventData, typeof(DynamicResponse));
                    //}
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// HttpRequest Content incoming -> Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">HttpRequest Content for Event</param>
        public void RequestContentIncoming(object sender, GenericEventArgs<string> e)
        {
            try
            {
                if (e!=null && !string.IsNullOrWhiteSpace(e.EventData))
                {
                    string fullpath = @$"{Config.SettingConfig.PathForHttpData}" + @$"\httpData\RequestContent" + Path.GetRandomFileName() + DateTime.Now.ToLongTimeString().Replace(" ", "_").Replace(":", "") + ".json";
                    e.EventData.Save(fullpath);
                    //var test = StringExtension.DeserializeAnonymousType(e.EventData,new { test = "" }).test;
                 
                }
                    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// HttpRequest incoming -> Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">HttpResponse Content for Event</param>
        public void RequestIncoming(object sender, GenericEventArgs<HttpRequestMessage> e)
        {
            try
            {
                if (e!= null && e.EventData != null)
                {
                    //string fullpath = @$"{Config.SettingConfig.PathForHttpData}" + @$"\httpData\Request" + Path.GetRandomFileName() + DateTime.Now.ToLongTimeString().Replace(" ", "_").Replace(":", "") + ".json";
                    //var test = StringExtension.DeserializeAnonymousType(e.EventData,new { test = "" }).test;
                    var requestTask = StringExtension.DeserializeToJSONDocumentAsync(e.EventData);
                    var request = requestTask.Result;

                    _responsefactory.FeedRequestDictionary(request,e.EventData.GetHashCode().ToString());

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

