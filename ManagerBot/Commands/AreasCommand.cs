using ManagerBot.Commands.Abstract;
using ManagerBot.DAL.DataBase.Repositories.Abstract;
using ManagerBot.DAL.Entity;
using ManagerBot.DAL.Entity.Enums;
using ManagerBot.Models;

using System.Collections.Generic;
using System.Linq;

namespace ManagerBot.Commands
{
    public class AreasCommand : IBaseCommand
    {
        private readonly IAreaRepository areaRepository;

        public string Name => string.Empty;

        public AreasCommand(IAreaRepository areaRepository)
        {
            this.areaRepository = areaRepository;
        }

        public List<UserEvent> Events => new List<UserEvent>()
        {
            UserEvent.AreasSelecting
        };

        public RequestResultModel Execute(string message, UserEntity user)
        {
            var areas = areaRepository.GetAsync().Result;

            var selectedArea = areas.FirstOrDefault(x => x.Name == message);
        }
    }
}
