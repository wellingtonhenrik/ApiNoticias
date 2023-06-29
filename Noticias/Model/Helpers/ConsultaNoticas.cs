using Microsoft.EntityFrameworkCore;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsAPI;
using static Noticias.Model.Helpers.Enums;

namespace Noticias.Model.Helpers
{
    public class ConsultaNoticas
    {
        public static List<Noticia> Noticias(Enums.Categoria categoria, string stringCategoria = "")
        {
            HttpResponseMessage response;
            List<Noticia> listaNoticias = new List<Noticia>();
            var data = DateTime.Now;
            var novaData = data.AddDays(-1);

            var newsApiClient = new NewsApiClient("1ee5f753635a417199eda96741385288");
            var articlesResponse = new ArticlesResult();
            if (categoria != Enums.Categoria.Todas && string.IsNullOrWhiteSpace(stringCategoria))
            {
                #region ViaEnum
                articlesResponse = newsApiClient.GetEverything(new EverythingRequest
                {
                    Q = categoria.GetDescription(),
                    SortBy = SortBys.Popularity,
                    Language = Languages.PT,
                    From = novaData,
                    Page = 1,
                    PageSize = 100,
                });
                #endregion
            }
            #region ViaString
            if (!string.IsNullOrWhiteSpace(stringCategoria))
            {
                articlesResponse = newsApiClient.GetEverything(new EverythingRequest
                {
                    Q = stringCategoria,
                    SortBy = SortBys.Popularity,
                    Language = Languages.PT,
                    From = novaData,
                    Page = 1,
                    PageSize = 100,
                });
            }
            #endregion
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
                        Url = article.Url,
                        NomeCategoria = stringCategoria
                    };

                    if (string.IsNullOrWhiteSpace(noticia.Titulo)) continue;

                    listaNoticias.Add(noticia);
                }
            }
            return listaNoticias;
        }
    }
}
