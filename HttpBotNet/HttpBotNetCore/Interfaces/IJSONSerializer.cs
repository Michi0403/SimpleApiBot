using BotNetCore.BusinessObjects.Enums.ApiCommandEnums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BotNetCore.Interfaces
{
    public interface IJSONSerializer
    {
        public T Deserialize<T>(string json, T anonymousTypeObject, JsonSerializerOptions options = default);
        public ValueTask<TValue> DeserializeAsync<TValue>(Stream stream, TValue anonymousTypeObject, JsonSerializerOptions options = default, CancellationToken cancellationToken = default);
        public string Serialize<T>(T anonymousTypeObject, JsonSerializerOptions options = default);
        public Task SerializeAsync<TValue>(Stream stream, TValue anonymousTypeObject, JsonSerializerOptions options = default, CancellationToken cancellationToken = default);
        public T SerializeToJsonDeserializeToAnonym<T>(T anonymousTypeObject, Type goalType, JsonSerializerOptions options = default);
        public Task<JsonDocument> DeserializeToJSONDocumentAsync<T>(T anonymousType, JsonDocumentOptions options = default, CancellationToken cancellationToken = default);
        public Task<JsonDocument> DeserializeByteArrayToJSONDocumentAsync(byte[] json, JsonDocumentOptions options = default, CancellationToken cancellationToken = default);
        public Task<Dictionary<IcqParamTypeEnum, string>> GetJsonEntries(string toJson);
        public string Unescape(string json);
    }
}
