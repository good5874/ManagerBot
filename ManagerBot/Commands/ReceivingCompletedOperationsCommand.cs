using ManagerBot.Commands.Abstract;
using ManagerBot.DAL.DataBase.Repositories.Abstract;
using ManagerBot.DAL.Entities;
using ManagerBot.DAL.Entities.Enums;
using ManagerBot.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

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
            UserEvent.Work
        };

        public async override Task<RequestResultModel> ExecuteAsync(string message, UserEntity user)
        {
            var notCompletedTask = await taskRepository
                .GetNotCompletedTaskWithIncludesByUserId(user.Id);

            if(message == "Отменить")
            {
                await taskRepository.RemoveAsync(notCompletedTask);

                user.CurrentEvent = UserEvent.OperationSelecting;

                return new RequestResultModel()
                {
                    Message = "Операция с названием: " + notCompletedTask.Operation.Name + ", была отменена.",
                    User = user
                };
            }

            if(int.TryParse(message, out int amountOperations))
            {
                notCompletedTask.AmountOperations = amountOperations;

                await taskRepository
                    .UpdateAsync(notCompletedTask);

                user.CurrentEvent = UserEvent.OperationSelecting;

                return new RequestResultModel()
                {
                    Message = "Ваша работа отправлена мастеру на проверку!",
                    User = user
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
