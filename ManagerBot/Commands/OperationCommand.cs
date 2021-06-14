using ManagerBot.Commands.Abstract;
using ManagerBot.DAL.DataBase.Repositories.Abstract;
using ManagerBot.DAL.Entities;
using ManagerBot.DAL.Entities.Enums;
using ManagerBot.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Telegram.Bot.Types.ReplyMarkups;

namespace ManagerBot.Commands
{
    public class OperationCommand : BaseCommand
    {
        private readonly IOperationCatalogRepository operationCatalogRepository;
        private readonly ITaskRepository taskRepository;

        public override string Name => string.Empty;

        public OperationCommand(
            IOperationCatalogRepository operationCatalogRepository,
            ITaskRepository taskRepository)
        {
            this.operationCatalogRepository = operationCatalogRepository;
            this.taskRepository = taskRepository;
        }

        public override List<UserEvent> Events => new List<UserEvent>()
        {
            UserEvent.OperationSelecting
        };

        public async override Task<RequestResultModel> ExecuteAsync(string message, UserEntity user)
        {
            var operations = await operationCatalogRepository.GetAsync();

            var selectedOperation = operations.FirstOrDefault(x => x.Name.Trim().Replace("\n", "").Replace("\r", "") == message.Trim().Replace("\n", "").Replace("\r", ""));

            if (selectedOperation == null)
            {
                return new RequestResultModel()
                {
                    Message = "Вы выбрали не существующую операцию!",
                    User = user
                };
            }

            await taskRepository.CreateAsync(new TaskEntity()
            {
                AmountOperations = 0,
                Date = DateTime.Now,
                IsFinish = false,
                OperationId = selectedOperation.Id,
                UserId = user.Id
            });

            user.CurrentEvent = UserEvent.Work;

            return new RequestResultModel()
            {
                Message = "Кол-во операций:",
                User = user,
                Buttons = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("Отменить"))
            };
        }
    }
}
