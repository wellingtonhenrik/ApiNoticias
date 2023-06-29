using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noticias.Model;
using System.Linq;
using System.Net.WebSockets;

namespace Noticias.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly Context _context;
        private readonly ILogger<UsuarioController> _logger;
        public UsuarioController(Context context, ILogger<UsuarioController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet, Route("Usuarios")]
        public List<Usuario> Get()
        {

            var usuarios =  _context.Usuarios.ToList();

            return usuarios;
        }

        [HttpGet, Route("Usuario")]
        public ActionResult GetUsuario(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(a => a.UsuarioId == id);

            return new JsonResult(usuario);
        }

        [HttpPost, Route("Cadastro")]
        public ActionResult Cadastro(Usuario usuario)
        {
            var messagem =  usuario.ValidacaoCadastro(_context);

            return new JsonResult(messagem);
        }

        [HttpPut, Route("Atualizar")]

        public ActionResult Atualizar(Usuario usuario)
        {

            var messagem = usuario.ValidarAtualizacao(_context);

            return new JsonResult(messagem);
        }

        [HttpDelete, Route("Deletar")]
        public ActionResult DeletarUsuario(string login)
        {
            var usuario =  _context.Usuarios.FirstOrDefault(a => a.Login.Equals(login));
            if (usuario == null) return new JsonResult("Usuario não existe");
            var messagem = usuario.Deletar(_context);
            return new JsonResult(messagem);
        }


        [HttpGet, Route("Teste")]
        public ActionResult Teste()
        {
            var usuario = _context.Usuarios.FirstOrDefault();

            return new JsonResult(usuario);
        }

        [HttpPost, Route("CadastroEmMassa")]
        public ActionResult CadastroEmMassa(List<Usuario> usuarios)
        {
            var messagem = "";
            foreach(var usuario in usuarios)
            {
                 messagem = usuario.ValidacaoCadastro(_context);
            }

            return new JsonResult(messagem);
        }
    }
}
