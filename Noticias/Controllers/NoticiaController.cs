using Microsoft.AspNetCore.Mvc;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsAPI;
using System.Net;
using Noticias.Model.Helpers;
using Noticias.Model;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

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


        [HttpGet, Route("Tecnologia")]
        public List<Noticia> GetNoticiasTecnologia()
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
        public List<Noticia> GetNoticiasUrgente()
        {
            return Noticias(Enums.Categoria.Urgente);
        }

        [HttpGet, Route("Financeiro")]
        public List<Noticia> GetNoticiasFinanceiro()
        {
            return Noticias(Enums.Categoria.Financeiro);
            
        }

        [HttpGet, Route("Politica")]
        public List<Noticia> GetNoticiasPolitica()
        {   
            return Noticias(Enums.Categoria.Politica);
        }

         List<Noticia> Noticias(Enums.Categoria categoria)
         {
            var noticas = _context.Noticias.Where(a => a.Categoria == categoria).ToList();
            return noticas;
         }
    }
}