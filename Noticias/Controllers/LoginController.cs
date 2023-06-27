using Microsoft.AspNetCore.Mvc;
using Noticias.Model;

namespace Noticias.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly Context _context;
        private readonly ILogger<LoginController> _logger;

        public LoginController(Context context, ILogger<LoginController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet, Route("Index")]
        public ActionResult Index()
        {
            return new JsonResult("Index");
        }

        [HttpGet, Route("Login")]
        public ActionResult Login(string login, string senha) 
        {
            var usario = _context.Usuarios.FirstOrDefault(a=>a.Login.Equals(login));

            if (usario == null) return new JsonResult("Usuario não encontrado");

            if (!usario.Senha.Equals(senha)) return new JsonResult("Senha incorreta");

            return new JsonResult("Usuario Logado com sucesso");
        }
    }
}
