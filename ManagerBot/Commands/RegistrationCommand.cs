using ManagerBot.Commands.Abstract;
using ManagerBot.DAL.DataBase.Repositories.Abstract;
using ManagerBot.DAL.Entities;
using ManagerBot.DAL.Entities.Enums;
using ManagerBot.Mappers;
using ManagerBot.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagerBot.Commands
{
    public class RegistrationCommand : BaseCommand
    {
        private readonly IAreaRepository areaRepository;

        public RegistrationCommand(IAreaRepository areaRepository)
        {
            this.areaRepository = areaRepository;
        }

        public override string Name { get; } = "Регистрация";

        public override List<UserEvent> Events => new List<UserEvent>()
        {
            UserEvent.FirstVisit,
            UserEvent.Registration,
        };

        public override bool OnContains(string message, UserEntity user)
        {
            return base.OnContains(message, user);
        }

        public override async Task<RequestResultModel> ExecuteAsync(string message, UserEntity user)
        {
            if (user.CurrentEvent == UserEvent.FirstVisit)
            {
                if(message.Trim() == Name)
                {
                    user.CurrentEvent = UserEvent.Registration;

                    return new RequestResultModel()
                    {
                        Message = "Введите ваше ФИО:",
                        User = user
                    };
                }
            }

            if (string.IsNullOrEmpty(user.FullName))
            {
                if (string.IsNullOrEmpty(message))
                {
                    return new RequestResultModel()
                    {
                        Message = "ФИО не может быть пустым",
                        User = user
                    };
                }

                user.FullName = message;
            }

            var areas = await areaRepository.GetAsync();

            user.CurrentEvent = UserEvent.AreasSelecting;

            return new RequestResultModel()
            {
                Message = "Выберите участок",
                User = user,
                Buttons = areas.ConvertToTelegramButtons()
            };
        }
    }
}
