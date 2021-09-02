using IcqBotNetCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IcqBotNetCore.Extensions
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
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            if (!File.Exists(path))
                CreateNew(path);
            using FileStream stream = new FileStream(path, FileMode.Open);
            {
                IJSONSerializer serializer = Singleton.Instance.Serializer;
                await serializer.SerializeAsync(stream, serializer.Unescape(data));
            }
        }

        //public static async ValueTask<T> AnonymousDeserialize<T>(this object data, T anonymousTypeObject, string path)
        //{
        //    Directory.CreateDirectory(Path.GetDirectoryName(path));
            
        //    if (!File.Exists(path))
        //        CreateNew(path);
        //    using FileStream stream = new FileStream(path, FileMode.Open);
        //    {
        //        IJSONSerializer serializer = Singleton.Instance.Serializer;
        //        return await serializer.DeserializeAsync(stream,anonymousTypeObject);
        //    }
        //}

        private static void CreateNew(string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            var data = string.Empty;
            using FileStream stream = File.Create(path);
        }

        public static string Unescape(string jsonString)
        {
            IJSONSerializer serializer = Singleton.Instance.Serializer;
            return serializer.Unescape(jsonString);
        }

        //public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        //{
        //    IJSONSerializer serializer = Singleton.Instance.Serializer;
        //    return serializer.Deserialize<T>(json, anonymousTypeObject);
        //}

        public static async Task<JsonDocument> DeserializeToJSONDocumentAsync<T>(T anonymousTypeObject)
        {
            IJSONSerializer serializer = Singleton.Instance.Serializer;
            return await serializer.DeserializeToJSONDocumentAsync(anonymousTypeObject);
        }

        public static async Task<JsonDocument> DeserializeByteArrayToJSONDocumentAsync(byte[] json)
        {
            IJSONSerializer serializer = Singleton.Instance.Serializer;
            return await serializer.DeserializeByteArrayToJSONDocumentAsync(json: json);
        }


        public static T DeserializeAnonymousType<T>(T anonymousTypeObject, Type goalType, JsonSerializerOptions options = default)
        {
            IJSONSerializer serializer = Singleton.Instance.Serializer;
            return serializer.SerializeToJsonDeserializeToAnonym(anonymousTypeObject, goalType);
        }

        //public static byte[] ObjectToByteArray(Object obj)
        //{
        //    //BinaryFormatter bf = new BinaryFormatter();
        //    //using (var ms = new MemoryStream())
        //    //{
        //    //    bf.Serialize(ms, obj);
        //    //    return ms.ToArray();
        //    //}
        //}
    }
}
