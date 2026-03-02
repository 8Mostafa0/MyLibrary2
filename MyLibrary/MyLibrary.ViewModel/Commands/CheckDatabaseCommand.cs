using MyLibrary.Model.DbContexts;

namespace MyLibrary.ViewModel.Commands
{
    public class CheckDatabaseCommand : CommandBase, ICheckDatabaseCommand
    {
        #region Dependencies
        private DbContextFactory _dbContextFactory;
        #endregion

        #region Contructor
        /// <summary>
        /// check ans validate database and tables
        /// </summary>
        public CheckDatabaseCommand(DbContextFactory dbContextFactory)
        {
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
