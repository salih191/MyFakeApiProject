using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IDataSourceService
    {
       // IDataResult<object> GetData(ValueData valueData);
        IResult SetData(string propertyName, object data);
        IResult UpdateData(string propertyName,object oldData ,object newData);
        IResult DeleteData(string propertyName, object data);
        IDataResult<List<object>> GetDataList(string propertyName);
        IDataResult<object> GetRandData(string propertyName);
    }
}