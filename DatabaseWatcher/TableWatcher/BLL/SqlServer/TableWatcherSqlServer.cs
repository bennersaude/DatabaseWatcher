using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TableDependency.Enums;
using TableDependency.SqlClient;
using TableWatcher.BLL.Base;
using TableWatcher.DAO.SqlServer;

namespace TableWatcher.BLL.SqlServer
{
    public sealed class TableWatcherSqlServer<T> : TableWatcherBase<T>, ITableWatcher<T> where T : class
    {
        public String ConnectionString { get; private set; }
        private SqlTableDependency<T> _dependency;

        protected override string handleZTabela { get; set; }
        public TableWatcherSqlServerDAO<T> daoTableWatcherSqlServer { get; set; }

        public TableWatcherSqlServer(String connectionString)
        {
            InicializarObjetosSqlServer(connectionString);
            MapearEntidade();
        }

        private void InicializarObjetosSqlServer(string connectionString)
        {
            ConnectionString = connectionString;
            string nomeClasse = typeof(T).Name;

            string handleZTabela = Task.Run(() => daoTableWatcherSqlServer.GetHandleZTabela(nomeClasse)).Result;
            base.InicializarObjetos(handleZTabela);
        }

        public void InitializeTableWatcher()
        {
            _dependency = new SqlTableDependency<T>(ConnectionString, nomeTabelaBase, mapper, listaUpdate, DmlTriggerType.All, destruirObjetosWatcher, nomeObjetosEstrutura);
            _dependency.OnChanged += OnChanged;
            _dependency.OnError += OnError;
        }

        public void StartTableWatcher()
        {
            _dependency.Start();
        }

        public void StopTableWatcher()
        {
            _dependency.Stop();
        }

        public void OnError(object sender, TableDependency.EventArgs.ErrorEventArgs e)
        {
            throw e.Error;
        }

        public void OnChanged(object sender, TableDependency.EventArgs.RecordChangedEventArgs<T> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                Task.Run(() => daoTableWatcherSqlServer.InserirOnChange(e));
            }
        }
    }
}
