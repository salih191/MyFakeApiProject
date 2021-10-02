using System;
using System.Reflection;
using System.Text;
using Business.Abstract;
using Core.Aspects.Caching;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace Business.Concrete
{
    public class UrlDataManager:IUrlDataService
    {
        private readonly IUrlDataDal _urlDataDal;
        private readonly IDataService _dataService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UrlDataManager(IUrlDataDal urlDataDal, IDataService dataService)
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _urlDataDal = urlDataDal;
            _dataService = dataService;
            var methods = this.GetType().GetMethods();
            foreach (var methodInfo in methods)
            {
                var attributes = methodInfo.GetCustomAttributes();
            }
        }

        public IResult AddUrl(UrlData urlData)
        {
            try
            {
                _urlDataDal.Add(urlData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException?.Message);
                return new ErrorResult(e.InnerException?.Message);
            }
            return new SuccessResult();
        }

        public IResult DeleteUrl(UrlData urlData)
        {
            throw new System.NotImplementedException();
        }

        public IResult UpdateUrl(UrlData urlData)
        {
            throw new System.NotImplementedException();
        }

        
        public IDataResult<object> UrlResult(string url, string token)
        {
            var urlArray = url.Split("/");
            if (token==StringValues.Empty&& _urlDataDal.Any(u=>u.Token==urlArray[0]))
            {
                token = urlArray[0];
                url=url.Replace(urlArray[0] + "/", "");
            }
            var result = _urlDataDal.Get(u => u.Token == token && u.Url == url);
            var data = _dataService.BinaryToData(result.DataBytes).Data;
            _dataService.SetCount(data, "[0]", int.Parse(_httpContextAccessor.HttpContext.Request.Query["q"].ToString()));
            var byt = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_dataService.GetData(data).Data));
            _httpContextAccessor.HttpContext.Response.ContentType = result.Content_Type;
            _httpContextAccessor.HttpContext.Response.Body.WriteAsync(byt,0,byt.Length);
            if (result==null)
            {
                return new ErrorDataResult<object>();
            }
            
            return new SuccessDataResult<object>(_dataService.GetData(_dataService.BinaryToData(result.DataBytes).Data).Data);
        }
    }
}