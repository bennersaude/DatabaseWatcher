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

namespace TableWatcher.BLL.Base
{
    public abstract class TableWatcherBase<T> where T : class
    {
        protected ModelToTableMapper<T> mapper;
        
        protected String nomeTabelaBase { get; private set; }
        protected String nomeObjetosEstrutura { get; private set; }
        protected String nomeTabelaEspelho { get; private set; }

        protected Boolean destruirObjetosWatcher = Convert.ToBoolean(ConfigurationManager.AppSettings["destruirObjetosWatcher"]);
        protected IList<String> listaUpdate;

        protected abstract string handleZTabela { get; set; }

        #region Mapeamento

        protected virtual void InicializarObjetos(String handleEntidade)
        {
            nomeTabelaBase = GetNomeEntidade();
            nomeObjetosEstrutura = GetNomeObjetosEstrutura(handleEntidade);
            nomeTabelaEspelho = GetNomeTabelaEspelho();
        }

        protected virtual void MapearEntidade()
        {
            mapper = new ModelToTableMapper<T>();
            listaUpdate = new List<String>();
            foreach (var prop in GetValuesForMapper())
            {
                mapper.AddMapping(prop.Key, prop.Value);
                listaUpdate.Add(prop.Value);
            }
        }

        private Dictionary<PropertyInfo, string> GetValuesForMapper()
        {
            Dictionary<PropertyInfo, string> valores = new Dictionary<PropertyInfo, string>();

            PropertyInfo[] props = typeof(T).GetProperties();

            foreach (PropertyInfo prop in props)
            {
                foreach (object attr in prop.GetCustomAttributes(true))
                {
                    if (attr is AtributoESocial authAttr)
                    {
                        var auth = authAttr.NomeCampoBanco;
                        valores.Add(prop, auth);
                    }
                }
            }

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
            return Utilidades<T>.GetNomeTabelaEspelho(nomeObjetosEstrutura); 
        }

        #endregion
    }
}
