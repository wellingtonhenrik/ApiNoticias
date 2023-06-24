using Microsoft.EntityFrameworkCore;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsAPI;
using static Noticias.Model.Helpers.Enums;

namespace Noticias.Model.Helpers
{
    public class ConsultaNoticas
    {
        public static List<Noticia> Noticias(Enums.Categoria categoria)
        {
            HttpResponseMessage response;
            List<Noticia> listaNoticias = new List<Noticia>();

            var data = DateTime.Now;
            var novaData = data.AddDays(-1);

            var newsApiClient = new NewsApiClient("1ee5f753635a417199eda96741385288");
            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Q = categoria.GetDescription(),
                SortBy = SortBys.Popularity,
                Language = Languages.PT,
                From = novaData
            });
            if (articlesResponse.Status == Statuses.Ok)
            {
                foreach (var article in articlesResponse.Articles)
                {
                    if (article.Title == null) continue;
                    var noticia = new Noticia
                    {
                        Titulo = article.Title,
                        Autor = article.Author,
                        Descricao = article.Description,
                        DataPublicacao = article.PublishedAt,
                        UrlImagem = article.UrlToImage,
                        Categoria = categoria,
                        Url = article.Url
                    };

                    if (string.IsNullOrWhiteSpace(noticia.Titulo)) continue;

                    listaNoticias.Add(noticia); 
                }
            }
            return listaNoticias;
        }
    }
}
