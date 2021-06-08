using Autofac;

using ManagerBot.Services;

using System;

namespace ManagerBot
{
    class Program
	{
		static void Main(string[] args)
		{
			var container = AutofacConfig.ConfigureContainer();

			var messageProcessingService = container.Resolve<MessageProcessingService>();
			messageProcessingService.StartProcessing();

			Console.ReadLine();
		}

	}
}
