using System;
using System.Collections.Generic;
using Entities.Abstract;

namespace Entities.Concrete
{
    [Serializable]
    public class ValueData:IData
    {
        public IData BaseData { get; set; }
        public string Path { get; set; }
        public Dictionary<string, IData> Children { get; set; }
        public string Value { get; set; }
        public KeyValuePair<string, string> TypeValue { get; set; }
    }
}