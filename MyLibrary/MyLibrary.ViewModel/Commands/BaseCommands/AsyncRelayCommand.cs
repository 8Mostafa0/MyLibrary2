using MyLibrary.ViewModel.Servicies;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
namespace MyLibrary.ViewModel.Commands.BaseCommands
{
    public class AsyncRelayCommand<T> : ICommand, ICommandBase
    {
        private bool _isRunning = false;
        private CancellationTokenSource _cts;
        private readonly Func<T, CancellationToken, Task> _execute;
        private readonly Func<T, bool> _canExecute;
        private Task _executionTask;
        private event EventHandler<Exception> ExecutionFailed;
        public event EventHandler CanExecuteChanged;
        public bool IsRunning => _isRunning;
        public Task ExecutionTask => _executionTask;

        public AsyncRelayCommand(Func<T, CancellationToken, Task> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (IsRunning) return false;
            return _canExecute == null || _canExecute((T)parameter);
        }

        public async void Execute(object parameter)
        {
            if (IsRunning) return;

            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
            _isRunning = true;
            RaiseCanExecuteChanged();


            try
            {
                _executionTask = _execute((T)parameter, _cts.Token);
                await _executionTask;
            }
            catch (OperationCanceledException)
            {
                LoggerService.logger.Information("Command Exceution Cancelled");
            }
            catch (Exception ex)
            {
                LoggerService.logger.Error("Error", ex.Message);
            }
            finally
            {
                _isRunning = false;
                _cts.Dispose();
                _cts = null;
                RaiseCanExecuteChanged();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        public void Cancel() => _cts?.Cancel();

        protected virtual void OnExceutionFailed(Exception ex)
        {
            ExecutionFailed?.Invoke(this, ex);
        }

    }

    public class AsyncRelayCommand : AsyncRelayCommand<object>
    {
        public AsyncRelayCommand(Func<Task> excute, Func<bool> canExcute = null)
            : base(async (_, token) => await excute(),
                canExcute != null ? new Func<object, bool>(_ => canExcute()) : null
                )
        {

        }

        public AsyncRelayCommand(Func<CancellationToken, Task> execute, Func<bool> canExcute = null)
            : base(
                  async (_, token) => await execute(token),
                  canExcute != null ? new Func<object, bool>(_ => canExcute()) : null)
        {
        }
    }
}