using Noticias.Model.Helpers;

namespace Noticias.Model
{
    public class Noticia
    {
        public int NoticiaId { get; set; }
        public Enums.Categoria Categoria { get; set; }
        public string? Titulo { get; set; }
        public string? Autor { get; set; }
        public string? Descricao { get; set; }
        public string? Url { get; set; }
        public string? UrlImagem { get; set; }
        public DateTime? DataPublicacao { get; set; }
    }
}
