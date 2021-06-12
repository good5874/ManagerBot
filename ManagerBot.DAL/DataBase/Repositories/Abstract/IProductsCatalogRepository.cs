using ManagerBot.DAL.Entities;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagerBot.DAL.DataBase.Repositories.Abstract
{
    public interface IProductsCatalogRepository
        : IBaseRepository<ProductCatalogEntity>
    {
        Task<IEnumerable<ProductCatalogEntity>> GetProductsWithIncludesAsync();
    }
}
