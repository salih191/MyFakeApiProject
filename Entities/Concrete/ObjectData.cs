﻿using System.Collections.Generic;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class ObjectData:IData
    {
        public IData BaseData { get; set; }
        public string Path { get; set; }
        public Dictionary<string, IData> Children { get; set; }

        public Dictionary<string,IData> Datas { get; set; }
    }
}