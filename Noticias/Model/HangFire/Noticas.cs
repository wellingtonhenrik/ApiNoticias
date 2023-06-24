using Microsoft.EntityFrameworkCore;
using Noticias.Model.Helpers;

namespace Noticias.Model.HangFire
{
    public class Noticas
    {
        private readonly Context _context;

        public Noticas(Context context)
        {
            _context = context;
        }
        public async Task Executar(Enums.Categoria categoria)
        {
           var noticas = ConsultaNoticas.Noticias(categoria);

            foreach(var notica in noticas)
            {
                var existe = await _context.Noticias.FirstOrDefaultAsync(a=>a.Titulo == notica.Titulo && a.Categoria == notica.Categoria);
                if (existe != null) continue;
                _ = _context.AddRangeAsync(noticas);
                await _context.SaveChangesAsync();

            }
        }
    }
}
