using System;
using System.Collections.Generic;
using System.Reflection;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Caching;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Interceptors;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class DataSourceManager : IDataSourceService
    {
        //private Dictionary<string, List<object>> _dataSource;
        private readonly IDataSourceDal _dataSourceDal;
        private readonly IDataSourceService _dataSourceService;
        public DataSourceManager(IDataSourceDal dataSourceDal)
        {
            _dataSourceDal = dataSourceDal;
            _dataSourceService = this;
            //var methods = this.GetType().GetMethods();
            //foreach (var methodInfo in methods)
            //{
            //    var attributes = methodInfo.GetCustomAttributes();
            //}
        }

        // public IDataResult<object> GetData(ValueData valueData)
        // {
        //     var count = GetCount(valueData);
        //     var data = GetData(valueData.TypeValue.Key, count);
        //     //data = Convert.ChangeType(data, Type.GetType(valueData.TypeValue.Value)!);
        //     return new SuccessDataResult<object>(data);
        // }

        // private object GetData(string propertyName, int count)
        // {
        //     _dataSource = _dataSource.New();
        //     if (!_dataSource.ContainsKey(propertyName))
        //     {
        //         _dataSource[propertyName]=JsonHelper.GetListRandData<object>(propertyName, count);
        //     }
        //
        //     return _dataSource[propertyName].PickRandom();
        // }
        // private int GetCount(ValueData valueData)
        // {
        //     var data = GetBaseArray(valueData);
        //     return 100;
        // }
        //
        // private ArrayData GetBaseArray(ValueData valueData)
        // {
        //     if (valueData == null) throw new ArgumentNullException(nameof(valueData));
        //     IData baseData = valueData;
        //     while (baseData is not ArrayData && baseData is not null)
        //     {
        //         baseData = baseData.BaseData;
        //     }
        //
        //     return baseData as ArrayData;
        // }
        [CacheRemoveAspect("IDataSourceService.Get")]
        public IResult SetData(string propertyName, object data)
        {
            _dataSourceDal.Add(propertyName, data);
            return new SuccessResult();
        }
        [CacheRemoveAspect("IDataSourceService.Get")]
        public IResult UpdateData(string propertyName, object oldData, object newData)
        {
            _dataSourceDal.Update(propertyName, oldData, newData);
            return new SuccessResult();
        }

        [CacheRemoveAspect("IDataSourceService.Get")]
        public IResult DeleteData(string propertyName, object data)
        {
            _dataSourceDal.Delete(propertyName, data);
            return new SuccessResult();
        }

        [CacheAspect()]
        //[SecuredOperation("admin2")]
        public IDataResult<List<object>> GetDataList(string propertyName)
        {

            return new SuccessDataResult<List<object>>(_dataSourceDal.GetAll(propertyName));
        }
        // [SecuredOperation("admin2")]
        public IDataResult<object> GetRandData(string propertyName)
        {
            var result = _dataSourceService.GetDataList(propertyName).Data;
            return new SuccessDataResult<object>(result.PickRandom());
        }
    }
}