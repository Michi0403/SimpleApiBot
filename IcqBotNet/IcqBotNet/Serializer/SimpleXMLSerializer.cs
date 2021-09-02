using IcqBotNetCore.Interfaces;
using System.IO;
using System.Xml.Serialization;

namespace IcqBotNet.Serializer
{
    public class SimpleXMLSerializer : ISerializer
    {
        public T Deserialize<T>(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stream);
        }

        public void Serialize<T>(T data, Stream stream)
        {
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, data);
        }
    }
}
