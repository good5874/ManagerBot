using Autofac;
using ManagerBot.DAL.DataBase;

namespace ManagerBot.DI
{
    public class AutofacConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            #region Repositories

            #endregion
            #region DbContext
            builder.RegisterType<BotDbContext>()
                .AsSelf();
            #endregion

            return builder.Build();
        }
    }
}
