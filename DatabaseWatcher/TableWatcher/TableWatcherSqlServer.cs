using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TableDependency;
using TableDependency.Enums;
using TableDependency.SqlClient;
using TableWatcher.Base;

namespace TableWatcher
{
    public sealed class TableWatcherSqlServer<T> : TableWatcherBase<T>, ITableWatcher<T> where T : class
    {
        public readonly String ConnectionString;
        private SqlTableDependency<T> _dependency;

        public TableWatcherSqlServer(String connectionString)
        {
            ConnectionString = connectionString;
            MapearEntidade();
        }

        public void InitializeTableWatcher()
        {
            _dependency = new SqlTableDependency<T>(ConnectionString, typeof(T).Name, mapper, (IList<string>)null, DmlTriggerType.All, true, typeof(T).Name + "ESOCIAL");
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
                PropertyInfo propInfo = e.Entity.GetType().GetProperty("Handle");
                object itemValue = propInfo.GetValue(e.Entity, null);

                switch (e.ChangeType)
                {
                    case ChangeType.Delete:
                        Console.WriteLine($"Deletou Id:{ itemValue }");
                        break;
                    case ChangeType.Insert:
                        Console.WriteLine($"Inseriu Id:{ itemValue }");
                        break;
                    case ChangeType.Update:
                        Console.WriteLine($"Atualizou Id:{ itemValue }");
                        break;
                }
            }
        }

     

    }
}
