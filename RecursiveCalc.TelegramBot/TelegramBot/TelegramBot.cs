using Telegram.Bot.Types;
using Telegram.Bot;
using RecursiveCalcEngine;
using Telegram.Bot.Types.Enums;

namespace TelegramBotCalc;

public class TelegramBot
{
    public async Task RunAsync(string apiKey, TimeSpan pollPeriod, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(apiKey))
            return;

        var offset = 0;
        var telegramBot = new TelegramBotClient(apiKey);

        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(pollPeriod, cancellationToken);

            Update[] updates = Array.Empty<Update>();
            try
            {
                updates = await telegramBot.GetUpdates(offset, timeout: (int)pollPeriod.TotalSeconds, cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            if (!cancellationToken.IsCancellationRequested)
            {
                foreach (var update in updates)
                {
                    offset = update.Id + 1;

                    if (update is null || 
                        update.Message is null || 
                        string.IsNullOrEmpty(update.Message.Text))
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

                    await telegramBot.SendMessage(update.Message.Chat, response, parseMode: ParseMode.Markdown, replyParameters: new ReplyParameters { MessageId = update.Message.Id });

                }
            }
        }
    }
}

