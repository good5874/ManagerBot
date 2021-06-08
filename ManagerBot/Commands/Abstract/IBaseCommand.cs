using ManagerBot.DAL.Entity;
using ManagerBot.DAL.Entity.Enums;
using ManagerBot.Models;

using System.Collections.Generic;

using Telegram.Bot.Args;

namespace ManagerBot.Commands.Abstract
{
    public interface IBaseCommand
    {
        public string Name { get; }
        List<UserEvent> Events { get; }

        public RequestResultModel Execute(MessageEventArgs message, UserEntity user);
    }
}
