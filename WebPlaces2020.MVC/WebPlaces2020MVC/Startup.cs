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
using WebPlaces2020.CLI.Models;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FluentValidation;
using WebPlaces2020.Validator;
using WebPlaces2020.CLI.Context;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebPlaces2020.CLI
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
            IMvcBuilder mvcBuilder = services.AddMvc();

            services.AddIdentity<IdtUser, IdtRole>().AddEntityFrameworkStores<IdtDbContext>();
            services.AddDbContext<IdtDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().AddFluentValidation();
            services.AddDbContext<CompteContext>(opt => opt.UseInMemoryDatabase("AccountsList"));
            services.AddDbContext<PlaceContext>(opt => opt.UseInMemoryDatabase("PlacesList"));
            //services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(connectionString));
            services.AddControllersWithViews();
            services.AddTransient<IValidator<Place>, PlaceValidator>();
            services.AddTransient<IValidator<Compte>, AccountValidator>();
            services
                 .AddIdentityServer()
                 .AddDeveloperSigningCredential()
                 .AddInMemoryClients(new List<Client>
                 {
                    new Client
                    {
                        ClientId = "console",
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets = new List<Secret>
                        {
                            new Secret("secret".Sha512())
                        },
                        AllowedScopes =
                        {
                            "api1"
                        }
                    }
                 }).AddInMemoryApiResources(new List<ApiResource>
                 {
                    new ApiResource("api1", "test api")
                 });

            services.AddAuthentication(defaultScheme: "Bearer")
        .AddIdentityServerAuthentication(authenticationScheme: "Bearer", configureOptions: options =>
        {
            options.Authority = "http://localhost:44325";
            options.RequireHttpsMetadata = false;
            options.ApiName = "api1";
        });

            services.AddMvc(options => options.EnableEndpointRouting = false);

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
            app.UseAuthentication();

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
            app.UseMvcWithDefaultRoute();

        }
    }
}
