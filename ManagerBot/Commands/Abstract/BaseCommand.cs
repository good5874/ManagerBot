using ManagerBot.DAL.Entities;
using ManagerBot.DAL.Entities.Enums;
using ManagerBot.Models;
using ManagerBot.Models.Constants;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerBot.Commands.Abstract
{
    public abstract class BaseCommand : IBaseCommand
    {
        public abstract string Name { get; }
        public abstract List<UserEvent> Events { get; }
        public virtual bool OnContains(string message, UserEntity user)
        {
            if (Events.Contains(user.CurrentEvent.GetValueOrDefault()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void ProcessBackCommand(string message, UserEntity user)
        {
            if(message == "Вернуться")
            {
                user.CurrentEvent = UserEventsConstant.BackEvents.GetValueOrDefault(user.CurrentEvent.Value);
            }
        }
        public abstract Task<RequestResultModel> ExecuteAsync(string message, UserEntity user);
    }
}
