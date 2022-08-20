using BotNetCore.Interfaces;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using BotNetCore.Helper;
using System.Reflection;
using System;

namespace HttpBotNet.Serializer
{
    /// <summary>
    /// Simple DataContract XML Serializer
    /// </summary>
    public class SimpleDataContractSerializer : ISerializer
    {
        /// <summary>
        /// Tries to find assembly of type in BotNetCore Assembly and deserialize the stream to an object of it
        /// </summary>
        /// <typeparam name="T">Type of Object to deserialize to</typeparam>
        /// <param name="stream"></param>
        /// <returns></returns>
        public T Deserialize<T>(Stream stream)
        {
            Assembly assembly = Assembly.Load(new AssemblyName("BotNetCore"));

            //typeof(Object), null, int.MaxValue, false, true, null, new MyDataContractResolver(assembly)
            //var serializer = new DataContractSerializer(typeof(T), null, int.MaxValue, false, true, null, new MyDataContractResolver(assembly),);
            var resolver = new MyDataContractResolver(assembly);
            DataContractSerializerSettings setting = new DataContractSerializerSettings() {DataContractResolver = resolver };

            var serializer = new DataContractSerializer(typeof(T), setting);

            var returnObject = (T)serializer.ReadObject(stream);

            return returnObject;
        }

        private void close(XmlDictionaryReader reader)
        {
            reader.Close();
            reader.Dispose();
        }

        public void Serialize<T>(T data, Stream stream)
        {
            Assembly assembly = Assembly.Load(new AssemblyName("BotNetCore"));

            var resolver = new MyDataContractResolver(assembly);
            DataContractSerializerSettings setting = new DataContractSerializerSettings() { DataContractResolver = resolver };

            var serializer = new DataContractSerializer(typeof(T), setting);
            /*
             * Experimental
             */

            using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(stream, Encoding.Unicode))
            {
                writer.WriteStartDocument();
                serializer.WriteObject(writer, data);
                writer.Flush();
            }
        }
    }
}
