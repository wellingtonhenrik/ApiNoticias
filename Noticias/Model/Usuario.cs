using Noticias.Model.Helpers;
using System.Runtime.CompilerServices;

namespace Noticias.Model
{
    public class Usuario : IPropriedadesPadrao
    {
        private readonly Context _context;
        private readonly ILogger<Usuario> _logger;

        public Usuario()
        {
            DataCadastro = DateTime.Now;
            Ativo = true;
            NoticiaFavorita NoticiaFavorita = new NoticiaFavorita();
        }
        public Usuario(Context context, ILogger<Usuario> logger, int usuarioId, string nome, string sobrenome, string login, string email, Enums.Sexo sexo, string imagemPessoa, string senha, NoticiaFavorita noticiaFavorita = null)
        {
            UsuarioId = usuarioId;
            Nome = nome;
            Sobrenome = sobrenome;
            Login = login;
            Email = email;
            Sexo = sexo;
            ImagemPessoa = imagemPessoa;
            Senha = senha;
            DataCadastro = DateTime.Now;
            Ativo = true;
            NoticiaFavorita = noticiaFavorita;
            _context = context;
            _logger = logger;
        }

        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public Enums.Sexo Sexo { get; set; }
        public string ImagemPessoa { get; set; }
        public string Senha { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }
        public virtual NoticiaFavorita NoticiaFavorita {get;set;}

        public string ValidacaoCadastro(Context context)
        {
            try
            {
                var usuarioLogin = context.Usuarios.FirstOrDefault(a => a.Login.Equals(Login));
                var usuarioEmail = context.Usuarios.FirstOrDefault(a => a.Email.Equals(Email));

                if (usuarioLogin != null) return "Já existe usuario com esse login";
                if (usuarioEmail != null) return "Já existe usuario com esse this";

                context.Usuarios.Add(this);
                context.SaveChanges();

                return "Operação realizada com sucesso";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ValidarAtualizacao(Context context)
        {
            try
            {
                var usuarioBase = context.Usuarios.FirstOrDefault(a => a.UsuarioId == 1);

                if (usuarioBase != null)
                {
                    usuarioBase.Senha = Senha;
                    usuarioBase.ImagemPessoa = ImagemPessoa;
                    usuarioBase.Email = Email;
                    context.Usuarios.Update(usuarioBase);
                    context.SaveChanges();
                    return "Operacação realizada com sucesso";
                }
                else
                {
                    return "Usuario não encontrado";

                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Deletar(Context context)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Login))
                {
                    context.Usuarios.Remove(this);
                    context.SaveChanges();

                    return "Operacação realizada com sucesso";
                }
                else
                {
                    return "Usuario não existe";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }

}
