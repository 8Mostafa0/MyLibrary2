using MyLibrary.Model.DbContexts;
using MyLibrary.ViewModel.Factory;

namespace MyLibrary.ViewModel.Commands
{
    public class CheckDatabaseCommand : CommandBase, ICheckDatabaseCommand
    {
        #region Dependencies
        private IDbContextFactory _dbContextFactory;
        #endregion

        #region Contructor
        /// <summary>
        /// check ans validate database and tables
        /// </summary>
        public CheckDatabaseCommand()
        {
            _dbContextFactory = ClassFactory.CreateDbContextFactory();
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
