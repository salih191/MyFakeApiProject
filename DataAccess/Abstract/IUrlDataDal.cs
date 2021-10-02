using System;
using System.Linq.Expressions;
using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IUrlDataDal:IEntityRepository<UrlData>
    {
        bool Any(Expression<Func<UrlData, bool>> filter);
    }
}