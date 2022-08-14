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
    [DataContract]
    public abstract class ResponseFactoryTemplate
    {
        [DataMember]
        public ConcurrentBag<IBotResponse> ResponseBag { get; set; } = new ConcurrentBag<IBotResponse>();


        public IJSONSerializer serializer { get; set; } = StringExtension.Singleton.Instance.Serializer;

        [DataMember]
        public ConcurrentDictionary<string, ParamTypeEnumComposite> RequestDictionary = new ConcurrentDictionary<string, ParamTypeEnumComposite>();

        public ResponseFactoryTemplate()
        {
        }

        public IBotResponse CreateResponseFromJson(JsonDocument request, JsonDocument response)
        {

            ParamTypeEnumComposite requestContent = ParseJsonDocument(request);
            ParamTypeEnumComposite responseContent = ParseJsonDocument(response);

            IBotResponse returnRespond = new Response(requestContent, responseContent);

            return returnRespond;

        }

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
                Console.WriteLine(ex.ToString());
                return string.Empty;
            }
        }

        public ParamTypeEnumComposite GetListOfContainedParams(IBotResponse response,IcqParamTypeEnum param)
        {
            try
            {
                ParamTypeEnumComposite returnList = null;

                //TODO Overwork
                Console.WriteLine("Whatever is written here doesn't work right now");

                    //foreach(var list in response.Request.Intersect(response.Response) )
                    //{
                    //   returnList.AddRange((List<(IcqParamTypeEnum, object)>)list.Where(x => (IcqParamTypeEnum)x.Item1== param).Select(y => y));
                //}
                //return returnList;
                return returnList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public IBotResponse CreateResponseFromRequestAndJson(string requestHash, JsonDocument response, ParamTypeEnum objectFirstParam)
        {
            try
            {
                ParamTypeEnumComposite request = null;
                if( RequestDictionary.TryGetValue(requestHash, out ParamTypeEnumComposite value))
                {
                    request = value;
                }
                ParamTypeEnumComposite responseContent = ParseJsonDocument(response, objectFirstParam);

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
        /// <param name="objectFirstParam"></param>
        /// maybe unnecessary who knows
        /// <returns></returns>
        private ParamTypeEnumComposite ParseJsonDocument(JsonDocument jsonDocument, ParamTypeEnum paramTypeEnum = null)
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
                                    Console.WriteLine($"ParamTypeEnum {paramTypeEnum} not found! Skip");
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
