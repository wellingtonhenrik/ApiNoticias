using Hangfire;
using Noticias.Model.HangFire;
using Noticias.Model.Helpers;
using System.Globalization;

namespace Noticias.Controllers
{
    public class HangFireConfig
    {
        public static void Start()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
            var timezene = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");


            ////////////////Implementação dos Jobs///////////////////////////
            RecurringJob.AddOrUpdate<Noticas>("Consulta noticas de tecnologia", x => x.Executar(Enums.Categoria.Tecnologia), Cron.Hourly, timezene);
            RecurringJob.AddOrUpdate<Noticas>("Consulta noticas de Financeiro", x => x.Executar(Enums.Categoria.Financeiro), Cron.Hourly, timezene);
            RecurringJob.AddOrUpdate<Noticas>("Consulta noticas de Urgente", x => x.Executar(Enums.Categoria.Urgente), Cron.Hourly, timezene);
            RecurringJob.AddOrUpdate<Noticas>("Consulta noticas de Politica", x => x.Executar(Enums.Categoria.Politica), Cron.Hourly, timezene);
            /////////////////////////////////////////////////////////////////
        }
    }
}
