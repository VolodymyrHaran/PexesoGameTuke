using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using PexesoCore;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace Microsoft.AspNetCore.Http;

public static class SessionExtension
{
    public static object GetObject(this ISession session, string key)
    {
        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream stream = new MemoryStream(session.Get(key));
        return bf.Deserialize(stream);

    }

    public static void SetObject<T>(this ISession session, string key, T value)
    {
        
        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream stream = new MemoryStream();
        bf.Serialize(stream, value);
        long len = stream.Length;
        byte[] serializedObject = new byte[len];
        Array.Copy(stream.GetBuffer(), serializedObject, len);
        session.Set(key, serializedObject);

    }
}
