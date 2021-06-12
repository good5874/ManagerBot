using ManagerBot.DAL.DataBase.Repositories.Abstract;
using ManagerBot.DAL.Entities;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagerBot.DAL.DataBase.Repositories
{
    public class AreaRepository
        : BaseRepository<AreaEntity>
        , IAreaRepository
    {
        private readonly BotDbContext context;

        public AreaRepository(BotDbContext context)
            : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<AreaEntity>> GetAreasWithIncludesAsync()
        {
            return await context.Areas
                .Include(c => c.ProductCatalog)
                .ToListAsync();
        }
    }
}
