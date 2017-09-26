﻿using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using TableDependency.Mappers;
using TableWatcher.Helper;

namespace TableWatcher.Base
{
    public abstract class TableWatcherBase<T> where T : class
    {
        protected ModelToTableMapper<T> mapper;

        protected String nomeEntidade = GetNomeEntidadeSqlServer(17); //Nas versões atuais do banco de dados caso o nome possua mais de 17 caracteres será retornado um erro;
        protected String nomeEntidadeOracle = GetNomeEntidadeSqlServer(17).ToUpper(); //Nas versões atuais do banco de dados caso o nome possua mais de 17 caracteres será retornado um erro;

        protected Boolean destruirObjetosWatcher = Convert.ToBoolean(ConfigurationManager.AppSettings["destruirObjetosWatcher"]);

        protected IList<String> listaUpdate;

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
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
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

        private static string GetNomeEntidadeSqlServer(int TamanhoMaximo)
        {
            return typeof(T).Name.Length > TamanhoMaximo ? typeof(T).Name.Substring(0, TamanhoMaximo) : typeof(T).Name;
        }

        protected virtual SqlCommand MontaInsertCommand(SqlConnection connection, TableDependency.EventArgs.RecordChangedEventArgs<T> e)
        {
            try
            {
                SqlCommand command = new SqlCommand();

                Dictionary<String, object> dicionarioValoresInsert = GetValuesForInsert(e);

                if (dicionarioValoresInsert.Count() > 0)
                {
                    string fields = String.Join(",", dicionarioValoresInsert.Select(s => s.Key));
                    string values = String.Join(",", dicionarioValoresInsert.Select(s => $"@{s.Key}"));

                    string sql = $"INSERT INTO {nomeEntidade}ESOCIAL ({fields}) values ({values})";
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
                    string values = String.Join(",", dicionarioValoresInsert.Select(s => $"@{s.Key}"));

                    string sql = $"INSERT INTO {nomeEntidade}ESOCIAL ({fields}) values ({values})";
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
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    var atributoEsocial = attr as AtributoESocial;
                    object valorPropriedade = prop.GetValue(evento.Entity, null);

                    valores.Add(atributoEsocial.NomeCampoBanco, valorPropriedade);
                }
            }

            if (valores.Count() > 0)
            {
                int valorOperacao = Convert.ToInt32(evento.ChangeType);
                valores.Add("TipoOperacao", valorOperacao.ToString());
            }

            return valores;
        }
    }
}
