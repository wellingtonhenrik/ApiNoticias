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
        //[HttpGet, Route("aa")]
        //public ActionResult Teste()
        //{
        //    return new JsonResult(new Noticia());
        //}

        [HttpGet, Route("Todas as noticias")]
        public ActionResult GetNoticias()
        {
            return Noticias(Enums.Categoria.Todas, true);
        }

        [HttpGet, Route("Get pelo Titulo")]
        public ActionResult GetNoticia(string? titulo)
        {
            return GetTitulo(titulo);
        }


        [HttpGet, Route("Tecnologia")]
        public ActionResult GetNoticiasTecnologia()
        {
            return Noticias(Enums.Categoria.Tecnologia);
        }

        /// <summary>
        //public async Task<ActionResult<IEnumerable<Noticia>>> GetNoticiasTecnologia()
        //{
        //    Noticias(Enums.Categoria.Tecnologia);

        //    _ = _context.AddRangeAsync(teste);
        //    await _context.SaveChangesAsync();

        //    return await listaNoticias;
        //}
        /// </summary>


        [HttpGet, Route("Urgente")]
        public ActionResult GetNoticiasUrgente()
        {
            return Noticias(Enums.Categoria.Urgente);
        }

        [HttpGet, Route("Financeiro")]
        public ActionResult GetNoticiasFinanceiro()
        {
            return Noticias(Enums.Categoria.Financeiro);
            
        }

        [HttpGet, Route("Politica")]
        public ActionResult GetNoticiasPolitica()
        {   
            return Noticias(Enums.Categoria.Politica);
        }

         ActionResult Noticias (Enums.Categoria categoria, bool todasCategorias = false)
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

            if(!noticia.Any()) return new JsonResult("Não foram encontradas noticias com esse titulo");

            return new JsonResult(noticia);
        }
    }
}