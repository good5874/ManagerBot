using ManagerBot.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagerBot.DAL.DataBase.Repositories.Abstract
{
    public interface IUserRepository
        : IBaseRepository<UserEntity>
    {
        UserEntity FindByTelegramId(int telegramId);
        public Task<IEnumerable<UserEntity>> GetUsersWithRolesAsync();
    }
}
