using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace HookManager.Models
{
    internal interface IConfiguration
    {
        string Get(string key);
        void Set(string key, string value);
    }

    internal class Configuration : IConfiguration
    {
        private const string FileName = "config.json";

        public string Get(string key)
        {
            if (File.Exists(FileName))
            {
                var json = File.ReadAllText(FileName);
                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                return dict.TryGetValue(key, out var value) ? value : null;
            }

            return null;
        }

        public void Set(string key, string value)
        {
            var dict = new Dictionary<string, string>();

            if (File.Exists(FileName))
            {
                var json = File.ReadAllText(FileName);
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }

            dict[key] = value;

            File.WriteAllText(FileName, JsonConvert.SerializeObject(dict));
        }
    }
}