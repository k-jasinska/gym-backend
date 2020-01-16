using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portal.Application;
using Portal.Application.RepositoriesInterfaces;
using Portal.Application.Services;
using Portal.Infrastructure;
using Portal.Infrastructure.Contexts;
using Portal.Models;
using Portal.StartupConfiguration;

namespace Portal
{
    public class Startup
    {
        public AuthConfig Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = new AuthConfig();
            configuration.Bind(Configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.ConfigureMvc();
            services.AddDbContext<Context>(options => options.UseNpgsql(Configuration.ConnectionStrings));

            services.AddAuthorization(Configuration);
            services.AddScoped<IUserContext, HttpUserContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
           
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IClientQueryRepository, ClientRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddScoped<ITrainerService, TrainerService>();
            services.AddScoped<ITrainerQueryRepository, TrainerRepository>();
            services.AddScoped<ITrainerRepository, TrainerRepository>();

            services.AddScoped<ICarnetService, CarnetService>();
            services.AddScoped<ICarnetQueryRepository, CarnetRepository>();
            services.AddScoped<ICarnetRepository, CarnetRepository>();

            services.AddMvc()
    .AddJsonOptions(
        options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.BuildServiceProvider().GetService<Context>().Database.Migrate();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication(Configuration);
            app.UseMvc();
        }
    }
}
