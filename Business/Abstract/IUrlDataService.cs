using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUrlDataService
    {
        IResult AddUrl(UrlData urlData);
        IResult DeleteUrl(UrlData urlData);
        IResult UpdateUrl(UrlData urlData);
        IDataResult<object> UrlResult(string url,string token);
    }
}