using ManagerBot.Commands.Abstract;
using ManagerBot.DAL.Entity;
using ManagerBot.DAL.Entity.Enums;
using ManagerBot.Models;

using System.Collections.Generic;

using Telegram.Bot.Args;

namespace ManagerBot.Commands
{
    public class FirstVisitCommand : IBaseCommand
    {
        public string Name { get; } = string.Empty;

        public List<UserEvent> Events => null;

        public RequestResultModel Execute(MessageEventArgs message, UserEntity user)
        {
            user.CurrentEvent = UserEvent.FirstVisit;

            return new RequestResultModel()
            {
                Message = "Добро пожаловать в ManagerBot",
                User = user
            };
        }
    }
}
