using Oracle.ManagedDataAccess.Client;
using System;
using System.Threading.Tasks;
using TableDependency.Enums;
using TableDependency.OracleClient;
using TableWatcher.DAO.Base;

namespace TableWatcher.DAO.Oracle
{
    public class TableWatcherOracleDAO<T> : TableWatcherBaseDAO<T> where T : class
    {
        public String ConnectionString { get; private set; }

        protected override string handleZTabela { get; set; }

        public TableWatcherOracleDAO(String connectionString, String nomeClasse)
        {
            InicializarObjetosOracle(connectionString, nomeClasse);
        }

        private void InicializarObjetosOracle(string connectionString, string nomeClasse)
        {
            ConnectionString = connectionString;
            string NomeClasse = nomeClasse;

            string handleZTabela = Task.Run(() => GetHandleZTabela(nomeClasse)).Result;
            base.InicializarObjetos(handleZTabela);
        }

        public async Task InserirOnChange(TableDependency.EventArgs.RecordChangedEventArgs<T> e)
        {
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                await Task.Run(() => connection.OpenAsync());
                OracleCommand insertCommand = MontaInsertCommand(connection, e);
                if (insertCommand != null)
                {
                    await Task.Run(() => insertCommand.ExecuteNonQueryAsync());
                }
            }
        }

        public async Task<String> GetHandleZTabela(string nmTabela)
        {
            string sql = @"SELECT HANDLE 
                             FROM Z_TABELAS
                            WHERE upper(NOME) = :Nome";

            String handleZTabela = String.Empty;
            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                await Task.Run(() => connection.OpenAsync());

                OracleCommand command = new OracleCommand(sql, connection);
                command.Parameters.Add("NOME", nmTabela.ToUpper());

                var handle = await command.ExecuteScalarAsync();

                handleZTabela = Convert.ToString(handle);
            }
            return handleZTabela;

        }


    }
}
