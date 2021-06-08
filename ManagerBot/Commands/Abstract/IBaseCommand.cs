using ManagerBot.Models;

using Telegram.Bot.Args;

namespace ManagerBot.Commands.Abstract
{
    public interface IBaseCommand
    {
        public string Name { get; }
        public RequestResultModel Execute(MessageEventArgs message, UserModel user);
    }
}
