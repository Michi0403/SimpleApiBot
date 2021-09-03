using IcqBotNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IcqBotNetCore.BusinessObjects.Responses;
using System.Text.Json;
using IcqBotNetCore.BusinessObjects.Commands;
using System.Collections.Concurrent;
using System.Runtime.Serialization;
using IcqBotNetCore.Extensions;

namespace IcqBotNetCore.Abstract
{
    [DataContract]
    public abstract class ResponseFactoryTemplate
    {
        [DataMember]
        public ConcurrentBag<IBotResponse> ResponseBag { get; set; } = new ConcurrentBag<IBotResponse>();


        public IJSONSerializer serializer { get; set; } = StringExtension.Singleton.Instance.Serializer;

        [DataMember]
        public ConcurrentDictionary<string, List<List<(ParamTypeEnum, object)>>> RequestDictionary = new ConcurrentDictionary<string, List<List<(ParamTypeEnum, object)>>>();

        public ResponseFactoryTemplate()
        {
        }

        public IBotResponse CreateResponseFromJson(JsonDocument request, JsonDocument response)
        {

            List<List<(ParamTypeEnum, object)>> requestContent = ParseJsonDocument(request);
            List<List<(ParamTypeEnum, object)>> responseContent = ParseJsonDocument(response);

            IBotResponse returnRespond = new Response(requestContent, responseContent);

            return returnRespond;

        }

        public string FeedRequestDictionary(JsonDocument request, string requestHash)
        {
            try
            {
                List<List<(ParamTypeEnum, object)>> requestContent = ParseJsonDocument(request);
                RequestDictionary.TryAdd(requestHash, requestContent);
                return requestHash;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return string.Empty;
            }
        }

        public List<(ParamTypeEnum,object)> GetListOfContainedParams(IBotResponse response,ParamTypeEnum param)
        {
            try
            {
                List<(ParamTypeEnum, object)> returnList = new List<(ParamTypeEnum, object)>();

                foreach(var list in response.Request.Intersect(response.Response) )
                {
                    returnList.AddRange(list.Where(x => x.Item1 == param).Select(y => y));
                }
                return returnList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<(ParamTypeEnum, object)>();
            }
        }

        public IBotResponse CreateResponseFromRequestAndJson(string requestHash, JsonDocument response, ParamTypeEnum objectFirstParam)
        {
            try
            {
                List<List<(ParamTypeEnum, object)>> request = new List<List<(ParamTypeEnum, object)>>();
                if( RequestDictionary.TryGetValue(requestHash, out List<List<(ParamTypeEnum, object)>>  value))
                {
                    request = value;
                }
                List<List<(ParamTypeEnum, object)>> responseContent = ParseJsonDocument(response, objectFirstParam);

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
        private List<List<(ParamTypeEnum, object)>> ParseJsonDocument(JsonDocument jsonDocument, ParamTypeEnum objectFirstParam = ParamTypeEnum.notDefined)
        {
            try
            {
                List<List<(ParamTypeEnum, object)>> keyValuePairs = new List<List<(ParamTypeEnum, object)>>();

                JsonElement root = jsonDocument.RootElement;
                Enumerate(root);


                return keyValuePairs;

                
                void Enumerate(JsonElement elementToProcess, List<(ParamTypeEnum,object)> nestedList = null, bool nestedCall=false)
                {
                    /*
                     * Step one Add all properties from elementToProcess to keyValuePairs BUT ONLY like Paramtype Events, list of events && Paramtype Ok, true
                     * If it's a nested call it will be NEVER necessary to add to keyValuePairs because -> You would add the inner Objects and Arrays to the outer Objects and Arrays
                     * If you have a Param Like Events (Array) it contains a list of objects, for useful approch List of Events, Should contain a List of the nested Objects
                     * The nested Objects should contain in a Own list. If a Paramtype is a Object, Array it should Containt like ParamType Events a List which contains all properties (nested lists and so on) 
                     * 
                     * What i've learned -> in All "Root Element" seems to be an Object???
                     * 
                     * Example JSON Starts with Objects Events and a simple Param with int or whatever. This contents have to be in the Outer List
                     */
                    List<(ParamTypeEnum, object)> objectList = new List<(ParamTypeEnum, object)>();
                    try
                    {
                        switch (elementToProcess.ValueKind)
                        {
                            case JsonValueKind.Undefined:
                                break;
                            case JsonValueKind.Object:
                                try
                                {
                                    
                                    foreach (JsonProperty item in elementToProcess.EnumerateObject())
                                    {


                                        //the Objects I tested contained Objects, Properties and Arrays
  
                                        //Does the Object Containts Arrays or Objects?????
                                        switch (item.Value.ValueKind)
                                        {

                                            case JsonValueKind.Object:
                                                //If Object or Array we have to enumerate it and put the values to a list
                                                //Add Object / Array To List as Entry? Uff
                                                if(nestedList == null)
                                                    nestedList = new List<(ParamTypeEnum, object)>();

                                                Enumerate(item.Value, nestedList, true);

                                                if(nestedList.Count>0)
                                                {
                                                    if (Enum.IsDefined(typeof(ParamTypeEnum), item.Name))
                                                    {
                                                        if (!objectList.Contains(new(Enum.Parse<ParamTypeEnum>(item.Name), nestedList)))
                                                        {
                                                            objectList.Add(new(Enum.Parse<ParamTypeEnum>(item.Name), nestedList));
                                                        }
                                                    }
                                                    else
                                                        if (!objectList.Contains(new(ParamTypeEnum.notDefined, nestedList)))
                                                        {
                                                            objectList.Add(new(ParamTypeEnum.notDefined, nestedList));
                                                        }
                                                }
                                                break;

                                            case JsonValueKind.Array:
                                                break;
                                            case JsonValueKind.Undefined:
                                            case JsonValueKind.String:
                                            case JsonValueKind.Number:
                                            case JsonValueKind.True:
                                            case JsonValueKind.False:
                                            case JsonValueKind.Null:
                                                if (Enum.IsDefined(typeof(ParamTypeEnum), item.Name))
                                                {
                                                    if (!objectList.Contains(new(Enum.Parse<ParamTypeEnum>(item.Name), item.Value.GetRawText())))
                                                    {
                                                        objectList.Add(new(Enum.Parse<ParamTypeEnum>(item.Name), item.Value.GetRawText()));
                                                    }
                                                }  
                                                else
                                                    if (!objectList.Contains(new(ParamTypeEnum.notDefined, item.Value.GetRawText())))
                                                    {
                                                        objectList.Add(new(ParamTypeEnum.notDefined, item.Value.GetRawText()));
                                                    }
                                                break;
                                            default:
                                                break;
                                        }
                                    }

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.ToString());
                                }


                                break;
                            case JsonValueKind.Array:
                                foreach(JsonElement element in elementToProcess.EnumerateArray())
                                {
                                    // The kind of Arrays I tested contained Objects and Properties
                                    try
                                    {
                                        //Does the Array Contains Arrays or Objects????? 
                                        switch (element.ValueKind)
                                        {
                                            case JsonValueKind.Object:
                                            case JsonValueKind.Array:

                                                Console.WriteLine(elementToProcess.ToString());
                                                Console.WriteLine(element.ToString());

                                                if(nestedList == null)
                                                    nestedList = new List<(ParamTypeEnum, object)>();

                                                Enumerate(element, nestedList, true);

                                                if (nestedList.Count > 0)
                                                {

                                                    if (!objectList.Contains(new(ParamTypeEnum.NamelessArray, nestedList)))
                                                    {
                                                        //objectList.Add(new(ParamTypeEnum.NamelessArray, nestedList));
                                                    }
                                                }
                                                //if (nestedCall)
                                                //{
                                                //Enumerate(element, nestedList, true);
                                                //}
                                                //else
                                                //{

                                                //}

                                                break;
                                            case JsonValueKind.Undefined:
                                                //Can be shitdata/faulty data so... only in an object.
                                                break;
                                            case JsonValueKind.String:
                                            case JsonValueKind.Number:
                                            case JsonValueKind.True:
                                            case JsonValueKind.False:
                                            case JsonValueKind.Null:

                                                //if(nestedCall)
                                                //{
                                                Enumerate(element, nestedList, true);
                                                //}
                                                //else
                                                //{

                                                //}

                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.ToString());
                                    }
                                }
                                break;
                            case JsonValueKind.String:
                                break;
                            case JsonValueKind.Number:
                                break;
                            case JsonValueKind.True:
                                break;
                            case JsonValueKind.False:
                                break;
                            case JsonValueKind.Null:
                                break;
                            default:
                                break;
                        }
                        //add every object to keyvaluepairs or list depends on what is what and where you are
                        if (!nestedCall)
                        {
                            keyValuePairs.Add(objectList);
                        }
                        else
                        {
                            nestedList.AddRange(objectList);
                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    //if(element.ValueKind == JsonValueKind.Object)
                    //{

                    //    foreach (JsonProperty item in element.EnumerateObject())
                    //    {
                    //        if (Enum.IsDefined(typeof(ParamTypeEnum), item.Name))
                    //        {
                    //            if (Enum.Equals(Enum.Parse<ParamTypeEnum>(item.Name), objectFirstParam))
                    //            {
                    //                if (objectList != null)
                    //                {
                    //                    if (!keyValuePairs.Contains(objectList))
                    //                        if (!nestedCall)
                    //                            keyValuePairs.Add(objectList);
                    //                }
                    //                else
                    //                {

                    //                    objectList = new List<(ParamTypeEnum, object)>();
                    //                }
                    //            }


                    //            if (item.Value.ValueKind != JsonValueKind.Object && item.Value.ValueKind != JsonValueKind.Array)
                    //            {
                    //                if(objectList == null)
                    //                    objectList = new List<(ParamTypeEnum, object)>();
                    //                if (!objectList.Contains(new(Enum.Parse<ParamTypeEnum>(item.Name), item.Value.GetRawText())))
                    //                    objectList.Add(new(Enum.Parse<ParamTypeEnum>(item.Name), item.Value.GetRawText()));
                    //            }


                    //            if (item.Value.ValueKind == JsonValueKind.Object || item.Value.ValueKind == JsonValueKind.Array)
                    //            {
                    //                //if (!objectList.Contains(new(Enum.Parse<ParamTypeEnum>(item.Name), item.Value)))
                    //                //    objectList.Add(new(Enum.Parse<ParamTypeEnum>(item.Name), item.Value.GetRawText()));
                    //                List<(ParamTypeEnum, object)> nestedObjectList = new List<(ParamTypeEnum, object)>();
                    //                if(nestedCall)
                    //                    Enumerate(item.Value, nestedObjectList, nestedCall:true);
                    //                else
                    //                    Enumerate(item.Value, nestedObjectList, nestedCall:false);
                    //                if (objectList == null)
                    //                    objectList = new List<(ParamTypeEnum, object)>();

                    //                if (nestedCall && objectList.Count > 0 || nestedObjectList.Count> 0)
                    //                {
                    //                    nestedObjectList = objectList.Intersect(nestedObjectList).ToList();
                    //                }
                    //                if(nestedObjectList.Count > 0)
                    //                    if (!objectList.Contains(new(Enum.Parse<ParamTypeEnum>(item.Name), nestedObjectList)))
                    //                        objectList.Add(new(Enum.Parse<ParamTypeEnum>(item.Name), (object)nestedObjectList));

                    //            }


                    //        }
                    //    }
                    //    if(!keyValuePairs.Contains(objectList))
                    //        if(!nestedCall)
                    //            keyValuePairs.Add(objectList);


                    //} else if (element.ValueKind == JsonValueKind.Array)
                    //{
                    //    var test = element.EnumerateArray();

                    //    do
                    //    {
                    //        if(test.Current.ValueKind == JsonValueKind.Object)
                    //        {
                    //            List<(ParamTypeEnum, object)> nestedObjectList = new List<(ParamTypeEnum, object)>();

                    //            Enumerate(test.Current , nestedObjectList, nestedCall);

                    //            if (nestedObjectList.Count > 0)
                    //                if (!objectList.Contains(new(objectFirstParam, nestedObjectList)))
                    //                    objectList.Add(new(objectFirstParam, (object)nestedObjectList));

                    //        }
                    //        else if (test.Current.ValueKind == JsonValueKind.Array)
                    //        {
                    //            Enumerate(test.Current, objectList, nestedCall);
                    //        }
                    //        else if (test.Current.ValueKind != JsonValueKind.Object && test.Current.ValueKind != JsonValueKind.Array && test.Current.ValueKind != JsonValueKind.Undefined)
                    //        {
                    //            //var test3 = Enum.GetValues(typeof(ParamTypeEnum));
                    //            //foreach (var bla in test3)
                    //            //{
                    //            //    var testxxxxxxx = test.Current.GetProperty(Enum.GetName(typeof(ParamTypeEnum), bla));
                    //            //    Console.WriteLine(testxxxxxxx.GetRawText());



                    //            //}

                    //            //var property = test.Current.GetProperty
                    //        }
                    //        else if (test.Current.ValueKind == JsonValueKind.Undefined)
                    //        {
                    //            //.WriteLine(test.Current.GetRawText());


                    //        }
                    //        else if (test.Current.ValueKind != JsonValueKind.Undefined)
                    //        {
                    //            Console.WriteLine(test.Current.ValueKind);
                    //        }

                    //    } while (test.MoveNext());

                    //}
                    //else if (element.ValueKind == JsonValueKind.String)
                    //{
                    //  //  var test = element.GetString();
                    //    //foreach (var item in element.EnumerateArray())
                    //    //{
                    //    // //   keyValuePairs.Add(new(Enum.Parse<ParamTypeEnum>(item.), item.Value.GetRawText()));
                    //    //}
                    //        //string elementAsString = element.GetString();
                    //        //var jsonEntriesTask = serializer.GetJsonEntries(elementAsString);
                    //        //var jsonEntries = jsonEntriesTask.Result;

                    //        //foreach(var entry in jsonEntries)
                    //        //{
                    //        //    Console.WriteLine(entry.toString());
                    //        //}
                    //        //foreach(var entry in jsonEntries)
                    //        //{
                    //        //    if(Enum.IsDefined(typeof(ParamTypeEnum), entry.Key))
                    //        //    {
                    //        //        keyValuePairs.Add(new(Enum.Parse<ParamTypeEnum>(entry.Key), entry.Value));
                    //        //    }
                    //        //}
                    //    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<List<(ParamTypeEnum, object)>>();
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
