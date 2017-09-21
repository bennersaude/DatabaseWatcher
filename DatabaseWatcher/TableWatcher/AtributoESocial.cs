using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;

namespace TableWatcher
{
    public class AtributoESocial : Attribute
    {
        public string NomeCampoBanco;
        public string TipoCampoBanco;

        public AtributoESocial(string nomeCampoBanco)
        {
            this.NomeCampoBanco = nomeCampoBanco;
        }
    }
}
