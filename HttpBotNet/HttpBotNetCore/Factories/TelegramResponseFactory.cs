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
                            foreach (JsonProperty jsonProperty in elementToProcess.EnumerateObject())
                            {
                                Enumerate(jsonProperty.Value, compositeObject);

                                Console.WriteLine("ValueMember; Try to find TelegramParamTypeEnum with name of :" + jsonProperty.Name);
                                TelegramParamTypeEnum param = null;

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
                            TelegramParamTypeEnum paramArray = TelegramParamTypeEnum.IsArray as TelegramParamTypeEnum;
                            try
                            {
                                paramArray = TelegramParamTypeEnum.IsArray as TelegramParamTypeEnum;
                                try
                                {
                                    paramArray = (TelegramParamTypeEnum)typeof(TelegramParamTypeEnum).GetField(
                                                elementToProcess.ValueKind.ToString(),
                                                BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                                                .GetValue(null);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"TelegramParamTypeEnum {elementToProcess.ValueKind.ToString()} not found: {ex}");
                                }

                                
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Could not Resolve Array name");
                                Console.WriteLine(ex.ToString());
                            }
                            ParamTypeEnumComposite compositeObject2 = null;
                            try
                            {
                                compositeObject2 = new ParamTypeEnumComposite(
                                paramArray,
                                elementToProcess.GetRawText());
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Could not Create ParamTypeComposite");
                                Console.WriteLine(ex.ToString());
                            }

                            foreach (var jsonElement in elementToProcess.EnumerateArray())
                            {
                                if(jsonElement.ValueKind == JsonValueKind.Array || jsonElement.ValueKind == JsonValueKind.Object || jsonElement.ValueKind == JsonValueKind.Undefined)
                                    Enumerate(jsonElement, compositeObject2);
                                else
                                {
                                    actualOrNewComposite.Add(new ParamTypeEnumLeaf(actualOrNewComposite.paramTypeEnum, jsonElement.GetRawText()));
                                }
                            }
                            actualOrNewComposite.Add(compositeObject2);
                            break;

                        case JsonValueKind.String:
                        case JsonValueKind.Number:
                        case JsonValueKind.True:
                        case JsonValueKind.False:
                        case JsonValueKind.Null:
                            ParamTypeEnumLeaf leaf2 = new ParamTypeEnumLeaf(paramTypeEnumComposite.paramTypeEnum, elementToProcess.GetRawText());
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
    }
}
