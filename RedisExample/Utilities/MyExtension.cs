using System.Text;
using System.Text.Json;

namespace RedisExample.Utilities
{
    public static class MyExtension
    {
        public static string ToJson(this object obj)
        {
            obj ??= "";
            var result = JsonSerializer.Serialize(obj);
            return result;
        }
        public static T JsonToObject<T>(this string str)
        {
            try
            {
                var result = JsonSerializer.Deserialize<T>(str);
                return result;
            }
            catch (Exception e)
            {
                return default(T);
            }
        }
        // Convert an object to a byte array
        public static byte[] ObjectToByteArray(this object obj)
        {
            var jsonStr = obj.ToJson();
            var bytes = Encoding.UTF8.GetBytes(jsonStr);
            return bytes;
        }
        // Convert a byte array to an Object
        public static T ByteArrayToObject<T>(this byte[] arrBytes)
        {
            var jsonStr = Encoding.UTF8.GetString(arrBytes);
            var res = jsonStr.JsonToObject<T>();
            return res;
        }
        //// Convert an object to a byte array
        //public static byte[] ObjectToByteArray(this object obj)
        //{
        //    BinaryFormatter bf = new BinaryFormatter();
        //    using (var ms = new MemoryStream())
        //    {
        //        bf.Serialize(ms, obj);
        //        return ms.ToArray();
        //    }
        //}
        //// Convert a byte array to an Object
        //public static Object ByteArrayToObject(this byte[] arrBytes)
        //{
        //    using (var memStream = new MemoryStream())
        //    {
        //        var binForm = new BinaryFormatter();
        //        memStream.Write(arrBytes, 0, arrBytes.Length);
        //        memStream.Seek(0, SeekOrigin.Begin);
        //        var obj = binForm.Deserialize(memStream);
        //        return obj;
        //    }
        //}
    }
}
