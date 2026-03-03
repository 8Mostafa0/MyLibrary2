using Serilog;

namespace MyLibrary.ViewModel.Servicies
{
    public interface ILoggerService
    {
        ILogger Logger { get; }

        static abstract ILogger CreateLogger();
    }
}