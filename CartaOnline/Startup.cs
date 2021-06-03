using System.Data;
using System.Data.SqlClient;
using CartaOnline.Config;
using CartaOnline.Query;
using CartaOnline.Repositories;
using CartaOnline.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SqlKata.Compilers;

namespace CartaOnline
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Guardo conexion en variable
            var conexion = Configuration.GetConnectionString("AppDbContext");
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(conexion));
            //Se agrega en generador de Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                { Title = "Api Caduca REST", Version = "v1" });
            });
            //SQLKATA
            services.AddTransient<Compiler, SqlServerCompiler>();
            services.AddTransient<IDbConnection>(c =>
            {
                return new SqlConnection(conexion);
            });

            services.AddTransient<IRepositoryGeneric, RepositoryGeneric>();
            services.AddTransient<IRepositoryComanda, RepositoryComanda>();
            services.AddTransient<IMercaderiaService, Mercaderiaervice>();
            services.AddTransient<IComandaService, Comandaervice>();
            services.AddTransient<IComandaMercaderiaervice, ComandaMercaderiaervice>();
            services.AddTransient<IComandaQuery, ComandaQuery>();
            services.AddTransient<IMercaderiaQuery, MercaderiaQuery>();

            services.AddCors(c => c.AddDefaultPolicy(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            //indica la ruta para generar la configuración de swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Caduca REST");
            });
            //CORS
            app.UseCors();




        }
    }
}
