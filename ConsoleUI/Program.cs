using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using Business.Abstract;
using Business.Concrete;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Security.JWT;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.Json;
using Entities.Concrete;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleUI
{
    class Program
    {
        
        public static void Main(string[] args)
        {
             deneme();
             IDataService dataService = new DataManager(new DataSourceManager(new DataSourceDal()));
             const string json = "[{\"name\":\"name:::string\",\"surname\":\"surname:::string\"},{\"name\":\"name:::string\"}]";
             var js2 = "{\"array\":[{\"name\":\"string:::name\"}]}";
             var token = JsonConvert.DeserializeObject(json);
             var data=dataService.ReadJsonFromJToken(token as JToken).Data;
             //var data = dataService.ReadJson(token as JToken).Data;
            // var a = d;
          //  List<object> dat = new();
            //var d2 = new { name = "salih" };
           // dat.Add(new { name = "salih" });
           // dat.Add(new { name = "efe" });
           // dat.Add(new { name = "salla" });
            //var j = JsonConvert.SerializeObject(dat);
            //JsonHelper.Write(JsonConvert.DeserializeObject(j, typeof(ExpandoObject)) as List<object>);
            // var data2 = JsonHelper.GetListRandData<object>("name",10);
            // data2.ForEach(n=>Console.WriteLine(n));
            // Console.ReadLine();
            // var d3 = JsonHelper.GetRandData<string>("name");
            // Console.WriteLine("d3:"+d3);


            // dataService.SetCount(data.Data, "array[0]", 5);
             var d = dataService.GetData(data).Data;
            // Console.WriteLine(JsonConvert.SerializeObject(d));
            //var js = JsonConvert.SerializeObject(data);
            //Console.WriteLine(js);
            var d0 = dataService.DataToBinary(data).Data;
            //var d2 = dataService.BinaryToData(d).Data;
            //var str = Console.ReadLine();
            //var d3 = dataService.BinaryToData(Convert.FromBase64String(str)).Data;
           // Console.WriteLine(Convert.ToBase64String(d));
            // var d3 = dataService.GetData(d2).Data;
            // Console.WriteLine(JsonConvert.SerializeObject(d3));


            IUrlDataService urlDataService = new UrlDataManager(new EfUrlDataDal(), dataService);
            urlDataService.AddUrl(new UrlData() { Url = "asd", Token = "13e20f05-8ca0-4990-a066-0c2c6413dfd6", DataBytes = d0 });

           // var obj = urlDataService.UrlResult("asd", "13e20f05-8ca0-4990-a066-0c2c6413dfd6").Data;
           
           Console.WriteLine(JsonConvert.SerializeObject(d));
            var userManager = new UserManager(new EfUserDal());
            //while (Console.ReadLine()!="e")
            //{
            //    var t = Guid.NewGuid().ToString();
            //    Console.WriteLine(t.Length);
            //}
            //userManager.Add(new User()
            //{
            //    FirstName = "salih", Email = "asdasdas", LastName = "özkara", PasswordHash = new byte[0],
            //    PasswordSalt = new byte[0],UserName = "salih",Token = Guid.NewGuid().ToString()
            //});
           // urlDataService.AddUrl(new UrlData() { Url = "asd", Token = "13e20f05-8ca0-4990-a066-0c2c6413dfd6", DataBytes = d });

        }
       
        public static void deneme()
        {
            DataSourceManager data = new DataSourceManager(new DataSourceDal());
            var a=data.GetType().GetMethod("GetDataList");
            //MyAttribute myAttribute = new MyAttribute("sadasd");
            Console.WriteLine("deneme");
        }
    }
}
