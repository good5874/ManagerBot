using ManagerBot.Commands.Abstract;
using ManagerBot.DAL.DataBase.Repositories.Abstract;
using ManagerBot.DAL.Entities;
using ManagerBot.DAL.Entities.Enums;
using ManagerBot.Mappers;
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
        private readonly IProductsCatalogRepository productsCatalogRepository;
        private readonly ITaskRepository taskRepository;

        public override string Name => string.Empty;

        public OperationCommand(
            IOperationCatalogRepository operationCatalogRepository,
            IProductsCatalogRepository productsCatalogRepository,
            ITaskRepository taskRepository)
        {
            this.operationCatalogRepository = operationCatalogRepository;
            this.productsCatalogRepository = productsCatalogRepository;
            this.taskRepository = taskRepository;
        }

        public override List<UserEvent> Events => new List<UserEvent>()
        {
            UserEvent.OperationSelecting
        };

        public async override Task<RequestResultModel> ExecuteAsync(string message, UserEntity user)
        {
            if (message == "Вернуться")
            {
                user.CurrentEvent = UserEvent.ProductSelecting;
                user.CurrentProductId = -1;
                user.CurrentOperationId = -1;

                return new RequestResultModel()
                {
                    Message = "Выберите продукт",
                    User = user,
                    Buttons = productsCatalogRepository
                                .GetWithInclude(x => x.Area.Id == user.CurrentAreaId, z => z.Area)
                                .ConvertToTelegramButtons()
                };
            }

            var selectedOperation = operationCatalogRepository
                .GetWithInclude(x => x.Name == message
                    && x.Product.Id ==user.CurrentProductId
                    && x.Product.Area.Id == user.CurrentAreaId,
                    z=> z.Product,
                    c =>c.Product.Area)
                .FirstOrDefault();

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

            user.CurrentOperationId = selectedOperation.Id;
            user.CurrentEvent = UserEvent.Work;

            return new RequestResultModel()
            {
                Message = "Введите количество выполненных операций:",
                User = user,
            };
        }
    }
}
