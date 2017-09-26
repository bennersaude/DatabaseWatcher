using TableWatcher.Helper;

namespace BennerESocialDbWatcherService.Model
{
    public class Eso_Versao
    {
        [AtributoESocial("CODIGO")]
        public int Codigo { get; set; }

        [AtributoESocial("VIGENTE")]
        public string Vigente { get; set; }
    }
}
