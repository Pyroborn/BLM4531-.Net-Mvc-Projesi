using System.Text.Json;

namespace Mysitemvc.Models
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {

            string serializedValue = JsonSerializer.Serialize(value);

         
            session.SetString(key, serializedValue);
        }

        public static T GetObject<T>(this ISession session, string key)
        {
   
            string serializedValue = session.GetString(key);

    
            return serializedValue == null ? default : JsonSerializer.Deserialize<T>(serializedValue);
        }
    }

}