using BotNetCore.Helper;
using BotNetCore.Interfaces;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace BotNetCore.Extensions
{
    /// <summary>
    /// Just an Interface with IOT Serializer Singleton
    /// </summary>
    public static class IDataFileExtension
    {
        /// <summary>
        /// Home of the injected Serializer
        /// </summary>
        public partial class Singleton
        {
            /// <summary>
            /// Singleton Instance
            /// </summary>
            public static Singleton Instance { get; set; } = new Singleton();
            private Singleton()
            {
            }
            /// <summary>
            /// Serializer who inherits ISerializer Interface
            /// </summary>
            public ISerializer Serializer { get; set; }
        }
        /// <summary>
        /// Basic Logic for Saving IDataFile
        /// </summary>
        /// <typeparam name="T">Type of IDataFile</typeparam>
        /// <param name="data">value of IDataFile</param>
        /// <param name="path">path to save to</param>
        public static void Save<T>(this T data, string path) where T : IDataFile, new()
        {
            try
            {
                if (!File.Exists(path))
                    CreateNew<T>(path);
                using FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
                {
                    ISerializer serializer = Singleton.Instance.Serializer;
                    serializer.Serialize(data, stream);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Saving IDataFile failed");
                Console.WriteLine(ex.ToString());
            }
         
        }
        /// <summary>
        /// Load IDataFile to value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">IDataFile</param>
        /// <param name="path">to load from, include Filename</param>
        /// <returns></returns>
        public static T Load<T>(this T data, string path) where T : IDataFile, new()
        {
            try
            {
                if (!File.Exists(path))
                    return CreateNew<T>(path);
                using FileStream stream = new FileStream(path, FileMode.Open);
                {
                    ISerializer serializer = Singleton.Instance.Serializer;
                    return (T)serializer.Deserialize<T>(stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loading IDataFile failed");
                Console.WriteLine(ex.ToString());
                return default(T);
            }
            
        }

        private static T CreateNew<T>(string path) where T : IDataFile, new()
        {
            try
            {
                var data = new T();
                GeneralHelper.TryCreateDirectoryForThisFile(path);
                using FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
                {
                    ISerializer serializer = Singleton.Instance.Serializer;
                    serializer.Serialize(data, stream);
                    return data;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Create New failed");
                Console.WriteLine(ex.ToString());
                return default(T);
            }

        }
    }
}
