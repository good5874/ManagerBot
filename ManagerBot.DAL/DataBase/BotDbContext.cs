using ManagerBot.DAL.Entities;

using Microsoft.EntityFrameworkCore;

namespace ManagerBot.DAL.DataBase
{
    public class BotDbContext : DbContext
    {
        public DbSet<AreaEntity> Areas { get; set; }
        public DbSet<OperationCatalogEntity> OperationCatalog { get; set; }
        public DbSet<ProductCatalogEntity> ProductCatalog { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<CastomTaskEntity> CastomTasks { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserRolesEntity> UserRoles { get; set; }

        public BotDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=tcp:trainingportal.database.windows.net,1433;Initial Catalog=ManagerBot;Persist Security Info=False;User ID=trainingportal;Password=123zxcVBNM;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            optionsBuilder.UseSqlServer("Server=tcp:trainingportal.database.windows.net,1433;Initial Catalog=ManagerBot;Persist Security Info=False;User ID=trainingportal;Password=123zxcVBNM;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}
