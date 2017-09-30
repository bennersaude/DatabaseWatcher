using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableWatcher.Helper
{
    public class Utilidades<T> where T : class
    {
        public static string GetNomeEntidade()
        {
            return typeof(T).Name;
        }

        public static string GetNomeObjetosEstrutura(String handleEntidade)
        {
            return $"DBW_ESPELHO_{handleEntidade}";
        }

        public static string GetNomeTabelaEspelho(String nomeObjetosEstrutura)
        {
            return $"{nomeObjetosEstrutura}_TAB";
        }
    }
}
