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
            RecurringJob.AddOrUpdate<NoticasHangfire>("Consulta noticas de tecnologia", x => x.Executar(Enums.Categoria.Tecnologia), Cron.Hourly, timezene);
            RecurringJob.AddOrUpdate<NoticasHangfire>("Consulta noticas de Financeiro", x => x.Executar(Enums.Categoria.Financeiro), Cron.Hourly, timezene);
            RecurringJob.AddOrUpdate<NoticasHangfire>("Consulta noticas de Urgente", x => x.Executar(Enums.Categoria.Urgente), Cron.Hourly, timezene);
            RecurringJob.AddOrUpdate<NoticasHangfire>("Consulta noticas de Politica", x => x.Executar(Enums.Categoria.Politica), Cron.Hourly, timezene);
            RecurringJob.AddOrUpdate<NoticasHangfire>("Delete noticas antigas", x => x.DeleteNoticasAntigas(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1)), Cron.Hourly, timezene);
            /////////////////////////////////////////////////////////////////
        }
    }
}
