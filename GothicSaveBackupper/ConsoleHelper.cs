namespace GothicSaveBackupper
{
    internal static class ConsoleHelper
    {
        public static void WriteLine(string message, ConsoleColor? color = null)
        {
            if (null != color) {
                Console.ForegroundColor = color.Value;
            }

            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
            Console.ResetColor();
        }
    }
}
