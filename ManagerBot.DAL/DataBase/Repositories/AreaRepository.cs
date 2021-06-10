using ManagerBot.DAL.DataBase.Repositories.Abstract;
using ManagerBot.DAL.Entity;

namespace ManagerBot.DAL.DataBase.Repositories
{
    public class AreaRepository
        : BaseRepository<AreaEntity>,
          IAreaRepository
    {
        public AreaRepository(BotDbContext context) 
            : base(context)
        {
        }
    }
}
