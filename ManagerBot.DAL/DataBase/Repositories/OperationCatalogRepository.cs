using ManagerBot.DAL.DataBase.Repositories.Abstract;
using ManagerBot.DAL.Entities;

namespace ManagerBot.DAL.DataBase.Repositories
{
    public class OperationCatalogRepository
        : BaseRepository<OperationCatalogEntity>
        , IOperationCatalogRepository
    {
        public OperationCatalogRepository(BotDbContext context)
            : base(context)
        {
        }
    }
}
