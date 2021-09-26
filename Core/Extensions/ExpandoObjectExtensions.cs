using System.Collections.Generic;
using System.Dynamic;

namespace Core.Extensions
{
    public static class ExpandoObjectExtensions
    {
        public static IDictionary<string, object> GetAll(this ExpandoObject expandoObject)
        {
            var result = expandoObject as IDictionary<string, object>;
            return result;
        }
    }
}