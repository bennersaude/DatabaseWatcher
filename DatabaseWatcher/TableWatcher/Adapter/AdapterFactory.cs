using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableWatcher.Interface;
using System.Configuration;

namespace TableWatcher.Adapter
{
    public class AdapterFactory<T> where T:class
    {
        private const string OracleConnectionString = "oracleConnectionString";
        private const string SqlServerConnectionString = "sqlServerConnectionString";

        public ITableWatcher<T> GetAdapter()
        {
            string connectionString;

            if (!string.IsNullOrEmpty(connectionString = getConnectionString(OracleConnectionString)))
                return new TableWatcherOracle<T>(connectionString);

            return new TableWatcherSqlServer<T>(getConnectionString(SqlServerConnectionString));
        }

        private string getConnectionString(string tag)
        {
            var connectionStringSettings = ConfigurationManager.AppSettings[tag];
            return connectionStringSettings;
        }
    }
}
