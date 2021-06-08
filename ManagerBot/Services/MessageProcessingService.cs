using ManagerBot.Models;

using System.Collections.Generic;

using Telegram.Bot;

namespace ManagerBot.Services
{
    public class MessageProcessingService
    {
        private readonly TelegramBotClient client;
        private static List<User> CurrentUsers { get; set; }
        public MessageProcessingService(TelegramBotClient client)
        {
            this.client = client;
        }

        public void StartProcessing()
        {

        }
    }
}
