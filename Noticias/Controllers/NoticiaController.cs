using Microsoft.AspNetCore.Mvc;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsAPI;
using System.Net;
using Noticias.Model.Helpers;
using Noticias.Model;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using Noticias.Model.HangFire;
using Newtonsoft.Json.Schema;

namespace Noticias.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoticiasController : ControllerBase
    {
        private readonly Context _context;
        private readonly ILogger<NoticiasController> _logger;

        public NoticiasController(ILogger<NoticiasController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet, Route("Get pelo Titulo")]
        public ActionResult GetNoticia(string? titulo)
        {
            return GetTitulo(titulo);
        }

        [HttpGet, Route("Noticias Salvas no banco")]
        public ActionResult GetNoticias(Enums.Categoria categoria)
        {
            return Noticias(categoria);
        }

        ActionResult Noticias(Enums.Categoria categoria, bool todasCategorias = false)
        {
            if (todasCategorias)
            {
                var noticas = _context.Noticias.ToList();

                return new JsonResult(noticas);
            }
            else
            {
                var noticas = _context.Noticias.Where(a => a.Categoria == categoria).ToList();
                return new JsonResult(noticas);
            }
        }
        ActionResult GetTitulo(string titulo)
        {

            if (string.IsNullOrWhiteSpace(titulo)) return new JsonResult("Nenhum titulo foi digitado");

            var noticia = _context.Noticias.Where(a => a.Titulo.Contains(titulo)).ToList();

            if (!noticia.Any()) return new JsonResult("Não foram encontradas noticias com esse titulo");

            return new JsonResult(noticia);
        }

        [HttpGet, Route("Noticias")]
        public ActionResult GetNoticias(string categoria)
        {
            return new JsonResult(ConsultaNoticas.Noticias(Enums.Categoria.Todas, categoria));
        }
    }
}