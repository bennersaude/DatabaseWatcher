using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TableDependency.Enums;
using TableDependency.SqlClient;
using TableWatcher.DAO.Base;

namespace TableWatcher.DAO.SqlServer
{
    public sealed class TableWatcherSqlServerDAO<T> : TableWatcherBaseDAO<T> where T : class
    {
        public String ConnectionString { get; private set; }
        private SqlTableDependency<T> _dependency;

        protected override string handleZTabela { get; set; }

        public TableWatcherSqlServerDAO(String connectionString)
        {
            InicializarObjetosSqlServer(connectionString);
        }

        private void InicializarObjetosSqlServer(string connectionString)
        {
            ConnectionString = connectionString;
            string nomeClasse = typeof(T).Name;

            string handleZTabela = Task.Run(() => GetHandleZTabela(nomeClasse)).Result;
            base.InicializarObjetos(handleZTabela); ;
        }

     
        public async Task InserirOnChange(TableDependency.EventArgs.RecordChangedEventArgs<T> e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    await Task.Run(() => connection.OpenAsync());
                    SqlCommand insertCommand = MontaInsertCommand(connection, e);
                    if (insertCommand != null)
                    {
                        await Task.Run(() => insertCommand.ExecuteNonQueryAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                var t = ex;
                throw;
            }
        }

        public async Task<String> GetHandleZTabela(string nmTabela)
        {
            string sql = @"SELECT HANDLE 
                             FROM Z_TABELAS
                            WHERE upper(NOME) = @Nome";
            String handleZTabela = String.Empty;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                await Task.Run(() => connection.OpenAsync());

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@NOME", nmTabela.ToUpper());

                var handle = await command.ExecuteScalarAsync();

                handleZTabela = Convert.ToString(handle);
            }
            return handleZTabela;
        }
    }
}
