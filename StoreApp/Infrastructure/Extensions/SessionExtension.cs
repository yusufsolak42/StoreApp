using System.Text.Json;

namespace StoreApp.Infrastructure.Extensions
{
    public static class SessionExtension //its generally used static, so we dont need to "new" this class. Can be used anywhere.
    {
        public static void SetJson(this ISession session, string key, object value) //Extension method, we need to give what we extend as the first parameter.
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }
        public static void SetJson<T>(this ISession session, string key, T value) //Extension method, we need to give what we extend as the first parameter.
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? GetJson<T>(this ISession session, string key) 
        {
            var data = session.GetString(key);
            return data is null ? default(T)
            : JsonSerializer.Deserialize<T>(data);
        }
    }
}