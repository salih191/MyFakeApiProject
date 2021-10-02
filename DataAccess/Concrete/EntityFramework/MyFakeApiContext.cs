using Core.Entities.Concrete;
using Core.Utilities.IoC;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Concrete.EntityFramework
{
    public class MyFakeApiContext:DbContext
    {
        private readonly IConfiguration _configuration;
        public MyFakeApiContext()
        {
            _configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionStr = @"Data Source=SALIH;Initial Catalog=MyFakeApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            // optionsBuilder.UseSqlServer(@"Server=SALIH\salih;Database=MyFakeApi;Trusted_Connection=true");
            var conStr = _configuration.GetConnectionString("MyFakeApiDb");
           optionsBuilder.UseSqlServer(conStr);
        }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        public DbSet<UrlData> UrlDatas { get; set; }
    }
}