using BotNetCore.BusinessObjects.Commands;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace BotNetCore.Factories
{
    /// <summary>
    /// copypasted from 
    /// https://docs.microsoft.com/de-de/dotnet/standard/serialization/system-text-json-converters-how-to?pivots=dotnet-5-0
    /// All rights to ms for this code
    /// </summary>


    public class DictionaryTKeyEnumTValueConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (!typeToConvert.IsGenericType)
            {
                return false;
            }

            if (typeToConvert.GetGenericTypeDefinition() != typeof(Dictionary<,>))
            {
                return false;
            }

            return typeToConvert.GetGenericArguments()[0].IsEnum;
        }

        public override JsonConverter CreateConverter(
            Type type,
            JsonSerializerOptions options)
        {
            Type keyType = type.GetGenericArguments()[0];
            Type valueType = type.GetGenericArguments()[1];
            Type value = type.GetGenericArguments()[2];

            JsonConverter converter = (JsonConverter)Activator.CreateInstance(
                typeof(ListEnumConverterInner<>).MakeGenericType(
                    new Type[] { keyType, valueType , value}),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                args: new object[] { options },
                culture: null);

            return converter;
        }

        private class ListEnumConverterInner<T> : JsonConverter<List<List<T>>>
        {
            private readonly JsonConverter<T> _valueConverter;
            private readonly Type _keyType;
            private readonly Type _valueType;
            private readonly List<string> _paramsToSkip;
            private readonly string _firstElementOfNestedClass;
            public ListEnumConverterInner(JsonSerializerOptions options, List<string> paramToSkip, string firstElementOfNestedClass)
            {
                // For performance, use the existing converter if available.
                _valueConverter = (JsonConverter<T>)options
                    .GetConverter(typeof(T));

                // Cache the key and value types.
                _keyType = typeof(T);

                _paramsToSkip = paramToSkip;
                _firstElementOfNestedClass = firstElementOfNestedClass;
                
            }

            public override List<List<T>> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var resultList = new List<List<T>>();
                
                if(reader.TokenType == JsonTokenType.String)
                {
                    var jsonstring = reader.GetString();

                    Regex valueCollection = new Regex("\"(?<Key>[\\w]*)\":\"?(?<Value>([\\s\\w\\d\\.\\\\\\-/:_\\+]+(,[,\\s\\w\\d\\.\\\\\\-/:_\\+]*)?)*)\"?");
                    MatchCollection mc = valueCollection.Matches(jsonstring);

                    var tmpList = new List<List<T>>();
                    foreach (Match k in mc)
                    {
                        if (_paramsToSkip.Contains(k.Name))
                        {

                        }
                        else if(k.Name == _firstElementOfNestedClass)
                        {
                            
                        }
                        else
                        {
                           // tmpList
                        }
                    }

                }
                return resultList;
            }

            public override void Write(Utf8JsonWriter writer, List<List<T>> value, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }
        }
        private class DictionaryEnumConverterInner<TKey,TValue> :
            JsonConverter<Dictionary<TKey, TValue>> where TKey : struct, Enum
        {
            private readonly JsonConverter<TValue> _valueConverter;
            private readonly Type _keyType;
            private readonly Type _valueType;

            public DictionaryEnumConverterInner(JsonSerializerOptions options)
            {
                // For performance, use the existing converter if available.
                _valueConverter = (JsonConverter<TValue>)options
                    .GetConverter(typeof(TValue));

                // Cache the key and value types.
                _keyType = typeof(TKey);
                _valueType = typeof(TValue);
            }

            public override Dictionary<TKey, TValue> Read(
                ref Utf8JsonReader reader,
                Type typeToConvert,
                JsonSerializerOptions options)
            {
             
                var dictionary = new Dictionary<TKey, TValue>();

                if (reader.TokenType != JsonTokenType.StartObject & reader.TokenType != JsonTokenType.String)
                {
                    throw new JsonException();
                }

                if (reader.TokenType == JsonTokenType.String)
                {
                    var jsonstring = reader.GetString();

                    Regex r = new Regex("\"(?<Key>[\\w]*)\":\"?(?<Value>([\\s\\w\\d\\.\\\\\\-/:_\\+]+(,[,\\s\\w\\d\\.\\\\\\-/:_\\+]*)?)*)\"?");
                    MatchCollection mc = r.Matches(jsonstring);

                 
                    foreach (Match k in mc)
                    {
                        if (!Enum.TryParse(k.Name, ignoreCase: false, out TKey key) &&
                            !Enum.TryParse(k.Name, ignoreCase: true, out key))
                        {
                            throw new JsonException(
                                $"Unable to convert \"{k.Name}\" to Enum \"{_keyType}\".");
                        }

                        // Get the value.
                //TValue value;
                //if (_valueConverter != null)
                //{
                //    reader.Read();
                //    value = _valueConverter.Read(ref reader, _valueType, options);
                //}
                //else
                //{
                //    value = JsonSerializer.Deserialize<TValue>(, options);
                //}

                //        //dictionary.Add(k.Groups["Key"].Value, k.Groups["Value"].Value);

                    }
                }
                return dictionary;
                //while (reader.Read())
                //{

                //    if (reader.TokenType == JsonTokenType.EndObject)
                //    {
                //        return dictionary;
                //    }

                //    // Get the key.
                //    if (reader.TokenType != JsonTokenType.PropertyName)
                //    {
                //        throw new JsonException();
                //    }

                //    string propertyName = reader.GetString();

                //    // For performance, parse with ignoreCase:false first.
                //    if (!Enum.TryParse(propertyName, ignoreCase: false, out TKey key) &&
                //        !Enum.TryParse(propertyName, ignoreCase: true, out key))
                //    {
                //        throw new JsonException(
                //            $"Unable to convert \"{propertyName}\" to Enum \"{_keyType}\".");
                //    }

                

                //    // Add to dictionary.
                //    dictionary.Add(key, value);
                //}

                //throw new JsonException();
            }

            public override void Write(
                Utf8JsonWriter writer,
                Dictionary<TKey, TValue> dictionary,
                JsonSerializerOptions options)
            {
                writer.WriteStartObject();

                foreach ((TKey key, TValue value) in dictionary)
                {
                    var propertyName = key.ToString();
                    writer.WritePropertyName
                        (options.PropertyNamingPolicy?.ConvertName(propertyName) ?? propertyName);

                    if (_valueConverter != null)
                    {
                        _valueConverter.Write(writer, value, options);
                    }
                    else
                    {
                        JsonSerializer.Serialize(writer, value, options);
                    }
                }

                writer.WriteEndObject();
            }
        }
    }
    
}
