using Autofac;

using Telegram.Bot;
using Telegram.Bot.Args;

namespace ManagerBot.Services
{
    public class TelegramBotService
    {
        private readonly ITelegramBotClient client;
        private readonly IContainer container;


        public TelegramBotService()
        {
            container = AutofacConfig.ConfigureContainer();
            client = container.Resolve<ITelegramBotClient>();
        }

        public void StartProcessing()
        {
            client.OnMessage += BotOnMessageReceived;
            client.OnMessageEdited += BotOnMessageReceived;
            client.OnCallbackQuery += Client_OnCallbackQuery;
            client.StartReceiving();
        }

        private async void Client_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            if (e.CallbackQuery.From.IsBot) return;

            var messageProcessing = container.Resolve<MessageProcessingService>();

            await messageProcessing.Start(
                e.CallbackQuery.From.Id,
                e.CallbackQuery.Message.Chat.Id,
                e.CallbackQuery.Data);

            messageProcessing.Dispose();
        }

        private async void BotOnMessageReceived(object sender, MessageEventArgs e)
        {
            if (e.Message.From.IsBot) return;

            var messageProcessing = container.Resolve<MessageProcessingService>();

            await messageProcessing.Start(
                e.Message.From.Id,
                e.Message.Chat.Id,
                e.Message.Text);

            messageProcessing.Dispose();
        }
    }
}
