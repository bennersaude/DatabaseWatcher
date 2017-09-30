using Oracle.ManagedDataAccess.Client;
using System;
using System.Threading.Tasks;
using TableDependency.Enums;
using TableDependency.OracleClient;
using TableWatcher.BLL.Base;
using TableWatcher.DAO.Oracle;

namespace TableWatcher.BLL.Oracle
{
    public class TableWatcherOracle<T> : TableWatcherBase<T>, ITableWatcher<T> where T : class
    {
        public String ConnectionString { get; private set; }
        private OracleTableDependency<T> _dependency;

        protected override string handleZTabela { get; set; }
        public TableWatcherOracleDAO<T> daoTableWatcherOracle { get; set; }

        public TableWatcherOracle(String connectionString)
        {
            InicializarObjetosOracle(connectionString);
            MapearEntidade();
        }

        private void InicializarObjetosOracle(string connectionString)
        {
            ConnectionString = connectionString;

            daoTableWatcherOracle = new TableWatcherOracleDAO<T>(ConnectionString);

            string nomeClasse = typeof(T).Name;
            string handleZTabela = Task.Run(() => daoTableWatcherOracle.GetHandleZTabela(nomeClasse)).Result;

            base.InicializarObjetos(handleZTabela);
        }

        public void InitializeTableWatcher()
        {
            _dependency = new OracleTableDependency<T>(ConnectionString, nomeTabelaBase, mapper, listaUpdate, DmlTriggerType.All, destruirObjetosWatcher, nomeObjetosEstrutura);
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
                Task.Run(() => daoTableWatcherOracle.InserirOnChange(e));
            }
        }
    }
}
