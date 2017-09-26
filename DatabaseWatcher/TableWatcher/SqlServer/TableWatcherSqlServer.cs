﻿using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TableDependency.Enums;
using TableDependency.SqlClient;
using TableWatcher.Base;

namespace TableWatcher.SqlServer
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
            _dependency = new SqlTableDependency<T>(ConnectionString, nomeEntidade, mapper, listaUpdate, DmlTriggerType.All, destruirObjetosWatcher, nomeEntidade + "ESOCIAL");
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
                Task.Run(() => InserirOnChange(e));
            }
        }

        public async Task InserirOnChange(TableDependency.EventArgs.RecordChangedEventArgs<T> e)
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


    }
}