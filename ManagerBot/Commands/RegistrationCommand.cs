using ManagerBot.Commands.Abstract;
using ManagerBot.DAL.DataBase.Repositories.Abstract;
using ManagerBot.DAL.Entity;
using ManagerBot.DAL.Entity.Enums;
using ManagerBot.Models;

using System.Collections.Generic;
using System.Linq;

using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace ManagerBot.Commands
{
    public class RegistrationCommand : IBaseCommand
    {
        private readonly IAreaRepository areaRepository;

        public RegistrationCommand(IAreaRepository areaRepository)
        {
            this.areaRepository = areaRepository;
        }

        public string Name { get; } = "Регистрация";

        public List<UserEvent> Events => new List<UserEvent>()
        {
            UserEvent.FirstVisit, UserEvent.Registration
        };

        public RequestResultModel Execute(string message, UserEntity user)
        {
            if (user.CurrentEvent == UserEvent.FirstVisit)
            {
                if(message == Name)
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
                user.CurrentEvent = UserEvent.AreasSelecting;
            }
            
            var areas = areaRepository.GetAsync().Result;

            var buttons = new List<List<InlineKeyboardButton>>();

            int processesCount = 0;
            while (areas.Count() > processesCount)
            {
                var areasButtonsLine = areas.Skip(processesCount).Take(3);

                buttons.Add(new List<InlineKeyboardButton>() { });

                foreach (var area in areasButtonsLine)
                {
                    buttons.Last().Add(InlineKeyboardButton.WithCallbackData(area.Name));
                }

                processesCount += 3;
            }

            return new RequestResultModel()
            {
                Message = "Выберите зону.",
                User = user,
                Buttons = new InlineKeyboardMarkup(buttons)
            };


        }
    }
}
