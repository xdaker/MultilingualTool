using System.IO;
using Newtonsoft.Json;

namespace MultilingualTool
{
    class JsonSerializer
    {
        public static T JsonFileToObject<T>(string path)
        {

            var jsonStr = File.ReadAllText(path);
            var ret = JsonConvert.DeserializeObject<T>(jsonStr);

            return ret;

        }

        public static string ObjectToJson<T>(T obj)
        {

            var ret = JsonConvert.SerializeObject(obj);

            return ret;

        }
    }
}
