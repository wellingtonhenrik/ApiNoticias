using Noticias.Model.Helpers;

namespace Noticias.Model
{
    public class Noticia
    {

        private readonly Context _context;

        private readonly ILogger<Noticia> _logger;


        public Noticia()
        {
        }
        public Noticia(Context context, ILogger<Noticia> logger, int noticiaId, Enums.Categoria categoria, string? titulo, string? autor, string? descricao, string? url, string? urlImagem, DateTime? dataPublicacao)
        {
            _context = context;
            _logger = logger;
            NoticiaId = noticiaId;
            Categoria = categoria;
            Titulo = titulo;
            Autor = autor;
            Descricao = descricao;
            Url = url;
            UrlImagem = urlImagem;
            DataPublicacao = dataPublicacao;
        }
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
