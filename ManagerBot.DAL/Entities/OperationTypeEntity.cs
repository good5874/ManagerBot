using System.Collections.Generic;

namespace ManagerBot.DAL.Entities
{
    public class OperationTypeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<OperationEntity>? Operations { get; set; }
        public List<TaskEntity>? TasksWithoutOperations { get; set; }
    }
}
