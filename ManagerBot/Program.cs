using ManagerBot.Services;

using System;

namespace ManagerBot
{
    class Program
	{
		static void Main(string[] args)
		{
			TelegramBotService messageProcessingService = new TelegramBotService();
			messageProcessingService.StartProcessing();

			Console.ReadLine();
		}

	}
}
