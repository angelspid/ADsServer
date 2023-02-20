using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ADsServer.Classes
{
    internal class BytesConvert
    {
        /// <summary>
        /// Convert object to bytes
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="obj">Object to convert</param>
        /// <returns>Bytes array</returns>
        public byte[] ToByteArray<T>(T obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
        /// <summary>
        /// Convert bytes array to object
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="data">Bytes array</param>
        /// <returns>Object</returns>
        public T FromByteArray<T>(byte[] data)
        {
            if (data == null)
                return default(T);
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }
        /// <summary>
        /// Compress bytes array
        /// </summary>
        /// <param name="data">Bytes array</param>
        /// <returns>Bytes array</returns>
        public  byte[] Compress(byte[] data)
        {
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(output, CompressionLevel.SmallestSize))
                dstream.Write(data, 0, data.Length);
            return output.ToArray();
        }
        /// <summary>
        /// Decompress bytes array
        /// </summary>
        /// <param name="data">Bytes array</param>
        /// <returns>Bytes array</returns>
        public byte[] Decompress(byte[] data)
        {
            MemoryStream input = new MemoryStream(data);
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
                dstream.CopyTo(output);
            return output.ToArray();
        }
    }
}
