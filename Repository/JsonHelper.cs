using Newtonsoft.Json;

namespace Repository
{
    public static class JsonHelper
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, new Newtonsoft.Json.Converters.StringEnumConverter());
        }
    }
}
