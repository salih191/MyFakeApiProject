using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;
using Business.Abstract;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Entities.Abstract;
using Entities.Concrete;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Business.Concrete
{
    public class DataManager : IDataService
    {
        private Dictionary<string, List<string>> _list;
        private readonly IDataSourceService _dataSourceService;
        public DataManager(IDataSourceService dataSourceService)
        {
            _dataSourceService = dataSourceService;
            _list = new();
        }
        public IDataResult<IData> ReadJsonFromJToken(JToken token)//jsonı okuyarak benim veri tipim olan IData ya dönüştürüyor
        {
            IData baseData = null;
            switch (token)//jtoken dizi mi obje mi değer mi 
            {
                case JArray jArray://dizi ise
                {
                    var data = new ArrayData();
                    baseData = data;
                    //baseData.Children[token.Path] = data;
                    baseData.Children = baseData.Children.New();

                    data.Datas = data.Datas.New();
                    foreach (var jt in jArray)//dizi geziliyor ve her jtoken için işlem baştan başlıyor
                    {
                        var d = ReadJsonFromJToken(jt).Data;
                        baseData.Children[jt.Path] = d;
                        baseData.Children.AddRange(d.Children);
                        d.BaseData = baseData;
                        data.Datas[d] = 1;
                    }

                    break;
                }
                case JObject jObject:
                {
                    var data = new ObjectData();
                    baseData = data;
                    baseData.Children = baseData.Children.New();
                    // baseData.Children[token.Path] = data;
                    data.Datas = data.Datas.New();
                    foreach (var (key, value) in jObject)
                    {
                        var d = ReadJsonFromJToken(value).Data;
                        data.Datas[key] = d;
                        baseData.Children[value.Path] = d;
                        baseData.Children.AddRange(d.Children);
                        data.Datas[key].BaseData = baseData;
                    }

                    break;
                }
                case JValue jValue:
                {
                    var data = new ValueData();
                    baseData = data;
                    var value = jValue.ToString(CultureInfo.CurrentCulture);
                    data.Value = value;
                    var str = value.Split(":::");
                    if (str.Length > 1)
                    {
                        var type = AppDomain.CurrentDomain.GetAssemblies()[0].GetTypes()
                            .FirstOrDefault(t =>
                                t.FullName != null && t.FullName.Contains("System") &&
                                t.Name.ToLower(new CultureInfo("en-US", false)) ==
                                str[1].ToLower(new CultureInfo("en-US", false)));
                        data.TypeValue = new KeyValuePair<string, string>(str[0].ToLower(new CultureInfo("en-US", false)),type?.FullName
                            );
                    }

                    break;
                }
            }

            Debug.Assert(baseData != null, nameof(baseData) + " != null");
            baseData.Path = token.Path;
            return new SuccessDataResult<IData>(baseData);
        }

        public IDataResult<IData> ReadJsonFromString(string json)
        {
            var token = JsonConvert.DeserializeObject(json) as JToken;
            return new SuccessDataResult<IData>(ReadJsonFromJToken(token).Data);
        }

        public IDataResult<IData> ReadXml(XNode node)
        {
            throw new System.NotImplementedException();
        }
        

        private object Fill(IData data)
        {
            //string key1 = "";
           // string key2 = "";
           // string key3 = "";
            // if (!key2.StartsWith("[") && key1 != "")
            // {
            //     key3 = "." + key2;
            // }
            // else
            // {
            //     key3 = key2;
            // }
            //
            // key1 += key3;
            //_dictionary[_key1]=data;
            var instance = data switch
            {
                ArrayData arrayData => FillArrayData(arrayData),
                ObjectData objectData => FillObjectData(objectData),
                ValueData valueData => FillValueData(valueData),
                _ => null
            };

            object FillArrayData(ArrayData arrayData)
            {
                ArrayList arrayList = new();
                foreach (var (key, value) in arrayData.Datas)
                {
                    for (var i = 0; i < value; i++)
                    {
                        //key2 = $"[{i}]";
                        arrayList.Add(Fill(key));
                    }
                }

                return arrayList;
            }

            object FillObjectData(ObjectData objectData)
            {
                Dictionary<string, object> dictionary = new();
                foreach (var (key, value) in objectData.Datas)
                {
                    //key2 = $"{key}";
                    dictionary.Add(key, Fill(value));
                }

                return dictionary;
            }

            object FillValueData(ValueData valueData)
            {
                object o = null;
                if (valueData.TypeValue.Key == "data")
                {
                }
                else if (valueData.TypeValue.Key is not null)
                {
                    //o = _dataSourceService.GetRandData(valueData.TypeValue.Key).Data;
                    o = _dataSourceService.GetDataList(valueData.TypeValue.Key).Data.PickRandom();
                    // if (!_list.ContainsKey(valueData.TypeValue.Value))
                    // {
                    //     _list[valueData.TypeValue.Value] = JsonHelper.GetListData<string>(valueData.TypeValue.Value);
                    // }
                    //
                    // o = _list[valueData.TypeValue.Value].PickRandom();
                }

                return o;
            }

            //_dictionary.SetValue2(_key1, instance);
            //key1 = key1.Remove(key1.Length - key3.Length, key3.Length);
            return instance;
        }


        public IDataResult<object> GetData(IData data)
        {
            return new SuccessDataResult<object>(Fill(data));
        }

        public IResult SetCount(IData data, string key, int count)
        {
            var d=data.Children[key] as ObjectData;
            var b = d?.BaseData as ArrayData;
            b!.Datas[d!] = count;
            return new SuccessResult();
        }
        private static IData DeserializeFromByteArray(byte[] b)
        {
            // byte[] b = Convert.FromBase64String(settings);
            using var stream = new MemoryStream(b);
            var formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            return (IData)formatter.Deserialize(stream);
        }

        private static byte[] SerializeToByteArray<TData>(TData settings)
        {
            using var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, settings);
            stream.Flush();
            stream.Position = 0;
            return stream.ToArray();
        }
        public IDataResult<byte[]> DataToBinary(IData data)
        {
            return new SuccessDataResult<byte[]>(SerializeToByteArray(data));
        }

        public IDataResult<IData> BinaryToData(byte[] bytes)
        {
            return new SuccessDataResult<IData>(DeserializeFromByteArray(bytes));
        }
    }
}