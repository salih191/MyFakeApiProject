using System.Collections.Generic;
using System.Xml.Linq;
using Core.Utilities.Results;
using Entities.Abstract;
using Newtonsoft.Json.Linq;

namespace Business.Abstract
{
    public interface IDataService
    {
        IDataResult<IData> ReadJson(JToken token);
        IDataResult<IData> ReadXml(XNode node);
        IResult Fill();
        IDataResult<object> GetData();
        
    }
}