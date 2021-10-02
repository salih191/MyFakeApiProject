using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using Core.Extensions;
using Core.Utilities.Business;
using DataAccess.Abstract;
using Newtonsoft.Json;

namespace DataAccess.Concrete.Json
{
    public class DataSourceDal : IDataSourceDal
    {
        private readonly string _path = Helper.TryGetSolutionDirectoryInfo().FullName+"/Data/json/";

        public List<object> GetAll(string dataName, Func<dynamic, bool> filter = null)
        {
            using StreamReader r = new(@$"{_path}{dataName}.json");
            var json = r.ReadToEnd();
            r.Dispose();
            var items = JsonConvert.DeserializeObject(json, typeof(ExpandoObject)) as List<object>;
            Debug.Assert(items != null, nameof(items) + " != null");
            return filter==null?items:items.Where(filter).ToList();
        }

        public object Get(string dataName, Func<dynamic, bool> filter)
        {
            throw new NotImplementedException();
        }

        public async void Add(string dataName, object data)
        {
            var p = @$"{_path}{dataName}.json";
            
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }

            List<object> items = null;
            if (File.Exists(p))
            {
                var json = "";
                using (var r = File.OpenText(p))
                {
                    json = await r.ReadToEndAsync();
                }
                items = JsonConvert.DeserializeObject<List<object>>(json);
                items = items.New();
                items!.Add(data);

            }
            await using var fs = File.Create(p);
            await System.Text.Json.JsonSerializer.SerializeAsync(fs, items);

        }

        public async void AddList(string dataName, List<object> datas)
        {
            var p = @$"{_path}{dataName}.json";
            
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }

            if (File.Exists(p))
            {
                var json = "";
                using (var r = File.OpenText(p))
                {
                    json = await r.ReadToEndAsync();
                }
                var items = JsonConvert.DeserializeObject<List<object>>(json);
                if (items != null)
                {
                    datas.AddRange(items);
                }

            }
            await using var fs = File.Create(p);
            await System.Text.Json.JsonSerializer.SerializeAsync(fs, datas);
        }

        public void Update(string dataName, object oldData, object newData)
        {
            throw new NotImplementedException();
        }

        public void Delete(string dataName, object data)
        {
            throw new NotImplementedException();
        }
    }
}