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
            client.OnCallbackQuery += Client_OnCallbackQuery;
            client.StartReceiving();
        }

        private async void Client_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            if (e.CallbackQuery.From.IsBot) return;

            var currentUser = userRepository
                .FindByTelegramId(e.CallbackQuery.From.Id);

            var result = await Commands
                .First(x => x.OnContains(e.CallbackQuery.Message.Text, currentUser))
                .ExecuteAsync(e.CallbackQuery.Data, currentUser ?? new UserEntity());

            if (result != null)
            {
                await client.SendTextMessageAsync(
                      e.CallbackQuery.Message.Chat.Id,
                      result.Message,
                      replyMarkup: result.Buttons);

                await userRepository
                    .UpdateAsync(result.User);
            }
        }

        private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            if (messageEventArgs.Message.From.IsBot) return;

            var currentUser = userRepository
                .FindByTelegramId(messageEventArgs.Message.From.Id);

            var result = await Commands
                .First(x => x.OnContains(messageEventArgs.Message.Text, currentUser))
                .ExecuteAsync(messageEventArgs.Message.Text, currentUser ?? new UserEntity());

            if(result != null)
            {
                await client.SendTextMessageAsync(
                      messageEventArgs.Message.Chat.Id,
                      result.Message,
                      replyMarkup: result.Buttons);

                if (currentUser == null)
                {
                    result.User.TelegramId = messageEventArgs.Message.From.Id;
                    await userRepository
                        .CreateAsync(result.User);
                }
                else
                {
                    await userRepository
                        .UpdateAsync(result.User);
                }
            }

            //var currentUser = userRepository
            //    .FindByTelegramId(messageEventArgs.Message.From.Id);

            //bool isNewUser = currentUser == null ? true : false;

            //var command = Commands
            //    .FirstOrDefault(x => x.Name == messageEventArgs.Message.Text);

            //if(command != null )
            //{
            //    var result = await command.ExecuteAsync(
            //        messageEventArgs.Message.Text,
            //        currentUser ?? new UserEntity());

            //    result.User.TelegramId = messageEventArgs.Message.From.Id;

            //    await client.SendTextMessageAsync(
            //        messageEventArgs.Message.Chat.Id,
            //        result.Message,
            //        replyMarkup: result.Buttons);

            //    if (isNewUser)
            //    {
            //        await userRepository.CreateAsync(result.User);
            //    }
            //    else
            //    {
            //        await userRepository.UpdateAsync(result.User);
            //    }

            //    return;
            //}

            //if(currentUser != null)
            //{
            //    command = Commands
            //        .Where(x => x.Events != null)
            //        .FirstOrDefault(x => x.Events.Contains(currentUser.CurrentEvent.GetValueOrDefault()));

            //    var result = await command.ExecuteAsync(
            //        messageEventArgs.Message.Text,
            //        currentUser);

            //    await client.SendTextMessageAsync(
            //        messageEventArgs.Message.Chat.Id,
            //        result.Message,
            //        replyMarkup: result.Buttons);

            //    await userRepository.UpdateAsync(result.User);

            //    return;
            //}

            //command = Commands
            //    .FirstOrDefault(x => x.Events == null);

            //var newUserResult = await command.ExecuteAsync(
            //    messageEventArgs.Message.Text,
            //    new UserEntity());

            //newUserResult.User.TelegramId = messageEventArgs.Message.From.Id;

            //if (!string.IsNullOrEmpty(newUserResult.Message))
            //{
            //    await client.SendTextMessageAsync(
            //        messageEventArgs.Message.Chat.Id,
            //        newUserResult.Message,
            //        replyMarkup: newUserResult.Buttons);

            //    await userRepository.CreateAsync(newUserResult.User);
            //}
        }
    }
}
