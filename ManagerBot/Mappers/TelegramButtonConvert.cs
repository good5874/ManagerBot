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
                    buttons.Last().Add(InlineKeyboardButton.WithCallbackData(button.Name.Trim().Replace("\n", "").Replace("\r", "")));
                }

                processesCount += 3;
            }

            return new InlineKeyboardMarkup(buttons);
        }
    }
}
