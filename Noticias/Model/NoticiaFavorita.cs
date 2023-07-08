using Noticias.Model.Helpers;

namespace Noticias.Model
{
    public class NoticiaFavorita : IPropriedadesPadrao
    {
        public NoticiaFavorita() 
        {
            Ativo = true;
            DataCadastro = DateTime.Now;
        
        }
        public int NoticiaFavoritaId { get; set; }
        public Enums.Categoria Categoria { get; set; }
        public DateTime DataCadastro { get ; set; }
        public bool Ativo { get; set; }
        public int UsuarioId { get; set; }
    }
}
