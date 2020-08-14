using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebPlaces2020.Client.Models;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FluentValidation;
using WebPlaces2020.Validator;
using WebPlaces2020.Client.Context;
using IdentityServer4.Models;

namespace WebPlaces2020MVC
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
            services.AddDbContext<AccountContext>(opt => opt.UseInMemoryDatabase("AccountsList"));
            services.AddDbContext<PlaceContext>(opt => opt.UseInMemoryDatabase("PlacesList"));
            services.AddControllersWithViews();
            services.AddTransient<IValidator<Place>, PlaceValidator>();
            services.AddTransient<IValidator<Account>, AccountValidator>();
            services.AddIdentityServer()
           .AddInMemoryClients(Enumerable.Empty<Client>())
           .AddInMemoryApiResources(Enumerable.Empty<ApiResource>());

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

            app.UseIdentityServer();
        }
    }
}
