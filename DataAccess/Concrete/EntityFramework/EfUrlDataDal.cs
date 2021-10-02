using System;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUrlDataDal : EfEntityRepositoryBase<UrlData, MyFakeApiContext>,IUrlDataDal
    {
        public bool Any(Expression<Func<UrlData, bool>> filter)
        {
            using var context = new MyFakeApiContext();
            return context.Set<UrlData>().Any(filter);
        }
    }
}