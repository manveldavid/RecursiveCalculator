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

            TelegramClientHandler.TelegramClient = 
                new TelegramBotClient("6497613927:AAEaPFQKRJ_PhGyBpzzz3HQdeGfKcQ8U5OQ");
            TelegramClientHandler.Start();

            Console.WriteLine("Recursive calc run!");
            while (true) ;
        }
    }
}