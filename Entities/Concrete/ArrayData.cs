using System.Collections.Generic;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class ArrayData:IData
    {
        public IData BaseData { get; set; }
        public string Path { get; set; }
        public Dictionary<string, IData> Children { get; set; }

        public Dictionary<IData,int> Datas { get; set; }
    }
}