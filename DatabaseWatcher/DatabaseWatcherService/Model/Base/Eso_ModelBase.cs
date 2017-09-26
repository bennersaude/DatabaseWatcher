using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableWatcher.Helper;

namespace BennerESocialDbWatcherService.Model
{
    public class Eso_ModelBase
    {
        [AtributoESocial("CODIGO")]
        public int Codigo { get; set; }

        [AtributoESocial("DESCRICAO")]
        public string Descricao { get; set; }

        [AtributoESocial("VERSAO")]
        public int Versao { get; set; }
    }
}
