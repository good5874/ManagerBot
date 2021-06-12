
using Autofac;

using ManagerBot.Commands;
using ManagerBot.Commands.Abstract;
using ManagerBot.DAL.DataBase;
using ManagerBot.DAL.DataBase.Repositories;
using ManagerBot.DAL.DataBase.Repositories.Abstract;
using ManagerBot.Models.Constants;
using ManagerBot.Services;

using Telegram.Bot;

namespace ManagerBot
{
    public class AutofacConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            #region Repositories
            builder.RegisterType<UserRepository>()
                .As<IUserRepository>();
            builder.RegisterType<AreaRepository>()
                .As<IAreaRepository>();
            builder.RegisterType<ProductsCatalogRepository>()
                .As<IProductsCatalogRepository>();
            #endregion
            #region Commands
            builder.RegisterType<FirstVisitCommand>()
                .As<IBaseCommand>();
            builder.RegisterType<RegistrationCommand>()
                .As<IBaseCommand>();
            builder.RegisterType<AreasCommand>()
                .As<IBaseCommand>();
            builder.RegisterType<ProductCommand>()
                .As<IBaseCommand>();
            #endregion
            #region DbContext
            builder.RegisterType<BotDbContext>()
                .AsSelf();
            #endregion
            #region Services
            builder.RegisterType<MessageProcessingService>()
                .AsSelf();
            #endregion
            #region TelegramBot
            builder.RegisterType<TelegramBotClient>()
                .As<ITelegramBotClient>()
                .WithParameter("token", SettingsConstant.TelegramBotToken)
                .SingleInstance();
            #endregion

            return builder.Build();
        }
    }
}
