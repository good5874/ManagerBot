using ManagerBot.DAL.Entities;
using ManagerBot.DAL.Entities.Enums;
using ManagerBot.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagerBot.Commands.Abstract
{
    public interface IBaseCommand
    {
        List<UserEvent> Events { get; }
        bool OnContains(string message, UserEntity user);
        Task<RequestResultModel> ExecuteAsync(string message, UserEntity user);
    }
}
