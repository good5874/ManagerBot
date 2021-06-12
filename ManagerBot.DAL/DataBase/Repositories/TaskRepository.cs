using ManagerBot.DAL.DataBase.Repositories.Abstract;
using ManagerBot.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerBot.DAL.DataBase.Repositories
{
    public class TaskRepository
        : BaseRepository<TaskEntity>
        , ITaskRepository
    {
        private readonly BotDbContext context;

        public TaskRepository(BotDbContext context)
            : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TaskEntity>> GetTaskWithIncludesAsync()
        {
            return await context.Tasks
                .Include(u => u.User)
                .Include(o => o.Operation)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskEntity>> GetNotCompletedTasksWithIncludes()
        {
            return await context.Tasks
                .Include(u => u.User)
                .Include(o => o.Operation)
                .Where(x => x.IsFinish == false)
                .ToListAsync();
        }

        public async Task<TaskEntity> GetNotCompletedTaskWithIncludesByUserId(int userId)
        {
            return (await GetTaskWithIncludesAsync())
                .Where(x => x.User.Id == userId)
                .Where(x => x.IsFinish == false)
                .FirstOrDefault();
        }

        public async Task<TaskEntity> GetTaskWithIncludesByTaskId(int taskId)
        {
            return await context.Tasks
                .Include(u => u.User)
                .Include(o => o.Operation)
                .FirstOrDefaultAsync(x => x.Id == taskId);
        }
    }
}
