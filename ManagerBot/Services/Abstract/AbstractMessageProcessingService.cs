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
        protected ITaskRepository taskRepository;
        protected List<IBaseCommand> commands;

        protected UserEntity CurrentUser { get; set; }
        protected RequestResultModel Result { get; set; }
        private static ReplyKeyboardMarkup mainMenu { get; } =
            new ReplyKeyboardMarkup
            {
                Keyboard = new[]
                {
                                new[]
                                {
                                    new KeyboardButton("Профиль"),
                                    new KeyboardButton("Вернуться"),
                                }
                }
            };

        public AbstractMessageProcessingService(
            ITelegramBotClient client,
            IUserRepository userRepository,
            ITaskRepository taskRepository,
            IEnumerable<IBaseCommand> commands)
        {
            this.client = client;
            this.userRepository = userRepository;
            this.taskRepository = taskRepository;
            this.commands = commands.ToList();
        }

        public virtual async Task Start(int telegramId, long chatId, string nameCommand)
        {
            try
            {
                if (nameCommand == "Вернуться")
                {
                    await client.SendTextMessageAsync(
                            chatId,
                            "Закончите пожалуйста регистрацию",
                            replyMarkup: mainMenu);
                    return;
                }

                if (nameCommand.Contains("Отменить"))
                {
                    var ms = nameCommand.Split();
                    var task = taskRepository.GetWithInclude(x => x.Date.ToString() == ms[1] + " " + ms[2] &&
                                                x.AmountOperations.ToString() == ms[3] &&
                                                x.UserId.ToString() == ms[4],
                                                z=>z.Operation).FirstOrDefault();
                    if (task != null)
                    {
                        await client.SendTextMessageAsync(
                                chatId,
                                "Отчёт по операции: " + task.Operation.Name + ", был удалён",
                                replyMarkup: mainMenu);

                        await taskRepository.RemoveAsync(task);
                        return;
                    }
                    if (task == null)
                    {
                        await client.SendTextMessageAsync(
                                chatId,
                                "Не удалось удалить отчёт по операции",
                                replyMarkup: mainMenu);
                        return;
                    }
                }

                SetUser(telegramId);

                if (nameCommand.Contains("Профиль"))
                {
                    if (CurrentUser.FullName == null)
                    {
                        await client.SendTextMessageAsync(
                                chatId,
                                "Профиль не доступен",
                                replyMarkup: mainMenu);
                        return;
                    }
                    if (CurrentUser != null)
                    {
                        await client.SendTextMessageAsync(
                                chatId,
                                $"ФИО: {CurrentUser.FullName}\n" +
                                "Ваш счёт: " + CurrentUser.Salary,
                                replyMarkup: mainMenu);
                        return;
                    }
                }

                await ExecuteCommand(nameCommand);
                await Finish(telegramId, chatId);
            }
            catch
            {
                await client.SendTextMessageAsync(
                        chatId,
                        "Команды не существует",
                        replyMarkup: mainMenu);
            }

        }

        protected virtual void SetUser(int telegramId)
        {
            CurrentUser = userRepository.FindByTelegramId(telegramId);
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
                if (Result.Buttons != null)
                {
                    await client.SendTextMessageAsync(
                        chatId,
                        Result.Message,
                        replyMarkup: Result.Buttons);
                }
                else
                {
                    await client.SendTextMessageAsync(
                        chatId,
                        Result.Message,
                        replyMarkup: mainMenu);
                }

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
