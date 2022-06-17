namespace WebApi.Services{
    public class DbService : ILoggerService
    {
        public void Write(string message)
        {
            System.Console.WriteLine("[DbLogger]  - " + message);
        }
    }
}