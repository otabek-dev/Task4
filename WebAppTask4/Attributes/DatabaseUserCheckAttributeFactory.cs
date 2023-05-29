using Microsoft.AspNetCore.Mvc.Filters;
using WebAppTask4.Areas.Identity.Data;
using WebAppTask4.Data;

namespace WebAppTask4.Attributes
{
    public class DatabaseUserCheckAttributeFactory : IFilterFactory
    {
        private readonly AppDbContext _dbContext;

        public DatabaseUserCheckAttributeFactory(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return new DatabaseUserCheckAttribute(_dbContext);
        }

        public bool IsReusable => false;
    }
}
