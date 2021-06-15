using ManagerBot.Commands.Abstract;
using ManagerBot.DAL.DataBase.Repositories.Abstract;
using ManagerBot.DAL.Entities;
using ManagerBot.DAL.Entities.Enums;
using ManagerBot.Mappers;
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
            UserEvent.AreasSelecting,
        };

        public override async Task<RequestResultModel> ExecuteAsync(string message, UserEntity user)
        {
            var areas = await areaRepository.GetAreasWithIncludesAsync();

            if (message == "Вернуться")
            {
                user.CurrentAreaId = -1;
                user.CurrentProductId = -1;
                user.CurrentOperationId = -1;

                return new RequestResultModel()
                {
                    Message = "Выберите участок",
                    User = user,
                    Buttons = areas.ConvertToTelegramButtons()
                };
            }

            var selectedArea = areas.FirstOrDefault(x => x.Name == message);

            if (selectedArea == null)
            {
                return new RequestResultModel()
                {
                    Message = "Вы выбрали не существующий участок!",
                    User = user
                };
            }

            user.CurrentAreaId = selectedArea.Id;
            user.CurrentEvent = UserEvent.ProductSelecting;

            return new RequestResultModel()
            {
                Message = "Выберите продукт.",
                User = user,
                Buttons = selectedArea.ProductCatalog.ConvertToTelegramButtons()
            };

        }
    }
}
