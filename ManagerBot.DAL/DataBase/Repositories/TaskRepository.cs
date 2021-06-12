using ManagerBot.DAL.DataBase.Repositories.Abstract;
using ManagerBot.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerBot.DAL.DataBase.Repositories
{
    public class TaskRepository
        : BaseRepository<TaskEntity>,
          ITaskRepository
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
    }
}
