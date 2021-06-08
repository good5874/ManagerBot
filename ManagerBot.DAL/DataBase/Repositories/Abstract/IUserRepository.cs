using ManagerBot.DAL.Entity;

namespace ManagerBot.DAL.DataBase.Repositories.Abstract
{
    public interface IUserRepository 
        : IBaseRepository<UserEntity>
    {
        UserEntity FindByTelegramId(int telegramId);
    }
}
