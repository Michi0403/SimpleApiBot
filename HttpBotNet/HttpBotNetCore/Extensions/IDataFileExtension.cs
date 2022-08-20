using BotNetCore.Interfaces;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace BotNetCore.Extensions
{

    public static class IDataFileExtension
    {
        public partial class Singleton
        {
            public static Singleton Instance { get; set; } = new Singleton();
            private Singleton()
            {
            }
            public ISerializer Serializer { get; set; }
        }

        public static void Save<T>(this T data, string path) where T : IDataFile, new()
        {
            if (!File.Exists(path))
                CreateNew<T>(path);
            using FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            {
                ISerializer serializer = Singleton.Instance.Serializer;
                serializer.Serialize(data, stream);
            }
        }

        public static T Load<T>(this T data, string path) where T : IDataFile, new()
        {
            if (!File.Exists(path))
                return CreateNew<T>(path);
            using FileStream stream = new FileStream(path, FileMode.Open);
            {
                ISerializer serializer = Singleton.Instance.Serializer;
                return (T)serializer.Deserialize<T>(stream);
            }
        }

        private static T CreateNew<T>(string path) where T : IDataFile, new()
        {
            var data = new T();
            var pathToDirectory = Path.GetDirectoryName(path);
            if ( !Directory.Exists(pathToDirectory))
                Directory.CreateDirectory(pathToDirectory);
            using FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            {
                ISerializer serializer = Singleton.Instance.Serializer;
                serializer.Serialize(data, stream);
                return data;
            }
        }
    }
}
