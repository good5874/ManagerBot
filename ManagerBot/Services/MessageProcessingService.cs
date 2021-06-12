using ManagerBot.Commands.Abstract;
using ManagerBot.DAL.DataBase.Repositories.Abstract;
using ManagerBot.Services.Abstract;
using System.Collections.Generic;
using Telegram.Bot;

namespace ManagerBot.Services
{
    public class MessageProcessingService : AbstractMessageProcessingService
    {
        public MessageProcessingService(ITelegramBotClient client,
            IUserRepository userRepository,
            IEnumerable<IBaseCommand> commands)
            : base(client, userRepository, commands) { }
    }
}
