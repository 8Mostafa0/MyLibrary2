using Serilog;

namespace MyLibrary.ViewModel.Servicies
{
    public interface ILoggerService
    {
        ILogger logger { get; }

        ILogger CreateLogger();
    }
}