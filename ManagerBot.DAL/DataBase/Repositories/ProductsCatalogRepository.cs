using ManagerBot.DAL.DataBase.Repositories.Abstract;
using ManagerBot.DAL.Entities;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagerBot.DAL.DataBase.Repositories
{
    public class ProductsCatalogRepository
        : BaseRepository<ProductCatalogEntity>,
          IProductsCatalogRepository
    {
        private readonly BotDbContext context;

        public ProductsCatalogRepository(BotDbContext context)
            : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ProductCatalogEntity>> GetProductsWithIncludesAsync()
        {
            return await context.ProductCatalog
                .Include(c => c.OperationCatalog)
                .ToListAsync();
        }
    }
}
