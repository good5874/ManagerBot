using ManagerBot.Commands.Abstract;
using ManagerBot.DAL.Entities;
using ManagerBot.DAL.Entity.Enums;
using ManagerBot.Models;
using ManagerBot.Models.Constants;

using System.Collections.Generic;
using System.Threading.Tasks;

using Telegram.Bot.Types.ReplyMarkups;

namespace ManagerBot.Commands
{
    public class FirstVisitCommand : BaseCommand
    {
        public override string Name { get; } = string.Empty;

        public override List<UserEvent> Events => null;

        public override bool OnContains(string message, UserEntity user)
        {
            return user == null;
        }
        public override async Task<RequestResultModel> ExecuteAsync(string message, UserEntity user)
        {
            if (message.Contains("/start " + SettingsConstant.InviteCode.ToString()))
            {
                user.CurrentEvent = UserEvent.FirstVisit;

                return new RequestResultModel()
                {
                    Message = "Добро пожаловать в ManagerBot",
                    User = user,
                    Buttons = new InlineKeyboardMarkup(new List<List<InlineKeyboardButton>>()
                    {
                        new List<InlineKeyboardButton>()
                        {
                            InlineKeyboardButton.WithCallbackData("Регистрация")
                        }
                    })
                };
            }

            return null;
        }
    }
}
