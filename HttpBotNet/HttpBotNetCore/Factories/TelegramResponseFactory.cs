using BotNetCore.Abstract;
using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
using BotNetCore.BusinessObjects.Responses.ResponeComposite;
using BotNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BotNetCore.Factories
{
    public class TelegramResponseFactory : ResponseFactoryTemplate
    {
        public TelegramResponseFactory() :   base()
        {
        }

        /// <summary>
        /// Contains a very lazy recursive function embedded, just warnin
        /// </summary>
        /// <param name="jsonDocument"></param>
        /// the Jsondocument from which we need the params+values from
        /// <param name="objectFirstParam"></param>
        /// maybe unnecessary who knows
        /// <returns></returns>
        protected override ParamTypeEnumComposite ParseJsonDocument(JsonDocument jsonDocument)
        {
            try
            {
                List<List<(ParamTypeEnum, object)>> keyValuePairs = new List<List<(ParamTypeEnum, object)>>();
                ParamTypeEnumComposite rootTrunk = new ParamTypeEnumComposite(ParamTypeEnum.isRootTrunk, "Start Trunk of JSON Document!");
                JsonElement root = jsonDocument.RootElement;
                try
                {
                    while (Monitor.IsEntered(_lock1)) { Monitor.Wait(_lock1); }
                    Monitor.TryEnter(_lock1);
                    while (Monitor.IsEntered(_lock2)) { Monitor.Wait(_lock2); }
                    Monitor.TryEnter(_lock2);
                    Enumerate(root, rootTrunk);
                    Console.WriteLine("Parsing JSON Document was a success " + rootTrunk.ToString());
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Locking Enumeration not worked");
                    Console.WriteLine(ex.ToString());

                }
                finally
                {
                    if(Monitor.LockContentionCount > 0)
                    {
                        Monitor.Pulse(_lock1);
                    }
                    Monitor.Exit(_lock1);

                    if(Monitor.LockContentionCount > 0)
                    {
                        Monitor.Pulse(_lock2);
                    }
                    Monitor.Exit(_lock2);
                }

                return rootTrunk;


               
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

        private static bool EnumerateObject(JsonElement elementToProcess, ParamTypeEnumComposite paramTypeEnumComposite)
        {
            try
            {
                TelegramParamTypeEnum paramObject = null;
                ParamTypeEnumComposite iAmTheObject = null;

                try
                {
                    paramObject = (TelegramParamTypeEnum)typeof(TelegramParamTypeEnum).GetField(
                                 elementToProcess.ValueKind.ToString(),
                                 BindingFlags.Instance |
                                     BindingFlags.Static |
                                     BindingFlags.Public |
                                     BindingFlags.NonPublic)
                                 .GetValue(null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"TelegramParamTypeEnum {elementToProcess.ValueKind.ToString()} not found: {ex}");
                }
                finally
                {
                    paramObject = paramTypeEnumComposite.paramTypeEnum as TelegramParamTypeEnum;
                }
                iAmTheObject = new ParamTypeEnumComposite(paramObject,elementToProcess.GetRawText());

                foreach (JsonProperty jsonProperty in elementToProcess.EnumerateObject())
                {
                    TelegramParamTypeEnum param = null;
                    ParamTypeEnumLeaf leaf = null;
                    ParamTypeEnumComposite compositeLeaf = null;

                    try
                    {
                        param = (TelegramParamTypeEnum)typeof(TelegramParamTypeEnum).GetField(
                        jsonProperty.Name,
                        BindingFlags.Instance |
                            BindingFlags.Static |
                            BindingFlags.Public |
                            BindingFlags.NonPublic)
                        .GetValue(null);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"TelegramParamTypeEnum {jsonProperty.Name} not found: {ex}");
                    }

                    switch (jsonProperty.Value.ValueKind)
                    {

                        case JsonValueKind.Object:
                        case JsonValueKind.Array:
                            compositeLeaf = new ParamTypeEnumComposite(param, jsonProperty.Value.ToString());
                            Enumerate(jsonProperty.Value, compositeLeaf);
                            break;

                        case JsonValueKind.Undefined:
                            Console.WriteLine("Valuekind of currentELement is undefined!");
                            try
                            {
                                Console.WriteLine("Undefined Element:" + Environment.NewLine + jsonProperty.ToString());
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Couldn't resolve value");
                                Console.WriteLine(ex.ToString());
                            }

                            leaf = new ParamTypeEnumLeaf(TelegramParamTypeEnum.undefined, "null");
                            break;
                        case JsonValueKind.String:
                        case JsonValueKind.Number:
                        case JsonValueKind.True:
                        case JsonValueKind.False:
                        case JsonValueKind.Null:
                            leaf = new ParamTypeEnumLeaf(param, jsonProperty.Value.ToString());
                            break;
                        default:
                            break;
                    }
                    if (compositeLeaf != null)
                        iAmTheObject.Add(compositeLeaf);
                    if (leaf != null)
                        iAmTheObject.Add(leaf);

                }

                paramTypeEnumComposite.Add(iAmTheObject); 
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Enumeration of Object failed");
                Console.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                Console.WriteLine("Enumeration of Object finished");
            }
        }

        private static bool Enumerate(JsonElement elementToProcess, ParamTypeEnumComposite actualOrNewComposite)
        {
            try
            {
                ParamTypeEnumComposite paramTypeEnumComposite = actualOrNewComposite;
                switch ((elementToProcess.ValueKind))
                {
                    case JsonValueKind.Undefined:
                        Console.WriteLine("Valuekind of currentELement is undefined!");
                        try
                        {
                            Console.WriteLine("Undefined Element:" + Environment.NewLine + paramTypeEnumComposite.ToString());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Couldn't resolve value");
                            Console.WriteLine(ex.ToString());
                        }

                        paramTypeEnumComposite.Add( new ParamTypeEnumLeaf(TelegramParamTypeEnum.undefined, "null"));
                        break;
                    case JsonValueKind.Object:

                        EnumerateObject(elementToProcess, paramTypeEnumComposite);

                        break;
                    case JsonValueKind.Array:
                        for (int i = 0; i < elementToProcess.GetArrayLength(); i++)
                        {
                            TelegramParamTypeEnum paramArray = null;
                            ParamTypeEnumLeaf leafArray = null;
                            ParamTypeEnumComposite compositeLeafArray = null;


                            JsonElement currentElement = elementToProcess[i];
                            switch (currentElement.ValueKind)
                            {

                                case JsonValueKind.Object:
                                    compositeLeafArray = new ParamTypeEnumComposite(paramTypeEnumComposite.paramTypeEnum, currentElement.GetRawText());
                                    EnumerateObject(currentElement, actualOrNewComposite);

                                    break;
                                case JsonValueKind.Array:
                                    compositeLeafArray = new ParamTypeEnumComposite(paramTypeEnumComposite.paramTypeEnum, currentElement.GetRawText());
                                    Enumerate(currentElement, compositeLeafArray);
                                    break;

                                case JsonValueKind.Undefined:
                                    Console.WriteLine("Valuekind of currentELement is undefined!");
                                    try
                                    {
                                        Console.WriteLine("Undefined Element:" + Environment.NewLine + currentElement.ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Couldn't resolve value");
                                        Console.WriteLine(ex.ToString());
                                    }
                                    paramTypeEnumComposite.Add(new ParamTypeEnumLeaf(TelegramParamTypeEnum.undefined, "null"));
                                    break;


                                case JsonValueKind.String:
                                case JsonValueKind.Number:
                                case JsonValueKind.True:
                                case JsonValueKind.False:
                                case JsonValueKind.Null:
                                    TelegramParamTypeEnum paramValue = paramTypeEnumComposite.paramTypeEnum as TelegramParamTypeEnum;
                                    leafArray = new ParamTypeEnumLeaf(paramValue, currentElement.GetRawText());
                                    break;
                                default:
                                    break;
                            }
                            if (compositeLeafArray != null)
                                paramTypeEnumComposite.Add(compositeLeafArray);
                            if (leafArray != null)
                                paramTypeEnumComposite.Add(leafArray);


                        }
                        break;
                    case JsonValueKind.String:
                    case JsonValueKind.Number:
                    case JsonValueKind.True:
                    case JsonValueKind.False:
                    case JsonValueKind.Null:
                        TelegramParamTypeEnum paramTypeEnum = null;
                        try
                        {
                            paramTypeEnum = (TelegramParamTypeEnum)typeof(TelegramParamTypeEnum).GetField(
                            elementToProcess.ValueKind.ToString(),
                            BindingFlags.Instance |
                                BindingFlags.Static |
                                BindingFlags.Public |
                                BindingFlags.NonPublic)
                            .GetValue(null);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"TelegramParamTypeEnum {elementToProcess.ToString()} not found: {ex}");
                        }
                        if(paramTypeEnum == null) paramTypeEnum = paramTypeEnumComposite.paramTypeEnum as TelegramParamTypeEnum;
                        ParamTypeEnumLeaf leaf2 = new ParamTypeEnumLeaf(paramTypeEnum, elementToProcess.GetRawText());
                        actualOrNewComposite.Add((leaf2));
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Enumeration of " + elementToProcess.GetRawText() + " failed");
                Console.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                Console.WriteLine("Enumeration is finished");
            }
        }
    }
}
