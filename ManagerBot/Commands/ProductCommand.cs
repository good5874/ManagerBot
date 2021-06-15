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
    public class ProductCommand : BaseCommand
    {
        private readonly IProductsCatalogRepository productsCatalogRepository;
        private readonly IAreaRepository areaRepository;

        public override string Name => String.Empty;

        public ProductCommand(IProductsCatalogRepository productsCatalogRepository,
            IAreaRepository areaRepository)
        {
            this.productsCatalogRepository = productsCatalogRepository;
            this.areaRepository = areaRepository;
        }

        public override List<UserEvent> Events => new List<UserEvent>()
        {
            UserEvent.ProductSelecting,
        };

        public async override Task<RequestResultModel> ExecuteAsync(string message, UserEntity user)
        {
            if (message == "Вернуться")
            {
                user.CurrentEvent = UserEvent.AreasSelecting;

                user.CurrentAreaId = -1;
                user.CurrentProductId = -1;
                user.CurrentOperationId = -1;

                return new RequestResultModel()
                {
                    Message = "Выберите участок",
                    User = user,
                    Buttons = (await areaRepository.GetAsync()).ConvertToTelegramButtons()
                };
            }

            var selectedProduct = productsCatalogRepository.GetWithInclude(x => x.Name == message, z => z.OperationCatalog).FirstOrDefault();

            if (selectedProduct == null)
            {
                return new RequestResultModel()
                {
                    Message = "Вы выбрали несуществующий продукт!",
                    User = user
                };
            }

            user.CurrentProductId = selectedProduct.Id;
            user.CurrentEvent = UserEvent.OperationSelecting;

            return new RequestResultModel()
            {
                Message = "Выберите операцию",
                User = user,
                Buttons = selectedProduct.OperationCatalog.ConvertToTelegramButtons()
            };
        }
    }
}
