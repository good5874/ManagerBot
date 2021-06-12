using ManagerBot.DAL.DataBase.Repositories.Abstract;
using ManagerBot.DAL.Entities;

using System.Linq;

namespace ManagerBot.DAL.DataBase.Repositories
{
    public class UserRepository
        : BaseRepository<UserEntity>
        , IUserRepository
    {
        private readonly BotDbContext context;

        public UserRepository(BotDbContext context)
            : base(context)
        {
            this.context = context;
        }

        public UserEntity FindByTelegramId(int telegramId)
        {
            return context.Users
                .FirstOrDefault(x => x.TelegramId == telegramId);
        }
    }
}
