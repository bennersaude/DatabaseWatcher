using TableWatcher.Helper;

namespace BennerESocialDbWatcherService.Model
{
    public class Eso_Param_Estado_Civil 
    {
        [AtributoESocial("ESOCIALESTADOCIVIL")]
        public int EsocialEstadoCivil { get; set; }

        [AtributoESocial("BENNERESTADOCIVIL")]
        public string BennerEstadoCivil { get; set; }

        [AtributoESocial("VERSAO")]
        public int Versao { get; set; }

    }
}
