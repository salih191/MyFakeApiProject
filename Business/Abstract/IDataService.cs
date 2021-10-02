using System.Collections.Generic;
using System.Xml.Linq;
using Core.Utilities.Results;
using Entities.Abstract;
using Newtonsoft.Json.Linq;

namespace Business.Abstract
{
    public interface IDataService
    {
        IDataResult<IData> ReadJsonFromJToken(JToken token);
        IDataResult<IData> ReadJsonFromString (string json);
        IDataResult<IData> ReadXml(XNode node);
        IDataResult<object> GetData(IData data);
        IResult SetCount(IData data,string key, int count);
        IDataResult<byte[]> DataToBinary(IData data);
        IDataResult<IData> BinaryToData(byte[] bytes);

    }
}