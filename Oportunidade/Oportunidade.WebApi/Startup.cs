using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oportunidade.Blog;
using Oportunidade.Interface.Blog;
using Oportunidade.Interface.Negocio;
using Oportunidade.Negocio;

namespace Oportunidade.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            ConfigurarInjecaoDependencia(services);
            ConfigurarInformacoesEstaticas();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #region ConfigurarInjecaoDependencia
        private void ConfigurarInjecaoDependencia(IServiceCollection services)
        {
            services.AddScoped<IFeed, Feed>();
            services.AddScoped<ITopico, Topico>();
        }
        #endregion

        #region ConfigurarInformacoesEstaticas
        private void ConfigurarInformacoesEstaticas()
        {
            ConfiguracaoAPI.urlBlog = Configuration["urlBlog"];
        }
        #endregion
    }
}
