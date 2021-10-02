using System;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IDataSourceDal
    {
        List<object> GetAll(string dataName,Func<dynamic, bool> filter = null);
        object Get(string dataName,Func<dynamic, bool> filter);
        void Add(string dataName,object data);
        void AddList(string dataName,List<object> datas);
        void Update(string dataName,object oldData,object newData);
        void Delete(string dataName,object data);
    }
}