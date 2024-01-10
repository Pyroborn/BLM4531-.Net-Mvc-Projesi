using System.Text.Json;

namespace Mysitemvc.Models
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            // Serialize the object to JSON
            string serializedValue = JsonSerializer.Serialize(value);

            // Set the serialized object in session
            session.SetString(key, serializedValue);
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            // Get the serialized object from session
            string serializedValue = session.GetString(key);

            // Deserialize the object from JSON
            return serializedValue == null ? default : JsonSerializer.Deserialize<T>(serializedValue);
        }
    }

}