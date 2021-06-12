using ManagerBot.DAL.DataBase.Repositories.Abstract;
using ManagerBot.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerBot.DAL.DataBase.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
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
        public async Task<IEnumerable<UserEntity>> GetUsersWithRolesAsync()
        {
            return await context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(r => r.Role)
                .ToListAsync();
        }
    }
}
