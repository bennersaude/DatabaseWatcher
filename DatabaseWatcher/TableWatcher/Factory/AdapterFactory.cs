using System.Configuration;
using TableWatcher.Base;
using TableWatcher.Oracle;
using TableWatcher.SqlServer;

namespace TableWatcher.Factory
{
    public class AdapterFactory<T> where T : class
    {
        private const string OracleConnectionString = "oracleConnectionString";
        private const string SqlServerConnectionString = "sqlServerConnectionString";

        public ITableWatcher<T> GetAdapter()
        {
            string connectionString;

            connectionString = GetConnectionString(OracleConnectionString);
            if (!string.IsNullOrEmpty(connectionString))
            {
                return new TableWatcherOracle<T>(connectionString);
            }
            else
            {
                connectionString = GetConnectionString(SqlServerConnectionString);
                return new TableWatcherSqlServer<T>(connectionString);
            }
        }

        private string GetConnectionString(string tag)
        {
            var connectionStringSettings = ConfigurationManager.AppSettings[tag];
            return connectionStringSettings;
        }
    }
}
