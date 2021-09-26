using System;
using System.IO;
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Security.JWT;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleUI
{
    class Program
    {
        public static void Main(string[] args)
        {
            IDataService dataService = new DataManager();
            string json = "[{\"name\":\"string:::name\"}]";
            var token = JsonConvert.DeserializeObject(json);
            var d = dataService.ReadJson(token as JToken).Data;
            var a = d;
        }
    }
}
