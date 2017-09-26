using TableWatcher.Helper;

namespace BennerESocialDbWatcherService.Model
{
    public class Eso_Param_Tipo_Logradouro
    {
        [AtributoESocial("ESOCIALTIPOLOGRADOURO")]
        public int EsocialTpLogradouro { get; set; }

        [AtributoESocial("BENNERTIPOLOGRADOURO")]
        public string BennerTpLogradouro { get; set; }

        [AtributoESocial("VERSAO")]
        public int Versao { get; set; }
    }
}
