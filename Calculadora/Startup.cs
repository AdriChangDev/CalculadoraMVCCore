using Calculadora.Data;
using Calculadora.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Calculadora
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICalculadoraRepository, CalculadoraRepository>();
            services.AddDbContext<CalculadoraDbContext>(opt => opt.UseSqlite("Data Source=Calculadora.db"));
            services.AddMvc(options => options.EnableEndpointRouting = false);


        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
           app.UseMvcWithDefaultRoute();
        }
    }
}
