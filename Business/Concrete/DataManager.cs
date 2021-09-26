using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Business.Abstract;
using Core.Extensions;
using Core.Utilities.Results;
using Entities.Abstract;
using Entities.Concrete;
using Newtonsoft.Json.Linq;

namespace Business.Concrete
{
    public class DataManager : IDataService
    {
        private IData _data;

        public IDataResult<IData> ReadJson(JToken token)
        {
            IData baseData = null;
            switch (token)
            {
                case JArray jArray:
                {
                    var data = new ArrayData();
                    baseData = data;
                    //baseData.Children[token.Path] = data;
                    baseData.Children = baseData.Children.New();

                    data.Datas = data.Datas.New();
                    foreach (var jt in jArray)
                    {
                        var d = ReadJson(jt).Data;
                        baseData.Children[jt.Path] = d;
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
                        data.Datas[key] = ReadJson(value).Data;
                        baseData.Children[value.Path] = data.Datas[key];
                        data.Datas[key].BaseData = baseData;
                    }

                    break;
                }
                case JValue jValue:
                {
                    var data = new ValueData();
                    baseData = data;
                    var value = jValue.ToString();
                    data.Value = value;
                    var str = value.Split(":::");
                    if (str.Length>1)
                    {
                        var type = AppDomain.CurrentDomain.GetAssemblies()[0].GetTypes()
                            .FirstOrDefault(t => t.FullName != null && t.FullName.Contains("System") && t.Name.ToLower(new CultureInfo("en-US", false)) == str[0].ToLower(new CultureInfo("en-US", false)));
                        data.TypeValue = new KeyValuePair<string, string>(type?.FullName, str[1].ToLower(new CultureInfo("en-US", false)));
                    }

                    break;
                }
            }

            Debug.Assert(baseData != null, nameof(baseData) + " != null");
            baseData.Path = token.Path;
            return new SuccessDataResult<IData>(baseData);
        }

        public IDataResult<IData> ReadXml(XNode node)
        {
            throw new System.NotImplementedException();
        }

        public IResult Fill()
        {
            throw new System.NotImplementedException();
        }

        public IDataResult<object> GetData()
        {
            throw new System.NotImplementedException();
        }
    }
}