using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TableDependency;
using TableDependency.Mappers;

namespace TableWatcher.Base
{
    public abstract class TableWatcherBase<T> where T : class
    {
        protected ModelToTableMapper<T> mapper;
        protected virtual void MapearEntidade()
        {
            mapper = new ModelToTableMapper<T>();

            foreach (var prop in GetValues())
            {
                mapper.AddMapping(c => prop.Key, prop.Value);
            }
        }

        private Dictionary<PropertyInfo, string> GetValues()
        {
            Dictionary<PropertyInfo, string> valores = new Dictionary<PropertyInfo, string>();


            PropertyInfo[] props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    var authAttr = attr as AtributoBanco;
                    if (authAttr != null)
                    {
                        var auth = authAttr.NomeBanco;
                        valores.Add(prop, auth);
                    }
                }
            }

            return valores;
        }
    }
}
