using Microsoft.AspNetCore.Mvc;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsAPI;
using System.Net;
using Noticias.Model.Helpers;
using Noticias.Model;

namespace ApiNoticias.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoticiasController : ControllerBase
    {
        private readonly ILogger<NoticiasController> _logger;

        public NoticiasController(ILogger<NoticiasController> logger)
        {
            _logger = logger;
        }


        [HttpGet, Route("Tecnologia")]
        public List<Noticia> GetNoticiasTecnologia()
        {
            var listaNoticias = Noticias(Enums.Categoria.Tecnologia);

            return listaNoticias;
        }

        [HttpGet, Route("Urgente")]
        public List<Noticia> GetNoticiasUrgente()
        {
            var listaNoticias = Noticias(Enums.Categoria.Urgente);
            return listaNoticias;
        }

        [HttpGet, Route("Financeiro")]
        public List<Noticia> GetNoticiasFinanceiro()
        {
            var listaNoticias = Noticias(Enums.Categoria.Financeiro);
            return listaNoticias;
        }

        [HttpGet, Route("Politica")]
        public List<Noticia> GetNoticiasPolitica()
        {
            var listaNoticias = Noticias(Enums.Categoria.Politica);
            return listaNoticias;
        }

        List<Noticia> Noticias(Enums.Categoria categoria)
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
                    var noticia = new Noticia
                    {
                        Titulo = article.Title,
                        Autor = article.Author,
                        Descricao = article.Description,
                        DataPublicacao = article.PublishedAt,
                        UrlImagem = article.UrlToImage,
                        Categoria = categoria,
                    };

                    listaNoticias.Add(noticia);
                }
            }
            return listaNoticias;
        }
    }
}