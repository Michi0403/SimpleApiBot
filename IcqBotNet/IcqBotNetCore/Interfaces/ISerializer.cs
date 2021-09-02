﻿using System.IO;
namespace IcqBotNetCore.Interfaces
{
    public interface ISerializer
    {
        public T Deserialize<T>(Stream stream);
        public void Serialize<T>(T data, Stream stream);
    }
}
