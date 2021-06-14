using ManagerBot.Commands.Abstract;
using ManagerBot.DAL.DataBase.Repositories.Abstract;
using ManagerBot.DAL.Entities;
using ManagerBot.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace ManagerBot.Services.Abstract
{
    public abstract class AbstractMessageProcessingService : IDisposable
    {
        protected ITelegramBotClient client;
        protected IUserRepository userRepository;
        protected List<IBaseCommand> commands;

        protected UserEntity CurrentUser { get; set; }
        protected RequestResultModel Result { get; set; }
        private static ReplyKeyboardMarkup mainMenu { get; } = new ReplyKeyboardMarkup(new KeyboardButton("Назад"));

        public AbstractMessageProcessingService(
            ITelegramBotClient client,
            IUserRepository userRepository,
            IEnumerable<IBaseCommand> commands)
        {
            this.client = client;
            this.userRepository = userRepository;
            this.commands = commands.ToList();
        }

        public virtual async Task Start(int telegramId, long chatId, string nameCommand)
        {
            SetUser(telegramId);
            await ExecuteCommand(nameCommand);
            await Finish(telegramId, chatId);
        }

        protected virtual void SetUser(int telegramId)
        {
            CurrentUser = userRepository.FindByTelegramIdWithIncludes(telegramId);
        }

        protected async virtual Task ExecuteCommand(string nameCommand)
        {
            Result = await commands
                .First(x => x.OnContains(nameCommand, CurrentUser))
                .ExecuteAsync(nameCommand, CurrentUser ?? new UserEntity());
        }

        protected async virtual Task Finish(int telegramId, long chatId)
        {
            if (Result != null)
            {
                await client.SendTextMessageAsync(
                      chatId,
                      Result.Message,
                      replyMarkup: Result.Buttons);

                await client.SendTextMessageAsync(
                      chatId,
                      string.Empty,
                      replyMarkup: mainMenu);

                if (CurrentUser == null)
                {
                    Result.User.TelegramId = telegramId;
                    await userRepository
                        .CreateAsync(Result.User);
                }
                else
                {
                    await userRepository
                        .UpdateAsync(Result.User);
                }
            }
        }

        public void Dispose()
        {
            client = null;
            userRepository = null;
            commands = null;
            CurrentUser = null;
            Result = null;
        }
    }
}
