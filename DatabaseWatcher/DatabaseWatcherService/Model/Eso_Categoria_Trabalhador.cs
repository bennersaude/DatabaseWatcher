using TableWatcher.Helper;

namespace BennerESocialDbWatcherService.Model
{
    public class Eso_Categoria_Trabalhador : Eso_ModelBase
    {
        [AtributoESocial("GRUPO")]
        public int Grupo { get; set; }
    }
}
