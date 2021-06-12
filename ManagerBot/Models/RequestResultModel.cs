using ManagerBot.DAL.Entities;
using Telegram.Bot.Types.ReplyMarkups;

namespace ManagerBot.Models
{
    public class RequestResultModel
    {
        public string Message { get; set; }
        public UserEntity User { get; set; }
        public InlineKeyboardMarkup Buttons { get; set; }
    }
}
