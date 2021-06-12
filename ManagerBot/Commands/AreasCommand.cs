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
    public class AreasCommand : BaseCommand
    {
        private readonly IAreaRepository areaRepository;

        public override string Name => string.Empty;

        public AreasCommand(IAreaRepository areaRepository)
        {
            this.areaRepository = areaRepository;
        }

        public override List<UserEvent> Events => new List<UserEvent>()
        {
            UserEvent.AreasSelecting
        };

        public override async Task<RequestResultModel> ExecuteAsync(string message, UserEntity user)
        {
            var areas = await areaRepository.GetAreasWithIncludesAsync();

            var selectedArea = areas.FirstOrDefault(x => x.Name == message);

            if (selectedArea == null)
            {
                return new RequestResultModel()
                {
                    Message = "Вы выбрали не существующий участок!",
                    User = user
                };
            }

            var buttons = new List<List<InlineKeyboardButton>>();

            int processesCount = 0;
            while (selectedArea.ProductCatalog.Count() > processesCount)
            {
                var productsButtonsLine = selectedArea.ProductCatalog.Skip(processesCount).Take(3);

                buttons.Add(new List<InlineKeyboardButton>() { });

                foreach (var product in productsButtonsLine)
                {
                    buttons.Last().Add(InlineKeyboardButton.WithCallbackData(product.Name.Trim().Replace("\n","").Replace("\r", "")));
                }

                processesCount += 3;
            }

            user.CurrentEvent = UserEvent.ProductSelecting;

            return new RequestResultModel()
            {
                Message = "Выберите продукт.",
                User = user,
                Buttons = new InlineKeyboardMarkup(buttons)
            };
        }
    }
}
