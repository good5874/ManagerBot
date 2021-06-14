using ManagerBot.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagerBot.DAL.DataBase.Repositories.Abstract
{
    public interface IUserRepository
        : IBaseRepository<UserEntity>
    {
        UserEntity FindByTelegramId(int telegramId);
        Task<IEnumerable<UserEntity>> GetUsersWithRolesAsync();
        UserEntity FindByTelegramIdWithIncludes(int telegramId);
    }
}
