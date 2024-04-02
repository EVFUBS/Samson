namespace SamsonConsoleApp.Helpers
{
    public static class Logger
    {
        public static void Log(string message, params object[] args)
        {
            // implement serialog here at some point
            Console.WriteLine(message, args);
        }
    }
}
