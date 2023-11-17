namespace TelegramBot.Common;
public class ConsoleExceptionLogger
{
    public static async void LogException(Exception ex)
    {
        await Task.Run(() =>
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error({ex.GetType().FullName}): \n\t{ex.Message}");
            Console.ResetColor();
        });
    }
}
