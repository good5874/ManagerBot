using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace ManagerBot
{
	class Program
	{
		static void Main(string[] args)
		{
			Bot bot = new Bot();
			bot.Start();
		}

	}

	public class Bot
    {
		private TelegramBotClient client;
		public void Start ()
		{
			client = new TelegramBotClient("1854950611:AAHE-_rO_PmkM8d8RZK1kSoRgMI0_6LtroU");
			client.OnMessage += BotOnMessageReceived;
			client.OnMessageEdited += BotOnMessageReceived;
			client.StartReceiving();
			Console.ReadLine();
			client.StopReceiving();
		}

		private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
		{
			var message = messageEventArgs.Message;
			if (message?.Type == MessageType.Text)
			{
				await client.SendTextMessageAsync(message.Chat.Id, message.Text);
			}
		}
	}
}
