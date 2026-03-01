using MyLibrary.Model.DbContexts;
using MyLibrary.Model.Repositories;

namespace MyLibrary.ViewModel.Commands
{
    public class CheckDatabaseCommand : CommandBase
    {
        #region Dependencies
        private LoanRepository _loansRepository;
        private DbContextFactory _dbContextFactory;
        #endregion

        #region Contructor
        /// <summary>
        /// check ans validate database and tables
        /// </summary>
        public CheckDatabaseCommand(LoanRepository loanRepository, DbContextFactory dbContextFactory)
        {
            _loansRepository = loanRepository;
            _dbContextFactory = dbContextFactory;
        }
        #endregion

        #region Execution
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public override async void Execute(object parameter)
        {
            await _dbContextFactory.CheckDatabaseExistsAsync();
        }
        #endregion
    }
}
