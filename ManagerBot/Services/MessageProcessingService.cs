using Autofac;

using ManagerBot.Commands.Abstract;
using ManagerBot.DAL.DataBase.Repositories.Abstract;
using ManagerBot.DAL.Entity;

using System.Collections.Generic;
using System.Linq;

using Telegram.Bot;
using Telegram.Bot.Args;

namespace ManagerBot.Services
{
    public class MessageProcessingService
    {
        private readonly ITelegramBotClient client;
        private readonly IUserRepository userRepository;
        private readonly List<IBaseCommand> Commands;
        public MessageProcessingService(
            ITelegramBotClient client,
            IUserRepository userRepository)
        {
            this.client = client;
            this.userRepository = userRepository;
            Commands = AutofacConfig.ConfigureContainer().Resolve<IBaseCommand[]>().ToList();
        }

        public void StartProcessing()
        {
            client.OnMessage += BotOnMessageReceived;
            client.OnMessageEdited += BotOnMessageReceived;
            client.StartReceiving();
        }

        private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            if (messageEventArgs.Message.From.IsBot) return;

            var currentUser = userRepository
                .FindByTelegramId(messageEventArgs.Message.From.Id);

            bool isNewUser = currentUser == null ? true : false;

            var command = Commands
                .FirstOrDefault(x => x.Name == messageEventArgs.Message.Text);

            if(command != null )
            {
                var result = command.Execute(
                    messageEventArgs,
                    currentUser ?? new UserEntity());

                result.User.TelegramId = messageEventArgs.Message.From.Id;

                await client.SendTextMessageAsync(messageEventArgs.Message.Chat.Id, result.Message);

                if (isNewUser)
                {
                    await userRepository.CreateAsync(result.User);
                }
                else
                {
                    await userRepository.UpdateAsync(result.User);
                }

                return;
            }

            if(currentUser != null)
            {
                command = Commands
                    .Where(x => x.Events != null)
                    .FirstOrDefault(x => x.Events.Contains(currentUser.CurrentEvent.GetValueOrDefault()));

                var result = command.Execute(
                    messageEventArgs,
                    currentUser);

                await client.SendTextMessageAsync(messageEventArgs.Message.Chat.Id, result.Message);

                await userRepository.UpdateAsync(result.User);

                return;
            }

            command = Commands
                .FirstOrDefault(x => x.Events == null);

            var newUserResult = command.Execute(
                messageEventArgs,
                new UserEntity());

            newUserResult.User.TelegramId = messageEventArgs.Message.From.Id;

            await client.SendTextMessageAsync(messageEventArgs.Message.Chat.Id, newUserResult.Message);

            await userRepository.CreateAsync(newUserResult.User);
        }
    }
}
