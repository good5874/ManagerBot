using ManagerBot.DAL.Entities.Abstract;

using System.Collections.Generic;
using System.Linq;

using Telegram.Bot.Types.ReplyMarkups;

namespace ManagerBot.Mappers
{
    public static class TelegramButtonConvert
    {
        public static InlineKeyboardMarkup ConvertToTelegramButtons<T>(this IEnumerable<T> models) where T : IConvertbleToTelegramButton
        {
            var buttons = new List<List<InlineKeyboardButton>>();

            int processesCount = 0;
            while (models.Count() > processesCount)
            {
                var buttonsLine = models.Skip(processesCount).Take(3);

                buttons.Add(new List<InlineKeyboardButton>() { });

                foreach (var button in buttonsLine)
                {
                    string buttonData = button.Id.ToString();
                    string buttonText = button.Name.Length >= 20 ? button.Name.Substring(0, 20) + "..." : button.Name;

                    buttons.Last().Add(InlineKeyboardButton.WithCallbackData(buttonText, buttonData));
                }

                processesCount += 3;
            }

            return new InlineKeyboardMarkup(buttons);
        }
    }
}
