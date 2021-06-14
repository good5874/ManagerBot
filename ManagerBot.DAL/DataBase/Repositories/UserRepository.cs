﻿using ManagerBot.DAL.DataBase.Repositories.Abstract;
using ManagerBot.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<UserEntity>> GetUsersWithRolesAsync()
        {
            return await context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(r => r.Role)
                .ToListAsync();
        }

        public UserEntity FindByTelegramIdWithIncludes(int telegramId)
        {
            return context.Users
                .Include(c => c.Tasks)
                .Include(c => c.CurrentOperation)
                .Include(c => c.CurrentProduct)
                .Include(c => c.CurrentArea)
                .Include(c => c.UserRoles)
                .FirstOrDefault(x => x.TelegramId == telegramId);
        }
    }
}
