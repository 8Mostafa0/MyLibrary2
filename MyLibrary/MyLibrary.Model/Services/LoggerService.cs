using Serilog;
namespace MyLibrary.ViewModel.Servicies
{
    /// <summary>
    /// class for recording logs
    /// </summary>
    public class LoggerService : ILoggerService
    {
        private ILogger _logger;
        public ILogger Logger
        {
            get
            {
                if (_logger is null)
                {
                    _logger = CreateLogger();
                }
                return _logger;
            }

        }
        /// <summary>
        /// Create instance of serilogger and Configure it.
        /// </summary>
        /// <returns></returns>
        public static ILogger CreateLogger()
        {
            ILogger logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            return logger;

        }

    }
}
