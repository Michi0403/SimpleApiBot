using BotNetCore.Helper;
using BotNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BotNetCore.Extensions
{
    public static class StringExtension
    {
        public partial class Singleton
        {
            public static Singleton Instance { get; set; } = new Singleton();
            private Singleton()
            {
            }
            public IJSONSerializer Serializer { get; set; }
        }

        public static async void Save(this string data, string path)
        {
            try
            {
                GeneralHelper.TryCreateDirectoryForThisFile(path);
                if (!File.Exists(path))
                    CreateNew(path);
                using FileStream stream = new FileStream(path, FileMode.Open);
                {
                    IJSONSerializer serializer = Singleton.Instance.Serializer;
                    await serializer.SerializeAsync(stream, serializer.Unescape(data));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Saving failed...");
                Console.WriteLine(ex.ToString());
            }
        }

        private static void CreateNew(string path)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                var data = string.Empty;
                using FileStream stream = File.Create(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Create New failed");
                Console.WriteLine(ex.ToString());
            }
        }

        public static string Unescape(string jsonString)
        {
            try
            {
                IJSONSerializer serializer = Singleton.Instance.Serializer;
                return serializer.Unescape(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unescaping string failed");
                Console.WriteLine(ex.ToString());
                return string.Empty;
            }
            
        }
        public static async Task<JsonDocument> DeserializeToJSONDocumentAsync<T>(T anonymousTypeObject)
        {
            try
            {
                IJSONSerializer serializer = Singleton.Instance.Serializer;
                return await serializer.DeserializeToJSONDocumentAsync(anonymousTypeObject);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Deserialize to JSON Document Async failed");
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public static async Task<JsonDocument> DeserializeByteArrayToJSONDocumentAsync(byte[] json)
        {
            try
            {
                IJSONSerializer serializer = Singleton.Instance.Serializer;
                return await serializer.DeserializeByteArrayToJSONDocumentAsync(json: json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Deserialize ByteArray to JSON Document Async failed");
                Console.WriteLine(ex.ToString());
                return null;
            }
        }


        public static T DeserializeAnonymousType<T>(T anonymousTypeObject, Type goalType, JsonSerializerOptions options = default)
        {
            try
            {
                IJSONSerializer serializer = Singleton.Instance.Serializer;
                return serializer.SerializeToJsonDeserializeToAnonym(anonymousTypeObject, goalType);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Deserialize Anonym Object to goalType failed");
                Console.WriteLine(ex.ToString());
                return default(T);
            }
        }

    }
}
