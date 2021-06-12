using ManagerBot.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagerBot.DAL.DataBase.Repositories.Abstract
{
    public interface ITaskRepository : IBaseRepository<TaskEntity>
    {
        public Task<IEnumerable<TaskEntity>> GetTaskWithIncludesAsync();
        public Task<IEnumerable<TaskEntity>> GetNotCompletedTasksWithIncludes();
        public Task<TaskEntity> GetNotCompletedTaskWithIncludesByUserId(int userId);
        public Task<TaskEntity> GetTaskWithIncludesByTaskId(int taskId);
    }
}
