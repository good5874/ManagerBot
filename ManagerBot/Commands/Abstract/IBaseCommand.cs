using ManagerBot.DAL.Entity;
using ManagerBot.DAL.Entity.Enums;
using ManagerBot.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

using Telegram.Bot.Args;

namespace ManagerBot.Commands.Abstract
{
    public interface IBaseCommand
    {
        List<UserEvent> Events { get; }

        Task<RequestResultModel> ExecuteAsync(string message, UserEntity user);
    }
}
