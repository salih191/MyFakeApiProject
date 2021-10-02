using System;
using System.Collections.Generic;

namespace Entities.Abstract
{
    public interface IData
    {
        public IData BaseData { get; set; }
        public string Path { get; set; }
        public Dictionary<string,IData> Children { get; set; }
    }
}