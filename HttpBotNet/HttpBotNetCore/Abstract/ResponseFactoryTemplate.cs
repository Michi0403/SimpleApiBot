using BotNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotNetCore.BusinessObjects.Responses;
using System.Text.Json;
using System.Collections.Concurrent;
using System.Runtime.Serialization;
using BotNetCore.Extensions;
using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
using BotNetCore.BusinessObjects.Enums;
using BotNetCore.BusinessObjects.Responses.ResponeComposite;
using System.Reflection;

namespace BotNetCore.Abstract
{
    /// <summary>
    /// ResponseFactory Template
    /// </summary>
    [DataContract]
    public abstract class ResponseFactoryTemplate
    {
        /// <summary>
        /// Concurrent Bag which can save objects of type IBotResponse
        /// </summary>
        [DataMember]
        public ConcurrentBag<IBotResponse> ResponseBag { get; set; } = new ConcurrentBag<IBotResponse>();

        /// <summary>
        /// IJSonSerializer to serialize JSON's (maybe not used anymore) //TODO
        /// </summary>
        public IJSONSerializer serializer { get; set; } = StringExtension.Singleton.Instance.Serializer;

        /// <summary>
        /// ConcurrentDictionary to store Request data to map for example responses to them
        /// </summary>
        [DataMember]
        public ConcurrentDictionary<string, ParamTypeEnumComposite> RequestDictionary = new ConcurrentDictionary<string, ParamTypeEnumComposite>();
        /// <summary>
        /// Default Constructor, don't use it just for serialization intends
        /// </summary>
        public ResponseFactoryTemplate()
        {
        }
        /// <summary>
        /// Try to create a Response from JSON Document from HTTPRequest and HTTPResponse
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <returns>IBotResponse</returns>
        public IBotResponse CreateResponseFromJson(JsonDocument request, JsonDocument response)
        {

            ParamTypeEnumComposite requestContent = ParseJsonDocument(request);
            ParamTypeEnumComposite responseContent = ParseJsonDocument(response);

            IBotResponse returnResponse = new Response(requestContent, responseContent);

            return returnResponse;

        }
        /// <summary>
        /// Feeds the Request Dictionary with a jsondocument of the request and a hash of the request
        /// </summary>
        /// <param name="request">JsonDocument of Request, parse HTTPRequest to JsonDocument</param>
        /// <param name="requestHash">hash of Request to map later responses to this request</param>
        /// <returns>Request Hash</returns>
        public string FeedRequestDictionary(JsonDocument request, string requestHash)
        {
            try
            {
                ParamTypeEnumComposite requestContent = ParseJsonDocument(request);
                RequestDictionary.TryAdd(requestHash, requestContent);
                return requestHash;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Feed RequestDictionary failed");
                Console.WriteLine(ex.ToString());
                return string.Empty;
            }
        }
        /// <summary>
        /// Tries to Create a Response from a Request in RequestDictionary(by hash) and a JSon Document
        /// </summary>
        /// <param name="requestHash">Hash of the request in RequestDictionary</param>
        /// <param name="response"></param>
        /// <returns></returns>
        public IBotResponse CreateResponseFromRequestAndJson(string requestHash, JsonDocument response)
        {
            try
            {
                ParamTypeEnumComposite request = null;
                if( RequestDictionary.TryGetValue(requestHash, out ParamTypeEnumComposite value))
                {
                    request = value;
                }
                ParamTypeEnumComposite responseContent = ParseJsonDocument(response);

                IBotResponse returnRespond = new Response(request, responseContent);
                return returnRespond;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Contains a very lazy recursive function embedded, just warnin
        /// </summary>
        /// <param name="jsonDocument"></param>
        /// the Jsondocument from which we need the params+values from
        /// maybe unnecessary who knows
        /// <returns></returns>
        protected virtual ParamTypeEnumComposite ParseJsonDocument(JsonDocument jsonDocument)
        {
            try
            {
                List<List<(ParamTypeEnum, object)>> keyValuePairs = new List<List<(ParamTypeEnum, object)>>();
                ParamTypeEnumComposite rootTrunk = new ParamTypeEnumComposite(ParamTypeEnum.isRootTrunk,"Start Trunk of JSON Document!");
                JsonElement root = jsonDocument.RootElement;
                Enumerate(root, rootTrunk);

                return rootTrunk;

                
                void Enumerate(JsonElement elementToProcess, ParamTypeEnumComposite actualOrNewComposite)
                {
                    ParamTypeEnumComposite paramTypeEnumComposite = actualOrNewComposite;
                    switch ((elementToProcess.ValueKind))
                    {
                        case JsonValueKind.Undefined:
                            break;
                        case JsonValueKind.Object:
                            ParamTypeEnumComposite compositeObject = new ParamTypeEnumComposite(
                            actualOrNewComposite.paramTypeEnum,
                            elementToProcess.GetRawText());
                            foreach(JsonProperty jsonProperty in elementToProcess.EnumerateObject())
                            {
                                Enumerate(jsonProperty.Value, compositeObject);

                                Console.WriteLine("ValueMember; Try to find IcqParamTypeEnum with name of :" + jsonProperty.Name);
                                IcqParamTypeEnum param = null;

                                try
                                {
                                        param = (IcqParamTypeEnum)typeof(IcqParamTypeEnum).GetField(
                                        jsonProperty.Name,
                                        BindingFlags.Instance |
                                            BindingFlags.Static |
                                            BindingFlags.Public |
                                            BindingFlags.NonPublic)
                                        .GetValue(null);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"IcqParamTypeEnum {jsonProperty.Name} not found: {ex}");
                                }

                                if (param != null)
                                {
                                    ParamTypeEnumLeaf leaf = new ParamTypeEnumLeaf(param, jsonProperty.Value.ToString());
                                    compositeObject.Add(leaf);
                                    Console.WriteLine("Found: " + jsonProperty.Name);
                                }
                                else
                                {
                                    Console.WriteLine($"ParamTypeEnum {jsonProperty.Name} not found! Skip");
                                }

                            }
                            actualOrNewComposite.Add(compositeObject);
                            break;
                        case JsonValueKind.Array:
                            ParamTypeEnumComposite compositeObject2 = new ParamTypeEnumComposite(
                                ParamTypeEnum.IsArray,
                                elementToProcess.GetRawText());
                            foreach(var jsonElement in elementToProcess.EnumerateArray())
                            {
                                Enumerate(jsonElement, compositeObject2);
                            }
                            actualOrNewComposite.Add(compositeObject2);
                            break;

                        case JsonValueKind.String:
                        case JsonValueKind.Number:
                        case JsonValueKind.True:
                        case JsonValueKind.False:
                        case JsonValueKind.Null:
                            ParamTypeEnumLeaf leaf2 = new ParamTypeEnumLeaf(actualOrNewComposite.paramTypeEnum, elementToProcess.GetRawText());
                                actualOrNewComposite.Add((leaf2));
                            break;
                        default:
                            break;
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            finally
            {
                jsonDocument.Dispose();
            }
        }
        /// <summary>
        /// Try add this IBotResponse to ResponseBag of this Factory
        /// </summary>
        /// <param name="iBotRespond">iBotRespond to store in Bag</param>
        /// <returns>success/fail bool</returns>
        public virtual bool TryAddResponseToBag(IBotResponse iBotRespond)
        {
            try
            {
                ResponseBag.Add(iBotRespond);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
