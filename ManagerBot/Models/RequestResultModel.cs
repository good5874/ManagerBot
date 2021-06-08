using ManagerBot.DAL.Entity;

namespace ManagerBot.Models
{
    public class RequestResultModel
    {
        public string Message { get; set; }
        public UserEntity User { get; set; }
    }
}
