using System.Globalization;
using RecursiveCalcEngine;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace TelegramBot;

public class Program
{
    public static async Task Main(string[] args)
    {
        var offset = 0;
        var apiKey = Environment.GetEnvironmentVariable("API_KEY")!;
        var telegramBot = new TelegramBotClient(apiKey);
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        Console.WriteLine("bot run!");

        while (true)
        {
            var updates = await telegramBot.GetUpdatesAsync(offset);
            await Task.Delay(TimeSpan.FromSeconds(5));

            foreach (var update in updates)
            {
                if (update is null || update.Message is null || string.IsNullOrWhiteSpace(update.Message.Text))
                    continue;

                var response = string.Empty;
                try
                {
                    var result = Calc.Solve(Calc.Clean(update.Message.Text));
                    response = $"result: `{Math.Round(result.Result, 4)}`\n\nhistory:\n{string.Join("\n",
                        result.History.Select((l, i) => $"\t{++i}. {l.Substring(0, l.IndexOf(new CalcConfig().EqualsDigits.First()) + 1) + "`" +
                            l.Substring(l.IndexOf(new CalcConfig().EqualsDigits.First()) + 1) + "`"}"))}".Replace("*", "\\*").Replace("+-", "-");
                }
                catch (Exception ex)
                {
                    response = ex.ToString();
                }

                await telegramBot.SendTextMessageAsync(update.Message.Chat, response, parseMode: ParseMode.Markdown);

                offset = update.Id + 1;
            }
        }
    }
}