using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using TableDependency.Mappers;
using TableWatcher.Helper;

namespace TableWatcher.DAO.Base
{
    public abstract class TableWatcherBaseDAO<T> where T : class
    {
        private Guid uiid;
        protected ModelToTableMapper<T> mapper;

        protected String nomeTabelaBase { get; private set; }
        protected String nomeObjetosEstrutura { get; private set; }
        protected String nomeTabelaEspelho { get; private set; }

        protected abstract string handleZTabela { get; set; }

        protected virtual void InicializarObjetos(String handleEntidade)
        {
            nomeTabelaBase = GetNomeEntidade();
            nomeObjetosEstrutura = GetNomeObjetosEstrutura(handleEntidade);
            nomeTabelaEspelho = GetNomeTabelaEspelho();
        }

        #region Insert Command

        protected virtual SqlCommand MontaInsertCommand(SqlConnection connection, TableDependency.EventArgs.RecordChangedEventArgs<T> e)
        {
            try
            {
                uiid = Guid.NewGuid();

                SqlCommand command = new SqlCommand();
                Dictionary<String, object> dicionarioValoresInsert = GetValuesForInsert(e);

                if (dicionarioValoresInsert.Count() > 0)
                {
                    string fields = String.Join(",", dicionarioValoresInsert.Select(s => s.Key));
                    string values = String.Join(",", dicionarioValoresInsert.Select(s => $"@{s.Key}"));

                    string sql = $"INSERT INTO {nomeTabelaEspelho} (UIID, {fields}) values ('{uiid.ToString()}', {values})";
                    foreach (var item in dicionarioValoresInsert)
                    {
                        command.Parameters.AddWithValue(item.Key, item.Value);
                    }

                    command.Connection = connection;
                    command.CommandText = sql;

                    return command;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        protected virtual OracleCommand MontaInsertCommand(OracleConnection connection, TableDependency.EventArgs.RecordChangedEventArgs<T> e)
        {
            try
            {
                OracleCommand command = new OracleCommand();

                Dictionary<String, object> dicionarioValoresInsert = GetValuesForInsert(e);

                if (dicionarioValoresInsert.Count() > 0)
                {
                    string fields = String.Join(",", dicionarioValoresInsert.Select(s => s.Key));
                    string values = String.Join(",", dicionarioValoresInsert.Select(s => $":{s.Key}"));

                    string sql = $"INSERT INTO {nomeTabelaEspelho} (UIID, {fields}) values (sys_guid(), {values})";
                    foreach (var item in dicionarioValoresInsert)
                    {
                        command.Parameters.Add(item.Key, item.Value);
                    }

                    command.Connection = connection;
                    command.CommandText = sql;

                    return command;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        private Dictionary<String, object> GetValuesForInsert(TableDependency.EventArgs.RecordChangedEventArgs<T> evento)
        {
            Dictionary<String, object> valores = new Dictionary<String, object>();

            List<PropertyInfo> propriedades = new List<PropertyInfo>(evento.Entity.GetType().GetProperties());

            foreach (PropertyInfo prop in propriedades)
            {
                foreach (object attr in prop.GetCustomAttributes(true))
                {
                    var atributoEsocial = attr as AtributoESocial;
                    object valorPropriedade = prop.GetValue(evento.Entity, null);

                    valores.Add(atributoEsocial.NomeCampoBanco, valorPropriedade);
                }
            }

            int valorOperacao = Convert.ToInt32(evento.ChangeType);
            valores.Add("TIPOOPERACAO", valorOperacao.ToString());
            valores.Add("DATAINCLUSAO", DateTime.Now);

            return valores;
        }

        #endregion

        #region Nome das Entidades

        private string GetNomeEntidade()
        {
            return Utilidades<T>.GetNomeEntidade();
        }

        private string GetNomeObjetosEstrutura(String handleEntidade)
        {
            return Utilidades<T>.GetNomeObjetosEstrutura(handleEntidade);
        }

        private string GetNomeTabelaEspelho()
        {
            return Utilidades<T>.GetNomeObjetosEstrutura(nomeObjetosEstrutura);
        }

        #endregion
    }
}
