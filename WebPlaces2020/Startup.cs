using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebPlaces2020.Models;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FluentValidation;
using WebPlaces2020.Validator;
using IdentityServer4.Models;

namespace WebPlaces2020
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
            services.AddMvc().AddFluentValidation();
            services.AddDbContext<AccountContext>(opt => opt.UseInMemoryDatabase("AccountList"));
            services.AddDbContext<PlaceContext>(opt => opt.UseInMemoryDatabase("PlaceList"));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
                    Title = "WebPlaces2020 API",
                    Version = "v1",
                    Description = "API of WebPlaces2020",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Dierickx Olivier",
                        Email = string.Empty,
                        Url = new Uri("https://www.frencholy.com"),
                    }
                }); ;

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddTransient<IValidator<Place>, PlaceValidator>();
            services.AddTransient<IValidator<Account>, AccountValidator>();
            services.AddIdentityServer()
           .AddInMemoryClients(Enumerable.Empty<Client>())
           .AddInMemoryApiResources(Enumerable.Empty<ApiResource>());

            services.AddAuthentication(defaultScheme: "Bearer")
     .AddIdentityServerAuthentication(authenticationScheme: "Bearer", configureOptions: options =>
     {
         options.Authority = "http://localhost:44325";
         options.RequireHttpsMetadata = false;
         options.ApiName = "api1";
     });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseSwagger();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwaggerUI(c =>
           {
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
               c.RoutePrefix = string.Empty;

           });
                

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseIdentityServer();
        }
    }
}
