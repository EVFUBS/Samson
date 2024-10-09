namespace SamsonClient.Helpers
{
    public static class Logger
    {
        public static void Log(string message, params object[] args)
        {
            // implement serilog here at some point
            Console.WriteLine(message, args);
        }
    }
}
