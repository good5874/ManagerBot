using ManagerBot.DAL.Entity;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagerBot.DAL.DataBase.Repositories.Abstract
{
    public interface IAreaRepository 
        : IBaseRepository<AreaEntity>
    {
        Task<IEnumerable<AreaEntity>> GetAreasWithIncludesAsync();
    }
}
