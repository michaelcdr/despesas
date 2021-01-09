using AutoMapper;
using LifeManager.API.Profiles;
using LifeManager.Domain.Repositorios;
using LifeManager.Infra.DBConfiguration;
using LifeManager.Infra.EF;
using LifeManager.Infra.Repositorios.EF;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace LifeManager.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers().AddJsonOptions(options => {
                
            });
            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "LifeManager.API", 
                    Version = "v1" ,
                    Description = "API REST criada com o ASP.NET Core para o Sistema Android Lanches",
                    Contact = new OpenApiContact
                    {
                        Name = "Michael Costa dos Reis",
                        Url = new Uri("https://github.com/michaelcdr")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(typeof(DespesaMap));
            services.AddAutoMapper(typeof(LembreteMap));
            //services.AddScoped<IDatabaseFactory, MySqlDatabaseFactory>();         // usando mysql
            services.AddScoped<IDatabaseFactory, SqlServerDatabaseFactory>();       // usando sql Server
            services.AddTransient<IDespesasRepositorio, DespesasRepositorio>();
            services.AddTransient<ILembretesRepositorio, LembretesRepositorio>();

            var assembly = AppDomain.CurrentDomain.Load("LifeManager.Application");
            services.AddMediatR(assembly);
            //services.add(options =>
            //{
            //    options.DefaultApiVersion = new ApiVersion(1, 0);
            //    options.AssumeDefaultVersionWhenUnspecified = true;
            //    options.ReportApiVersions = true;
            //});
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LifeManager.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(b => b.AllowAnyOrigin());
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
