using System;

namespace TableWatcher.Helper
{
    public class AtributoESocial : Attribute
    {
        public string NomeCampoBanco;

        /// <summary>
        /// Esta anotação tem 3 funções dentro do projeto DatabaseWatcher:
        /// 1 - Indicar quais serão os campos que irão compor a tabela destino.
        /// 2 - Mapear as propriedades com os repectivos nomes dos campos do banco de suas respectivas tabelas observaveis. 
        /// 3 - Mapear os campos que serão observados nas operações de update de banco de suas respectivas tabelas observaveis. 
        /// </summary>
        /// <param name="nomeCampoBanco"></param>
        public AtributoESocial(string nomeCampoBanco)
        {
            this.NomeCampoBanco = nomeCampoBanco;
        }
    }
}
