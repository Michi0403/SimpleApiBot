using BotNetCore.Interfaces;
using System.IO;
using System.Xml.Serialization;

namespace HttpBotNet.Serializer
{
    /// <summary>
    /// Pretty Simple XML Serializer
    /// </summary>
    public class SimpleXMLSerializer : ISerializer
    {
        /// <summary>
        /// Deserialize Stream
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <returns></returns>
        public T Deserialize<T>(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stream);
        }
        /// <summary>
        /// Serialize Data to Stream
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="stream"></param>

        public void Serialize<T>(T data, Stream stream)
        {
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, data);
        }
    }
}
