using ManagerBot.Commands.Abstract;
using ManagerBot.Models;
using ManagerBot.Models.Enums;

using Telegram.Bot.Args;

namespace ManagerBot.Commands
{
    public class FirstVisitCommand : IBaseCommand
    {
        public string Name { get; } = string.Empty;

        public RequestResultModel Execute(MessageEventArgs message, UserModel user)
        {
            user.CurrentEvent = UserEventsEnum.FirstVisit;

            return new RequestResultModel()
            {
                Message = "Добро пожаловать в ManagerBot",
                User = user
            };
        }
    }
}
