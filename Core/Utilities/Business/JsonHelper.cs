// using System;
// using System.Collections.Generic;
// using System.Dynamic;
// using System.IO;
// using System.Linq;
// using System.Text;
// using System.Threading.Channels;
// using System.Threading.Tasks;
// using Core.Extensions;
// using Newtonsoft.Json;
// using JsonSerializer = System.Text.Json.JsonSerializer;
//
// namespace Core.Utilities.Business
// {
//     public static class JsonHelper
//     {
//         //private static readonly string Path = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName, "/Data/json/");
//         // private const string Path = "./Data/json/";
// //        private static readonly string Path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory())?.FullName+"/Data/json/";
//         private static readonly string Path = Helper.TryGetSolutionDirectoryInfo().FullName+"/Data/json/";
//
//         public static List<T> GetListRandData<T>(string property, int count, string path = null)
//         {
//             path ??= Path;
//             using StreamReader r = new(@$"{path}{property}.json");
//             var json = r.ReadToEnd();
//             r.Dispose();
//             //var items = JsonConvert.DeserializeObject<List<T>>(json);
//             var items = JsonConvert.DeserializeObject(json, typeof(ExpandoObject)) as List<T>;
//             return items.PickRandom(count).ToList();
//         }
//         public static List<T> GetListData<T>(string property, string path = null)
//         {
//             path ??= Path;
//             using StreamReader r = new(@$"{path}{property}.json");
//             var json = r.ReadToEnd();
//             r.Dispose();
//             var items = JsonConvert.DeserializeObject<List<T>>(json);
//             return items;
//         }
//         public static T GetRandData<T>(string property, string path = null)
//         {
//             path ??= Path;
//             var json = "";
//             using (var r = File.OpenText(@$"{path}{property}.json"))
//             {
//                 json = r.ReadToEnd();
//             }
//             var items = JsonConvert.DeserializeObject<List<T>>(json);
//             return items.PickRandom();
//         }
//
//         public static void Write(List<object> data, string path = null)
//         {
//             path ??= Path;
//             var p = (data[0] as ExpandoObject).GetAll().Keys.ToArray();
//             Dictionary<string, List<string>> stringsDictionary = new();
//             foreach (var s in p)
//             {
//                 stringsDictionary[s] = new();
//             }
//             foreach (var o1 in data.Select(o => (o as ExpandoObject).GetAll()).SelectMany(o2 => o2))
//             {
//                 stringsDictionary[o1.Key].Add(o1.Value.ToString());
//             }
//
//             FileWrite(stringsDictionary, path);
//         }
//
//         public static async void Add(object data, string propertyName, string path = null)
//         {
//             path ??= Path;
//             var p = @$"{path}{propertyName}.json";
//
//             var values = new List<object>() { data };
//             if (!Directory.Exists(path))
//             {
//                 Directory.CreateDirectory(path);
//             }
//
//             if (File.Exists(p))
//             {
//                 var json = "";
//                 using (var r = File.OpenText(p))
//                 {
//                     json = await r.ReadToEndAsync();
//                 }
//                 var items = JsonConvert.DeserializeObject<List<object>>(json);
//                 if (items != null)
//                 {
//                     values.AddRange(items);
//                 }
//
//             }
//             await using var fs = File.Create(path);
//             await JsonSerializer.SerializeAsync(fs, values);
//
//         }
//         private static async void FileWrite(Dictionary<string, List<string>> stringsDictionary, string p)
//         {
//             foreach (var (key, value) in stringsDictionary)
//             {
//                 var path = @$"{p}{key}.json";
//                 if (!Directory.Exists(p))
//                 {
//                     Directory.CreateDirectory(p);
//                 }
//
//                 if (File.Exists(path))
//                 {
//                     var json = "";
//                     using (var r = File.OpenText(path))
//                     {
//                         json = await r.ReadToEndAsync();
//                     }
//                     var items = JsonConvert.DeserializeObject<List<string>>(json);
//                     if (items != null)
//                     {
//                         value.AddRange(items);
//                     }
//
//                 }
//                 await using var fs = File.Create(path);
//                 await JsonSerializer.SerializeAsync(fs, value);
//
//             }
//         }
//     }
// }
