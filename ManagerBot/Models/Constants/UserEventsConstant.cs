using ManagerBot.DAL.Entities.Enums;

using System.Collections.Generic;

namespace ManagerBot.Models.Constants
{
    public static class UserEventsConstant
    {
        public static Dictionary<UserEvent, UserEvent> BackEvents { get; } = new Dictionary<UserEvent, UserEvent>()
        {
            { UserEvent.OperationSelecting, UserEvent.BackOperation },
            { UserEvent.ProductSelecting, UserEvent.BackProduct },
            { UserEvent.AreasSelecting, UserEvent.BackAreas }
        };
    }
}
