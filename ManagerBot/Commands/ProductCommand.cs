using ManagerBot.Commands.Abstract;
using ManagerBot.DAL.DataBase.Repositories.Abstract;
using ManagerBot.DAL.Entity;
using ManagerBot.DAL.Entity.Enums;
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

            var selectedProduct = products.FirstOrDefault(x => x.Name == message);

            if (selectedProduct == null)
            {
                return new RequestResultModel()
                {
                    Message = "Выберите участок!",
                    User = user
                };
            }

            var buttons = new List<List<InlineKeyboardButton>>();

            int processesCount = 0;
            while (selectedProduct.OperationCatalog.Count() > processesCount)
            {
                var productsButtonsLine = selectedProduct.OperationCatalog.Skip(processesCount).Take(3);

                buttons.Add(new List<InlineKeyboardButton>() { });

                foreach (var product in productsButtonsLine)
                {
                    buttons.Last().Add(InlineKeyboardButton.WithCallbackData(product.Name.Trim().Replace("\n", "").Replace("\r", "")));
                }

                processesCount += 3;
            }

            return new RequestResultModel()
            {
                Message = "Выберите операцию.",
                User = user,
                Buttons = new InlineKeyboardMarkup(buttons)
            };
        }
    }
}
