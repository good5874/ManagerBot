using ManagerBot.DAL.Entities;

namespace ManagerBot.DAL.DataBase.Repositories.Abstract
{
    public interface IUserRepository
        : IBaseRepository<UserEntity>
    {
        UserEntity FindByTelegramId(int telegramId);
    }
}
