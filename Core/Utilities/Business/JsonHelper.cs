using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Core.Extensions;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Core.Utilities.Business
{
    public class JsonHelper
    {
        private const string Path = "./Data/json/";

        public static List<T> GetListRandData<T>(string property, int count)
        {
            using StreamReader r = new(@$"{Path}{property}.json");
            string json = r.ReadToEnd();
            r.Dispose();
            List<T> items = JsonConvert.DeserializeObject<List<T>>(json);
            return items.PickRandom(count).ToList();
        }
        public static T GetRandData<T>(string property)
        {
            using StreamReader r = new(@$"{Path}{property}.json");
            string json = r.ReadToEnd();
            r.Dispose();
            List<T> items = JsonConvert.DeserializeObject<List<T>>(json);
            return items.PickRandom();
        }

        public static void Write(List<object> data)
        {
            var p = (data[0] as ExpandoObject).GetAll().Keys.ToArray();
            Dictionary<string, List<string>> stringsDictionary = new();
            foreach (var s in p)
            {
                stringsDictionary[s] = new();
            }
            foreach (var o in data)
            {
                var o2 = (o as ExpandoObject).GetAll();
                foreach (var o1 in o2)
                {
                    stringsDictionary[o1.Key].Add(o1.Value.ToString());
                }
            }

            FileWrite(stringsDictionary);
        }

        private static async void FileWrite(Dictionary<string, List<string>> stringsDictionary)
        {
            foreach (var (key, value) in stringsDictionary)
            {
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
                await using FileStream fs = File.Create(@$"{Path}{key}.json");
                await JsonSerializer.SerializeAsync(fs, value);
                await fs.DisposeAsync();
            }
        }
    }
}
