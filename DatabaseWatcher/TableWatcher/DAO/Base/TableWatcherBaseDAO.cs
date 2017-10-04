using Newtonsoft.Json;
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
                String jsonInsert = GetJasonValueForInsert(e);

                if (!String.IsNullOrEmpty(jsonInsert))
                {
                    string sql = $"INSERT INTO {nomeTabelaEspelho} (UIID, VALORES) values (@UIID, @VALORES)";

                    command.Parameters.AddWithValue("UIID", uiid.ToString());
                    command.Parameters.AddWithValue("VALORES", jsonInsert);

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

                String jsonInsert = GetJasonValueForInsert(e);

                if (!String.IsNullOrEmpty(jsonInsert))
                {
                    string sql = $"INSERT INTO {nomeTabelaEspelho} (UIID, VALORES) values (sys_guid(), :VALORES)";
                    command.Parameters.Add("VALORES", jsonInsert);

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

        private String GetJasonValueForInsert(TableDependency.EventArgs.RecordChangedEventArgs<T> evento)
        {
            String jasonValues = String.Empty;
            try
            {
                List<object> valores = new List<object>(); 
               
                List<PropertyInfo> propriedades = new List<PropertyInfo>(evento.Entity.GetType().GetProperties());

                foreach (PropertyInfo prop in propriedades)
                {
                    if (prop.Name.ToUpper().Equals("HANDLE"))
                        valores.Add(prop.GetValue(evento.Entity, null));

                    foreach (object attr in prop.GetCustomAttributes(true))
                    {
                        var atributoEsocial = attr as AtributoESocial;
                        object valorPropriedade = prop.GetValue(evento.Entity, null);

                        valores.Add(valorPropriedade);
                    }
                }

                valores.Add(Convert.ToInt32(evento.ChangeType));
                valores.Add(DateTime.Now);
                valores.Add(ConfigurationManager.AppSettings["versaoESocial"]);

                jasonValues = JsonConvert.SerializeObject(valores);
            }
            catch
            {
                jasonValues = null;
            }

            return jasonValues;
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
            return Utilidades<T>.GetNomeTabelaEspelho(nomeObjetosEstrutura);
        }

        #endregion
    }
}
