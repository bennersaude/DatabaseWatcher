using TableWatcher.Helper;

namespace BennerESocialDbWatcherService.Model
{
    public class Eso_Param_Tipo_Dependente 
    {
        [AtributoESocial("ESOCIALTIPODEPENDENTE")]
        public int EsocialTpDependente { get; set; }

        [AtributoESocial("BENNERTIPODEPENDENTE")]
        public string BennerTpDependente { get; set; }

        [AtributoESocial("VERSAO")]
        public int Versao { get; set; }
    }
}
