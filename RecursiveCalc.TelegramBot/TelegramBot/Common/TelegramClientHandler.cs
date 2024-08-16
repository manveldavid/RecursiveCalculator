using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Polling;
using RecursiveCalcEngine;

namespace TelegramBot.Common
{
    public class TelegramClientHandler
    {
        public static TelegramBotClient TelegramClient { get; set; } = default!;

        public static void Start()
        {
            var resiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new UpdateType[]
                {
                    UpdateType.Message,
                    UpdateType.EditedMessage
                }
            };

            if (TelegramClient == null) { throw new ArgumentNullException("TelegramClient is null"); }

            TelegramClient.StartReceiving(UpdateHandler, ErrorHandler, resiverOptions);
        }

        private static Task ErrorHandler(
            ITelegramBotClient telegramClient, Exception ex, CancellationToken cancellationToken)
        {
            return Task.Run( //Async log to console any telegramClient exception
                () => ConsoleExceptionLogger.LogException(ex),
                cancellationToken);
        }

        private static async Task UpdateHandler(
            ITelegramBotClient telegramClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            if (message == null) return;
            if (string.IsNullOrWhiteSpace(message.Text)) return;

            var chat = message.Chat;
            var i = 0;
            try
            {
                var result = Calc.Solve(Calc.Clean(message.Text));
                var response = $"result: `{Math.Round(result.Result,4)}`\n\nhistory:\n{string.Join("\n", 
                    result.History.Select(l => $"\t{++i}. {
                        l.Substring(0,l.IndexOf(new CalcConfig().EqualsDigits.First())+1)+"`"+
                        l.Substring(l.IndexOf(new CalcConfig().EqualsDigits.First())+1) + "`"
                        }"))}".Replace("*","\\*").Replace("+-","-");
                await telegramClient.SendTextMessageAsync(chat, response, parseMode:ParseMode.Markdown);
            }
            catch(Exception ex) 
            {
                ConsoleExceptionLogger.LogException(ex);
            }
        }
    }
}
