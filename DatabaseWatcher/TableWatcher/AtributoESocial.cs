using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableWatcher
{
    public class AtributoESocial : Attribute
    {
        public string NomeCampoBanco;

        public AtributoESocial(string nomeCampoBanco)
        {
            this.NomeCampoBanco = nomeCampoBanco;
        }
    }
}
