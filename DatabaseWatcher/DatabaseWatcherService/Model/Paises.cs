using TableWatcher.Helper;

namespace BennerESocialDbWatcherService.Model
{
    public class Paises
    {
        [AtributoESocial("CODIGOESOCIAL")]
        public int CodigoESocial { get; set; }

        [AtributoESocial("NOME")]
        public string NomePais { get; set; }
    }
}
