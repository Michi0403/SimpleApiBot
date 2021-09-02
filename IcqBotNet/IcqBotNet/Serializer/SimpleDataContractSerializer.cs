using IcqBotNetCore.Interfaces;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using IcqBotNetCore.Helper;
using System.Reflection;
using System;

namespace IcqBotNet.Serializer
{
    public class SimpleDataContractSerializer : ISerializer
    {
        public T Deserialize<T>(Stream stream)
        {
            Assembly assembly = Assembly.Load(new AssemblyName("IcqBotNetCore"));

            //typeof(Object), null, int.MaxValue, false, true, null, new MyDataContractResolver(assembly)
            //var serializer = new DataContractSerializer(typeof(T), null, int.MaxValue, false, true, null, new MyDataContractResolver(assembly),);
            var resolver = new MyDataContractResolver(assembly);
            DataContractSerializerSettings setting = new DataContractSerializerSettings() {DataContractResolver = resolver };

            var serializer = new DataContractSerializer(typeof(T), setting);

            var test = (T)serializer.ReadObject(stream);

            return test;//(T)serializer.ReadObject(stream);
        }

        private void close(XmlDictionaryReader reader)
        {

        }

        public void Serialize<T>(T data, Stream stream)
        {
            Assembly assembly = Assembly.Load(new AssemblyName("IcqBotNetCore"));

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
