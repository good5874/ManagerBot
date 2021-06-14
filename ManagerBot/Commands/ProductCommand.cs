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

        public override string Name => String.Empty;

        public ProductCommand(IProductsCatalogRepository productsCatalogRepository)
        {
            this.productsCatalogRepository = productsCatalogRepository;
        }

        public override List<UserEvent> Events => new List<UserEvent>()
        {
            UserEvent.ProductSelecting
        };

        public async override Task<RequestResultModel> ExecuteAsync(string message, UserEntity user)
        {
            var products = await productsCatalogRepository.GetProductsWithIncludesAsync();

            var selectedProduct = products.FirstOrDefault(x => x.Name.Trim().Replace("\n", "").Replace("\r", "") == message.Trim().Replace("\n", "").Replace("\r", ""));

            if (user.CurrentEvent == UserEvent.BackProduct)
            {
                var currentProduct = products.FirstOrDefault(x => x.Id == user.CurrentProduct.Id);

                return GetResult(currentProduct, user)
            }
            if (selectedProduct == null)
            {
                return new RequestResultModel()
                {
                    Message = "Вы выбрали несуществующий продукт!",
                    User = user
                };
            }

            return GetResult(selectedProduct, user);
        }

        private RequestResultModel GetResult(ProductCatalogEntity product, UserEntity user)
        {
            user.CurrentEvent = UserEvent.OperationSelecting;

            return new RequestResultModel()
            {
                Message = "Выберите операцию.",
                User = user,
                Buttons = product.OperationCatalog.ConvertToTelegramButtons()
            };
        }
    }
}
