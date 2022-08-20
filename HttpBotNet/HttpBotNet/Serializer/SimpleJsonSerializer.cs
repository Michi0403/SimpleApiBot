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
    /// <summary>
    /// Simple JSON Serializer, not rly needed.
    /// </summary>
    public class SimpleJsonSerializer : IJSONSerializer
    {
        
        /// <summary>
        /// Can be used to unescape json strings
        /// </summary>
        /// <param name="json"></param>
        /// <returns>hopefully unescaped json strings</returns>
        public string Unescape(string json)
        {
            try
            {
                string unescaped = Regex.Unescape(@json);
                return unescaped;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unescaping JSON failed");
                Console.WriteLine(ex.ToString());
                return string.Empty;
            }
            
        }
        /// <summary>
        /// Deserialize jsonstring to typeof anonymousTypeObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">jsonString</param>
        /// <param name="anonymousTypeObject">Type to serialize to</param>
        /// <param name="options">JsonSerializer Options, optional</param>
        /// <returns>JsonValue</returns>
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
        /// <summary>
        /// SerializeAsync
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="stream"></param>
        /// <param name="anonymousTypeObject"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SerializeAsync<TValue>(Stream stream, TValue anonymousTypeObject, JsonSerializerOptions options = default, CancellationToken cancellationToken = default)
        {

            return JsonSerializer.SerializeAsync<TValue>(stream, anonymousTypeObject, options, cancellationToken);
        }
        /// <summary>
        /// Serialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="anonymousTypeObject"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public string Serialize<T>( T anonymousTypeObject, JsonSerializerOptions options = default)
        {
            return JsonSerializer.Serialize<T>(anonymousTypeObject, options);
        }
        /// <summary>
        /// Deserialize Async
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="stream"></param>
        /// <param name="anonymousTypeObject"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public ValueTask<TValue> DeserializeAsync<TValue>(Stream stream, TValue anonymousTypeObject, JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
        {
            return  JsonSerializer.DeserializeAsync<TValue>(stream, options, cancellationToken);
        }
        /// <summary>
        /// Deserialize to JSON-Doc Async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="anonymousType"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<JsonDocument> DeserializeToJSONDocumentAsync<T>( T anonymousType , JsonDocumentOptions options = default, CancellationToken cancellationToken = default)
        {
            Byte[] JsonToByteArray = JsonSerializer.SerializeToUtf8Bytes<T>(anonymousType);
            using(MemoryStream stream = new MemoryStream(JsonToByteArray))

            return await JsonDocument.ParseAsync(stream, options, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Deserialize Byte Array to JSON-Doc Async
        /// </summary>
        /// <param name="json"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<JsonDocument> DeserializeByteArrayToJSONDocumentAsync(byte[] json, JsonDocumentOptions options = default, CancellationToken cancellationToken = default)
        {
            using (MemoryStream stream = new MemoryStream(json))
                return await JsonDocument.ParseAsync(stream, options, cancellationToken).ConfigureAwait(false);
        }
        /// <summary>
        /// Get all Json Entries not really used 
        /// </summary>
        /// <param name="toJson"></param>
        /// <returns></returns>

        public async Task<Dictionary<IcqParamTypeEnum, string>> GetJsonEntries(string toJson)
        {
            try
            {
                var options = new JsonSerializerOptions();
                DictionaryTKeyEnumTValueConverter converterFactory = new DictionaryTKeyEnumTValueConverter();

                //options.Converters.Add(converterFactory.CreateConverter(typeof(Dictionary<ParamTypeEnum,string>),options));
                var json = JsonSerializer.SerializeToUtf8Bytes(Unescape(toJson));
                using (MemoryStream stream = new MemoryStream(json))
                    return await JsonSerializer.DeserializeAsync<Dictionary<IcqParamTypeEnum, string>>(stream, options).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get Json Entries failes");
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// Serialize to JSon and deserialize output to Anonym Type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="anonymousTypeObject"></param>
        /// <param name="goalType"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public T SerializeToJsonDeserializeToAnonym<T>(T anonymousTypeObject, Type goalType , JsonSerializerOptions options = null)
        {
            try
            {
                var json = Regex.Unescape(Serialize<T>(anonymousTypeObject, options));
                return Deserialize<T>(json, (T)Convert.ChangeType(goalType, typeof(T)), options);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SerializeToJsonDeserializeToAnonym failed");
                    Console.WriteLine(ex.ToString());
                return default(T);
            }
            
        }

    }
}
