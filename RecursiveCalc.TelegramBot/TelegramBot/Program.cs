using System.Globalization;
using Telegram.Bot;
using TelegramBot.Common;

namespace TelegramBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var apiKey = Environment.GetEnvironmentVariable("API_KEY");

            TelegramClientHandler.TelegramClient = 
                new TelegramBotClient(apiKey ?? throw new Exception(nameof(apiKey)));
            TelegramClientHandler.Start();

            Console.WriteLine("Recursive calc run!");
            while (true) ;
        }
    }
}