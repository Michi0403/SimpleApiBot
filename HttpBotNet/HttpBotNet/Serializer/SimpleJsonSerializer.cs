using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
using BotNetCore.Factories;
using BotNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HttpBotNet.Serializer
{
    public class SimpleJsonSerializer : IJSONSerializer
    {
        

        public string Unescape(string json)
        {
            string unescaped = Regex.Unescape(@json);
            return unescaped;
        }

        public T Deserialize<T>(string json, T anonymousTypeObject, JsonSerializerOptions options = default)
        {
            try
            {
                

                return JsonSerializer.Deserialize<T>(json, options);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return default(T);
            }
        }

        public Task SerializeAsync<TValue>(Stream stream, TValue anonymousTypeObject, JsonSerializerOptions options = default, CancellationToken cancellationToken = default)
        {

            return JsonSerializer.SerializeAsync<TValue>(stream, anonymousTypeObject,options);
        }

        public string Serialize<T>( T anonymousTypeObject, JsonSerializerOptions options = default)
        {
            return JsonSerializer.Serialize<T>(anonymousTypeObject, options);
        }

        public ValueTask<TValue> DeserializeAsync<TValue>(Stream stream, TValue anonymousTypeObject, JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            return  JsonSerializer.DeserializeAsync<TValue>(stream, options, cancellationToken);
        }

        public async Task<JsonDocument> DeserializeToJSONDocumentAsync<T>( T anonymousType , JsonDocumentOptions options = default, CancellationToken cancellationToken = default)
        {
            Byte[] JsonToByteArray = JsonSerializer.SerializeToUtf8Bytes<T>(anonymousType);
            using(MemoryStream stream = new MemoryStream(JsonToByteArray))
                return await JsonDocument.ParseAsync(stream, options, cancellationToken);
        }

        public async Task<JsonDocument> DeserializeByteArrayToJSONDocumentAsync(byte[] json, JsonDocumentOptions options = default, CancellationToken cancellationToken = default)
        {
            using (MemoryStream stream = new MemoryStream(json))
                return await JsonDocument.ParseAsync(stream, options, cancellationToken);
        }

        public async Task<Dictionary<IcqParamTypeEnum, string>> GetJsonEntries(string toJson)
        {
            var options = new JsonSerializerOptions();
            DictionaryTKeyEnumTValueConverter converterFactory = new DictionaryTKeyEnumTValueConverter();

            //options.Converters.Add(converterFactory.CreateConverter(typeof(Dictionary<ParamTypeEnum,string>),options));
            var json = JsonSerializer.SerializeToUtf8Bytes(Unescape(toJson));
            using (MemoryStream stream = new MemoryStream(json))
                return await JsonSerializer.DeserializeAsync<Dictionary<IcqParamTypeEnum, string>>(stream,options);
        }

        public T SerializeToJsonDeserializeToAnonym<T>(T anonymousTypeObject, Type goalType , JsonSerializerOptions options = null)
        {
            var json = Regex.Unescape( Serialize<T>(anonymousTypeObject, options));

            


            return Deserialize<T>(json, (T)Convert.ChangeType(goalType, typeof(T)), options);
        }

    }
}
