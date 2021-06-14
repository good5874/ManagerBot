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
            UserEvent.BackProduct
        };

        public override async Task<RequestResultModel> ExecuteAsync(string message, UserEntity user)
        {
            var areas = await areaRepository.GetAreasWithIncludesAsync();

            var selectedArea = areas.FirstOrDefault(x => x.Name == message);

            if (user.CurrentEvent == UserEvent.BackProduct)
            {
                var currentArea = areas.FirstOrDefault(x => x.Id == user.CurrentArea.Id);

                return GetResult(currentArea, user);
            }
            if (selectedArea == null)
            {
                return new RequestResultModel()
                {
                    Message = "Вы выбрали не существующий участок!",
                    User = user
                };
            }

            return GetResult(selectedArea, user);
        }

        private RequestResultModel GetResult(AreaEntity area, UserEntity user)
        {
            user.CurrentEvent = UserEvent.ProductSelecting;

            return new RequestResultModel()
            {
                Message = "Выберите продукт.",
                User = user,
                Buttons = area.ProductCatalog.ConvertToTelegramButtons()
            };
        }
    }
}
