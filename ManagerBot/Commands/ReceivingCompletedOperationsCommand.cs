using ManagerBot.Commands.Abstract;
using ManagerBot.DAL.DataBase.Repositories.Abstract;
using ManagerBot.DAL.Entities;
using ManagerBot.DAL.Entities.Enums;
using ManagerBot.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace ManagerBot.Commands
{
    public class ReceivingCompletedOperationsCommand : BaseCommand
    {
        private readonly ITaskRepository taskRepository;

        public ReceivingCompletedOperationsCommand(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        public override string Name => string.Empty;

        public override List<UserEvent> Events => new List<UserEvent>()
        {
            UserEvent.Work,
        };

        public async override Task<RequestResultModel> ExecuteAsync(string message, UserEntity user)
        {

            var notCompletedTasks = taskRepository
                .GetWithInclude(x => x.UserId == user.Id && x.AmountOperations == 0,
                                    z => z.Operation, c => c.Operation.Product, v => v.Operation.Product.Area);

            if(notCompletedTasks.Count() > 1)
            {
                return new RequestResultModel()
                {
                    Message = $"Ошибка, не удалось отправить"
                };
            }

            var notCompletedTask = notCompletedTasks.FirstOrDefault();

            if (int.TryParse(message, out int amountOperations))
            {
                notCompletedTask.AmountOperations = amountOperations;

                await taskRepository
                    .UpdateAsync(notCompletedTask);

                user.CurrentEvent = UserEvent.OperationSelecting;
                user.CurrentOperationId = -1;

                return new RequestResultModel()
                {
                    Message = $"Ваша работа отправлена мастеру на проверку!\n" +
                    $"Участок: {notCompletedTask.Operation.Product.Area.Name}\n" +
                    $"Продукт: {notCompletedTask.Operation.Product.Name}\n" +
                    $"Операция: {notCompletedTask.Operation.Name}\n" +
                    $"Количество выполненных операций: {notCompletedTask.AmountOperations}",
                    User = user,
                    Buttons = new InlineKeyboardMarkup(
                        InlineKeyboardButton.WithCallbackData("Отменить", $"Отменить {notCompletedTask.Date}" +
                        $" {notCompletedTask.AmountOperations}" +
                        $" {notCompletedTask.UserId}"))
                };
            }
            else
            {
                return new RequestResultModel()
                {
                    Message = "Вы ввели не число",
                    User = user
                };
            }
        }
    }
}
